/*
*   File:           PlannerCalenderDate.cs
*   Author:         Benjamin Albrecht
*   Date:           11/19/2017
*   Description:    
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Planner.Models;

namespace Digital_Planner.Sorting
{
    class PlannerAvailability
    {
        public Availability Availability { get; set; }
        public TimeSpan timeAvailable { get; set; }
        public double RemainingWorkMinutes { get; set; }

        public PlannerAvailability(Availability day)
        {
            this.Availability = day;
            this.timeAvailable = day.Duration;
            this.RemainingWorkMinutes = day.Duration.TotalMinutes;// day.OccursAt.Minute + (day.OccursAt.Hour * 60.0);
        }
        
    }
}