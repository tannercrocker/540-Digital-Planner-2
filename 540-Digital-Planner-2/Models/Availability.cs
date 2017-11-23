
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Planner.Models
{
    public class Availability
    {
        //PK
        [Key]
        public int AvailabilityID { get; set; }

        //Attributes
        [Required]
        [Display(Name = "Occurs At")]
        [UIHint("DateTimeSelector")]
        public DateTime OccursAt { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Length of Availability")]
        [UIHint("TimeSelector")]
        public TimeSpan Duration { get; set; } = new TimeSpan(0);

        //FK
        [Required]
        [ForeignKey("DPUser")]
        public int DPUserID { get; set; }

        //Navigation Properties
        public virtual DPUser DPUser { get; set; }
    }
}