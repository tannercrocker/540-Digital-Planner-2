namespace Digital_Planner.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Digital_Planner.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<DigitalPlannerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DigitalPlannerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            int index;
            List<DPUser> user_entities = new List<DPUser>();
            List<Availability> avail_entities = new List<Availability>();
            List<Category> category_entities = new List<Category>();
            List<Event> event_entities = new List<Event>();

            List<ApplicationUser> test_users = new ApplicationDbContext().Users.Where(u => u.UserName.Contains("@thatsthepassword.com")).ToList();

            //Limit to five users
            while (test_users.Count() > 5)
            {
                test_users.RemoveAt(test_users.Count());
            }

            //Generate 5 DPUsers
            for (index = 1; index <= 5; index ++)
            {
                if (test_users.Count() > index)
                {
                    var u = test_users.ElementAt(index-1);
                    user_entities.Add(new DPUser(index, "first-" + u.Email.Split('@')[0], "last-" + u.Email.Split('@')[1], u.Id));
                }
            }
            context.DPUsers.AddOrUpdate(user_entities.ToArray());
            context.SaveChanges();


            //Generate 5 Availabilities for each DPUser
            foreach (DPUser u in context.DPUsers.Where(u => u.DPUserID <= 5))
            {
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 0);
                for (index = 1; index <= 15; index++)
                {
                    avail_entities.Add(new Availability(avail_entities.Count, date, 3, u.DPUserID));
                    date = date.AddDays(1.4);
                }
            }
            context.Availabilities.AddOrUpdate(avail_entities.ToArray());
            context.SaveChanges();


            //Generate 5 Categories for each DPUser
            foreach (DPUser u in context.DPUsers.Where(u => u.DPUserID <= 5))
            {
                DateTime month_day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                for (index = 1; index <= 5; index ++)
                {
                    //DPusers may have overlapping names, but we're just gonna separate them
                    category_entities.Add(new Category(category_entities.Count, "Category-" + (index * u.DPUserID), u.DPUserID));
                }
            }
            context.Categories.AddOrUpdate(category_entities.ToArray());
            context.SaveChanges();


            //Generate 15 events for each DPUser
            foreach (DPUser u in context.DPUsers.Where(u => u.DPUserID <= 5))
            {
                DateTime day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 0);
                for (index = 1; index <= 15; index++)
                {
                    //Add more events than there is time for in a day (as we set above)
                    event_entities.Add(
                        new Event(
                            event_entities.Count,
                            "Event-" + (index * u.DPUserID),
                            day,
                            day.AddDays(1),
                            new TimeSpan(1, 0, 0),
                            index % 4,
                            Convert.ToBoolean(index % 2),
                            Convert.ToBoolean(index % 2),
                            "Eternal Plains of Suffering",
                            u.DPUserID));
                    day = day.AddDays(1.4);
                }
            }
            context.Events.AddOrUpdate(event_entities.ToArray());
            context.SaveChanges();

        }
    }
}
