
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
        public String Title { get; set; } = "";
        [Required]
        [Display(Name = "Occurs At")]
        [UIHint("DateTimeSelector")]
        public DateTime OccursAt { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Complete By")]
        [UIHint("DateTimeSelector")]
        public DateTime CompleteBy { get; set; } = DateTime.Now.AddDays(1);
        [Required]
        [Display(Name = "Length of Event")]
        [UIHint("TimeSelector")]
        public TimeSpan Duration { get; set; } = new TimeSpan(0);
        [Required]
        [Display(Name = "Is Complete")]
        [UIHint("CompletionCheck")]
        public bool IsComplete { get; set; } = false;
        [Required]
        [Display(Name = "Automatic Assignment")]
        [UIHint("AutomaticAssignment")]
        public bool AutoAssign { get; set; } = false;
        [Required]
        [UIHint("PriorityButtons")]
        public int Priority { get; set; } = 1;
        public String Location { get; set; } = "";

        //FKs
        [Required]
        [ForeignKey("DPUser")]
        public int DPUserID { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        //Navigation Properties
        public virtual DPUser DPUser { get; set; }
        public virtual Category Category { get; set; }
    }
}