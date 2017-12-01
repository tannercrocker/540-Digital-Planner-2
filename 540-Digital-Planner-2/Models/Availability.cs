
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

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
        public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 0);

        //FK
        [ForeignKey("User")]
        public string UserID { get; set; }

        //Navigation Properties
        public virtual ApplicationUser User { get; set; }
    }
}