namespace Digital_Planner.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DigitalPlannerModels : ApplicationDbContext
    {
        // Your context has been configured to use a 'DigitalPlannerModels' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Digital_Planner.Models.DigitalPlannerModels' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DigitalPlannerModels' 
        // connection string in the application configuration file.
        public DigitalPlannerModels()
        //    : base("name=DigitalPlannerModels")
        {
            Database.SetInitializer<DigitalPlannerModels>(null);// Remove default initializer
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<DPUser> DPUsers { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Availability> Days { get; set; }
        
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
        public virtual ICollection<Availability> Days { get; set; }
    }

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

    public class Category
    {
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

    public class Availability
    {
        //PK
        [Key]
        public int AvailabilityID { get; set; }
        
        //Attributes
        [Required]
        [Display(Name = "Occurs At")]
        public DateTime OccursAt { get; set; }
        [Required]
        [Display(Name = "Length of Availability")]
        public TimeSpan Duration { get; set; }

        //FK
        [ForeignKey("DPUser")]
        public int DPUserID { get; set; }

        //Navigation Properties
        public virtual DPUser DPUser { get; set; }
    }
}