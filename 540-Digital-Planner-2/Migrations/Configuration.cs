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

            //TC - This isn't working. I don't know why.
            /*
            int index;

            List<ApplicationUser> test_users = new ApplicationDbContext().Users.Where(u => u.UserName.Contains("@thatsthepassword.com")).ToList();

            //Limit to five users
            while (test_users.Count() > 5)
            {
                test_users.RemoveAt(test_users.Count());
            }

            if (test_users.Count() > 0)
            {
                //Generate 5 DPUsers
                index = 0;
                foreach (var tester in test_users)
                {
                    index++;
                    var dp = new DPUser() { DPUserID = index, FirstName = tester.Email.Split('@')[0], LastName = tester.Email.Split('@')[1], User = tester, UserID = tester.Id };
                    //context.DPUsers.Add(dp);
                    context.DPUsers.AddOrUpdate(dp);
                }
                index = 0;
                context.SaveChanges();


                //Generate 5 Availabilities for each DPUser
                foreach (DPUser u in context.DPUsers.Where(u => u.DPUserID <= 5))
                {
                    List<Availability> avails = new List<Availability>();
                    DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 0);
                    for (index = 1; index <= 15; index++)
                    {
                        var a = new Availability() { AvailabilityID = avails.Count(), DPUser = u, OccursAt = date, Duration = new TimeSpan(3, 0, 0) };
                        avails.Add(a);
                        date = date.AddDays(1.4);
                    }
                    u.Availabilities = avails;
                }
                index = 0;
                context.SaveChanges();


                //Generate 5 Categories for each DPUser
                foreach (DPUser u in context.DPUsers.Where(u => u.DPUserID <= 5))
                {
                    List<Category> cats = new List<Category>();
                    DateTime month_day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    for (index = 1; index <= 5; index++)
                    {
                        var c = new Category() { CategoryID = cats.Count(), Description = "Category-" + (index * u.DPUserID), DPUser = u, DPUserID = u.DPUserID };
                        cats.Add(c);
                    }
                    u.Categories = cats;
                }
                index = 0;
                context.SaveChanges();


                //Generate 15 events for each DPUser
                foreach (DPUser u in context.DPUsers.Where(u => u.DPUserID <= 5))
                {
                    List<Event> evts = new List<Event>();
                    DateTime day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 0);
                    for (index = 1; index <= 15; index++)
                    {
                        //Add more events than there is time for in a day (as we set above)
                        var e =
                            new Event()
                            {
                                EventID = evts.Count(),
                                Title = "Event-" + (index * u.DPUserID),
                                OccursAt = day,
                                CompleteBy = day.AddDays(1),
                                Duration = new TimeSpan(1, 0, 0),
                                Priority = index % 4,
                                AutoAssign = Convert.ToBoolean(index % 2),
                                IsComplete = Convert.ToBoolean(index % 2),
                                Location = "Eternal Plains of Suffering",
                                DPUser = u,
                                DPUserID = u.DPUserID
                            };
                        evts.Add(e);
                        day = day.AddDays(1.4);
                    }
                    u.Events = evts;
                }
                context.SaveChanges();
            }
           */
        }
    }
}
