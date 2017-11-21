using System.Data.Entity;

namespace Digital_Planner.Models
{
    public class DigitalPlannerDbContext : ApplicationDbContext
    {
        // Your context has been configured to use a 'DigitalPlannerDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Digital_Planner.Models.DigitalPlannerDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DigitalPlannerDbContext' 
        // connection string in the application configuration file.
        public DigitalPlannerDbContext()
        //    : base("name=DigitalPlannerDbContext")
        {
            Database.SetInitializer<DigitalPlannerDbContext>(null);// Remove default initializer
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<DPUser> DPUsers { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Availability> Availabilities { get; set; }
        
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

}