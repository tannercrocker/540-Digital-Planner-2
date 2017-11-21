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

namespace Digital_Planner
{
    class PlannerDay
    {
        Models.Day day;
        private System.TimeSpan totalTimeAvailable;
        private float remainingWorkMinutes;

        public PlannerDay(Models.Day day)
        {
            this.day = day;
            totalTimeAvailable = day.HoursAvailable;
            remainingWorkMinutes = day.HoursAvailable.Minutes;
        }

        public System.DateTime Date {
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