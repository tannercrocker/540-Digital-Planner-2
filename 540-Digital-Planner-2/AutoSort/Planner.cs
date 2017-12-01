/*
*   File:           Planner.cs
*   Author:         Benjamin Albrecht 
*   Date:           11/29/2017
*   Description:    Automatically sorts and assigns events when GenerateSchedule() is called.
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Digital_Planner.Models;

namespace Digital_Planner.Sorting
{
    static class Planner
    {
        //private static ApplicationDbContext db = new ApplicationDbContext();

        public static void GenerateSchedule(ApplicationUser user)
        {
            //Debug
            System.Diagnostics.Debug.Print("Generate Schedule");

            List<PlannerEvent> autoEvents = new List<PlannerEvent>();
            List<PlannerAvailability> availabilities = new List<PlannerAvailability>();

            GetDataFromDatabase(autoEvents, availabilities, user);
            SortEvents(autoEvents, availabilities);
            AssignWorkDays(autoEvents, availabilities);

            using (var db = new ApplicationDbContext())
            {
                foreach(var evt in autoEvents)
                {
                    db.Entry(evt);
                }
                db.SaveChanges();
            }
        }


        private static void GetDataFromDatabase(List<PlannerEvent> autoEvents, List<PlannerAvailability> availabilities, ApplicationUser user)
        {
            //Debug
            System.Diagnostics.Debug.Print("Get Data From Database");

            //Get all database records
            List<Event> plannerEvents = user.getEvents();// db.Events.ToList();
            List<Availability> plannerAvailabilities = user.getAvailabilities();// db.Availabilities.ToList();
            
            //get the specified users automatic events
            for (int i = 0; i < plannerEvents.Count; i++)
                if (plannerEvents[i].AutoAssign)
                    //if (userID == "all" || plannerEvents[i].UserID == userID)
                        autoEvents.Add(new PlannerEvent(plannerEvents[i]));

            //get the specified user's availabilities
            for (int i = 0; i < plannerAvailabilities.Count; i++)
                //if (plannerAvailabilities[i].UserID == userID)
                    availabilities.Add(new PlannerAvailability(plannerAvailabilities[i]));
                    

            //Debug
            System.Diagnostics.Debug.Print("Availabilities List Count: " + availabilities.Count);
            System.Diagnostics.Debug.Print("Auto List Count: " + autoEvents.Count);
        }


        private static void SortEvents(List<PlannerEvent> autoEvents, List<PlannerAvailability> days)
        {
            //Debug
            System.Diagnostics.Debug.Print("Sort Events");

            //Create temp list to temporarily hold the contents of autoEvents
            List<PlannerEvent> temp = new List<PlannerEvent>();

            //populate temp with the contents of autoEvents
            for (int i = 0; i < autoEvents.Count; i++)
                temp.Add(autoEvents[i]);

            autoEvents.Clear();

            //add events to autoEvents in order of score
            while (temp.Count > 0)
            {
                int highestIndex = 0;
                for (int i = 0; i < temp.Count; i++)
                    if (temp[i].Score > temp[highestIndex].Score) highestIndex = i;
                autoEvents.Add(temp[highestIndex]);
                temp.RemoveAt(highestIndex);
            }
        }


        private static void AssignWorkDays(List<PlannerEvent> autoEvents, List<PlannerAvailability> days)
        {
            //Debug
            System.Diagnostics.Debug.Print("Assign Work Days");

            int dayIndex = 0;
            int eventIndex = 0;

            //start at the first day and keep adding events until no events fit
            //then move to next day.  Repeat until all events have been assigned
            while (dayIndex < days.Count && autoEvents.Count > 0)
            {
                if (days.Count > 0 && days[dayIndex].RemainingWorkMinutes >= autoEvents[eventIndex].Duration.Minutes)
                {
                    days[dayIndex].RemainingWorkMinutes -= autoEvents[eventIndex].Duration.Minutes;
                    autoEvents[eventIndex].OccursAt = days[dayIndex].Date;
                    System.Diagnostics.Debug.Print(autoEvents[eventIndex].Title + " assign to " + days[dayIndex].Date + " - new date: " + autoEvents[eventIndex].OccursAt);
                    autoEvents.RemoveAt(eventIndex);
                }
                else
                {
                    eventIndex++;
                    if (eventIndex >= autoEvents.Count)
                    {
                        dayIndex++;
                        eventIndex = 0;
                    }

                }
            }
        }

    }
}