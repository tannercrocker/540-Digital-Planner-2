
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Planner.Models
{
    public class Category
    {
        //PK
        [Key]
        public int CategoryID { get; set; }

        //Attributes
        [Required]
        public String Description { get; set; } = "";

        //FK
        [ForeignKey("User")]
        public string UserID { get; set; }

        //Navigation Properties
        public virtual ApplicationUser User { get; set; }


        public virtual ICollection<Event> Events { get; set; }
    }
}