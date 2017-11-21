
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
        public Category() { }
        public Category(int c_id, string desc, int u_id)
        {
            this.CategoryID = c_id;
            this.Description = desc;
            this.DPUserID = u_id;
            this.DPUser = new DigitalPlannerDbContext().DPUsers.FirstOrDefault(u => u.DPUserID == u_id);
        }

        //PK
        [Key]
        public int CategoryID { get; set; }

        //Attributes
        [Required]
        public String Description { get; set; } = "Category";

        //FK
        [ForeignKey("DPUser")]
        public int DPUserID { get; set; }

        //Navigation Properties
        public virtual DPUser DPUser { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}