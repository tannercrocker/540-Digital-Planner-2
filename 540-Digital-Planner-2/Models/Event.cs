
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Planner.Models
{
    public class Event
    {
        public Event() { }
        public Event(int id, string t, DateTime o, DateTime c, TimeSpan d, int p, bool aa, bool ic, string loc, int u_id)
        {
            this.EventID = id;
            this.Title = t;
            this.OccursAt = o;
            this.CompleteBy = c;
            this.Duration = d;
            this.Priority = p;
            this.AutoAssign = aa;
            this.IsComplete = ic;
            this.Location = loc;
            this.DPUserID = u_id;
            this.DPUser = new DigitalPlannerDbContext().DPUsers.FirstOrDefault(u => u.DPUserID == u_id);
        }

        //PK
        [Key]
        public int EventID { get; set; }

        //Attributes
        [Required]
        public String Title { get; set; } = "Title";
        [Required]
        [Display(Name = "Occurs At")]
        public DateTime OccursAt { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Complete By")]
        public DateTime CompleteBy { get; set; } = DateTime.Now.AddDays(1);
        [Required]
        [Display(Name = "Length of Event")]
        public TimeSpan Duration { get; set; } = new TimeSpan(0);
        [Required]
        [Display(Name = "Is Complete")]
        public bool IsComplete { get; set; } = false;
        [Required]
        [Display(Name = "AutomaticAssignment")]
        public bool AutoAssign { get; set; } = false;
        public int Priority { get; set; } = 1;
        public String Location { get; set; } = "";

        //FKs
        [ForeignKey("DPUser")]
        public int DPUserID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        //Navigation Properties
        public virtual DPUser DPUser { get; set; }
        public virtual Category Category { get; set; }
    }
}