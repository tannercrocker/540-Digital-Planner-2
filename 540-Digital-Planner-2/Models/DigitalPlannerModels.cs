namespace Digital_Planner.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DigitalPlannerModels : DbContext
    {
        // Your context has been configured to use a 'DigitalPlannerModels' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Digital_Planner.Models.DigitalPlannerModels' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DigitalPlannerModels' 
        // connection string in the application configuration file.
        public DigitalPlannerModels()
            : base("name=DigitalPlannerModels")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<DPUser> DPUsers { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Availability> Days { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class DPUser
    {
        //PK
        [Key]
        public int DPUserID { get; set; }

        //Attributes
        public int FirstName { get; set; }
        public int LastName { get; set; }
            //Email & Password is in the ApplicationUser
        
        //FK
        public String ApplicationUserID { get; set; }

        //Navigation Properties
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Availability> Days { get; set; }
    }

    public class Event
    {
        //PK
        [Key]
        public int EventID { get; set; }
        
        //Attributes
        public String Title { get; set; }
        public DateTime OccursAt { get; set; }
        public DateTime CompleteBy { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsComplete { get; set; }
        public bool AutoAssign { get; set; }
        public string Location { get; set; }

        //FKs
        public int DPUserID { get; set; }
        public int CategoryID { get; set; }

        //Navigation Properties
        [ForeignKey("DPUserID")]
        public virtual DPUser DPUser { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }

    public class Category
    {
        //PK
        [Key]
        public int CategoryID { get; set; }

        //Attributes
        public String Description { get; set; }

        //FK
        public int DPUserID { get; set; }

        //Navigation Properties
        [ForeignKey("DPUserID")]
        public virtual DPUser DPUser { get; set; }
        public virtual ICollection<Event> Events { get; set; }


    }

    public class Availability
    {
        //PK
        [Key]
        public int AvailabilityID { get; set; }
        
        //Attributes
        public DateTime OccursAt { get; set; }
        public TimeSpan Duration { get; set; }

        //FK
        public int DPUserID { get; set; }

        //Navigation Properties
        [ForeignKey("DPUserID")]
        public virtual DPUser DPUser { get; set; }
    }
}