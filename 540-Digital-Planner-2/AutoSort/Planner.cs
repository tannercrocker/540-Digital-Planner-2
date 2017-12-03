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
using Microsoft.AspNet.Identity;

namespace Digital_Planner.Sorting
{
    static class Planner
    {
        public static void GenerateSchedule(ApplicationUser user)
        {
            //Debug
            System.Diagnostics.Debug.Print("Generate Schedule");

            List<PlannerEvent> autoEvents = new List<PlannerEvent>();
            List<PlannerAvailability> availabilities = new List<PlannerAvailability>();

            Tuple<List<PlannerEvent>, List<PlannerAvailability>> some_tuple = GetDataFromDatabase(autoEvents, availabilities, user);
            autoEvents = some_tuple.Item1;
            availabilities = some_tuple.Item2;

            autoEvents = SortEvents(autoEvents, availabilities);

            List<Event> events_to_update = AssignWorkDays(autoEvents, availabilities);

            using (var AppDB = new ApplicationDbContext())
            {
                foreach (var evt in events_to_update)
                {
                    //Re-load event from DB, because it got lost by the "change tracker"
                    var e = AppDB.Events.Single(@event => @event.EventID == evt.EventID);
                    e.OccursAt = evt.OccursAt;
                    AppDB.Entry(e);
                    AppDB.SaveChanges();
                }
            }

        }


        private static Tuple<List<PlannerEvent>, List<PlannerAvailability>> GetDataFromDatabase(List<PlannerEvent> autoEvents, List<PlannerAvailability> availabilities, ApplicationUser user)
        {
            //Debug
            System.Diagnostics.Debug.Print("Get Data From Database");

            //Get all database records
            List<Event> plannerEvents = user.getEvents();
            List<Availability> plannerAvailabilities = user.getAvailabilities();
            
            //get the specified users automatic events
            for (int i = 0; i < plannerEvents.Count(); i++)
                if (plannerEvents[i].AutoAssign)
                    //if (userID == "all" || plannerEvents[i].UserID == userID)
                        autoEvents.Add(new PlannerEvent(plannerEvents[i]));

            //get the specified user's availabilities
            for (int i = 0; i < plannerAvailabilities.Count(); i++)
                //if (plannerAvailabilities[i].UserID == userID)
                    availabilities.Add(new PlannerAvailability(plannerAvailabilities[i]));
                    

            //Debug
            System.Diagnostics.Debug.Print("Availabilities List Count: " + availabilities.Count);
            System.Diagnostics.Debug.Print("Auto List Count: " + autoEvents.Count);

            return new Tuple<List<PlannerEvent>, List<PlannerAvailability>>(autoEvents, availabilities);
        }


        private static List<PlannerEvent> SortEvents(List<PlannerEvent> autoEvents, List<PlannerAvailability> days)
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
                int highestIndex = 0;   //Tracks the index of the highest score

                //Get the index of the highest score
                for (int i = 0; i < temp.Count(); i++)
                    if (temp[i].getScore() > temp[highestIndex].getScore())
                        highestIndex = i;

                autoEvents.Add(temp[highestIndex]);
                temp.RemoveAt(highestIndex);
            }

            return autoEvents;
        }


        private static List<Event> AssignWorkDays(List<PlannerEvent> autoEvents, List<PlannerAvailability> availabilities)
        {
            //Debug
            System.Diagnostics.Debug.Print("Assign Work Days");

            List<Event> events_to_update = new List<Event>();
            int availableIndex = 0;
            int eventIndex = 0;
            int last_update = -1;

            //start at the first availability and keep adding events until no events fit
            //then move to next availability.  Repeat until all events have been assigned
            // or until there are no availabilities left
            while (availableIndex < availabilities.Count() && eventIndex < autoEvents.Count())
            {
                //That check isn't needed. While loop won't run if 0 == 0
                if (/*availabilities.Count > 0 && */availabilities[availableIndex].RemainingWorkMinutes >= autoEvents[eventIndex].Duration.TotalMinutes)
                {
                    var current_availability = availabilities[availableIndex];
                    var current_plannerevent = autoEvents[eventIndex];

                    current_plannerevent.OccursAt = 
                        current_availability.Availability.OccursAt
                        //Adjust the start time of the event
                        .AddMinutes(
                            current_availability.timeAvailable.TotalMinutes - current_availability.RemainingWorkMinutes
                            );
                    //Subtract the event duration after setting the new time for the event.
                    current_availability.RemainingWorkMinutes -= current_plannerevent.Duration.TotalMinutes;

                    System.Diagnostics.Debug.Print("'" + current_plannerevent.Title + "' assigned to " + current_availability.Availability.OccursAt + " - new date: " + current_plannerevent.OccursAt);

                    events_to_update.Add(current_plannerevent.getEvent());
                    last_update = eventIndex;
                    //autoEvents.RemoveAt(eventIndex);
                }

                if (eventIndex >= autoEvents.Count)
                {
                    //availableIndex++;
                    eventIndex = last_update+1;
                }

                availableIndex++;
                /*
                if (availabilities[availableIndex].RemainingWorkMinutes <= 0)
                {
                    availableIndex++;
                }
                */

            }

            return events_to_update;
        }

    }
}