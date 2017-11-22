
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
        [Required]
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