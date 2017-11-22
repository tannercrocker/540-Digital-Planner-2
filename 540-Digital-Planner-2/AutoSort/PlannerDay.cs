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
    class PlannerDay
    {
        Availability day;
        private TimeSpan totalTimeAvailable;
        private float remainingWorkMinutes;

        public PlannerDay(Availability day)
        {
            this.day = day;
            totalTimeAvailable = day.Duration;
            remainingWorkMinutes = day.OccursAt.Minute + day.OccursAt.Hour / 60;
        }

        public DateTime Date {
            get {
                return Date;
            }
        }

        public float RemainingWorkMinutes {
            get {
                return remainingWorkMinutes;
            }
            set {
                remainingWorkMinutes = value;
            }
        }
    }
}