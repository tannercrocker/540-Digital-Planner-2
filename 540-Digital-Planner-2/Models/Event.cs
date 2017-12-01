
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

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
        public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 0);
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
        [ForeignKey("User")]
        public string UserID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        //Navigation Properties
        public virtual ApplicationUser User { get; set; }
        //public virtual DPUser DPUser { get; set; }
        public virtual Category Category { get; set; }
    }
}