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
            AutomaticMigrationsEnabled = false;
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

            //Generate 5 DPUsers
            for (index = 1; index <= 5; index ++)
            {
                user_entities.Add(new DPUser(index, "first-" + index, "last-" + index, ""));
            }
            context.DPUsers.AddOrUpdate(user_entities.ToArray());

            //Generate 5 Availabilities for each DPUser
            foreach (DPUser u in context.DPUsers)
            {
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 0);
                for (index = 1; index <= 15; index++)
                {
                    avail_entities.Add(new Availability(avail_entities.Count, date, 3, u.DPUserID));
                    date = date.AddDays(1.4);
                }
            }
            context.Availabilities.AddOrUpdate(avail_entities.ToArray());

            //Generate 5 Categories for each DPUser
            foreach(DPUser u in context.DPUsers)
            {
                DateTime month_day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                for (index = 1; index <= 5; index ++)
                {
                    //DPusers may have overlapping names, but we're just gonna separate them
                    category_entities.Add(new Category(category_entities.Count, "Category-" + (index * u.DPUserID), u.DPUserID));
                }
            }
            context.Categories.AddOrUpdate(category_entities.ToArray());
            
            //Generate 15 events for each DPUser
            foreach (DPUser u in context.DPUsers)
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
        }
    }
}
