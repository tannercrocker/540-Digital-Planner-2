namespace Digital_Planner.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Digital_Planner.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            

            /* You need to manually create a couple of test users 
             * (The first five DPUsers are seeded. [first based on DPUserID]) 
             */
            

            //TC - Commenting so that this doesn't run accidentally
            /*

            int index;

            //Generate 5 Availabilities for each DPUser
            foreach (DPUser u in context.DPUsers.Where(u => u.DPUserID <= 5))
            {
                List<Availability> avails = new List<Availability>();
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
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
                    var c = new Category() { Description = "Category-" + (index * u.DPUserID), DPUserID = u.DPUserID };
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
                DateTime day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                for (index = 1; index <= 10; index++)
                {
                    foreach (var cat in u.Categories)
                    {
                        //Add more events than there is time for in a day (as we set above)
                        var e =
                            new Event()
                            {
                                Title = "Event-" + (index * u.DPUserID),
                                OccursAt = day,
                                CompleteBy = day.AddDays(1),
                                Duration = new TimeSpan(1, 0, 0),
                                Priority = index % 4,
                                AutoAssign = Convert.ToBoolean(index % 2),
                                IsComplete = Convert.ToBoolean(index % 2),
                                Location = "Eternal Plains of Suffering",
                                DPUserID = u.DPUserID,
                                CategoryID = cat.CategoryID
                            };
                        evts.Add(e);
                    }
                    day = day.AddDays(1.4);
                }
                u.Events = evts;
            }
            context.SaveChanges();
        */
        }
    }
}
