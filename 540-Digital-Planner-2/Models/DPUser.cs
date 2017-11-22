
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Planner.Models
{
    public class DPUser
    {
        public DPUser() { }
        public DPUser(int id, string f, string l, string auth)
        {
            this.DPUserID = id;
            this.FirstName = f;
            this.LastName = l;
            this.ApplicationUserID = auth;
            this.ApplicationUser = new ApplicationDbContext().Users.FirstOrDefault(u => auth.Equals(u.Id));
        }

        //PK
        [Key]
        public int DPUserID { get; set; }

        //Attributes
        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; } = "";
        [Display(Name = "Last Name")]
        public String LastName { get; set; } = "";
        //Email & Password is in the ApplicationUser

        //FK
        [ForeignKey("ApplicationUser")]
        public String ApplicationUserID { get; set; }

        //Navigation Properties
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }
    }
}