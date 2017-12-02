using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Digital_Planner.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public List<Event> getEvents()
        {
            List<Event> list = new List<Event>();
            using (var db = new ApplicationDbContext())
            {
                var l2 = db.Events.Where(e => e.UserID == this.Id).ToList();
                list = l2;
            }
            return list;
        }

        public List<Availability> getAvailabilities()
        {
            List<Availability> list = new List<Availability>();
            using (var db = new ApplicationDbContext())
            {
                var l2 = db.Availabilities.Where(e => e.UserID == this.Id).ToList();
                list = l2;
            }
            return list;
        }

        public List<Category> getCategories()
        {
            List<Category> list = new List<Category>();
            using (var db = new ApplicationDbContext())
            {
                var l2 = db.Categories.Where(e => e.UserID == this.Id).ToList();
                list = l2;
            }
            return list;
        }

        public List<Event> getEventsWithCategories()
        {
            List<Event> list = new List<Event>();
            using (var db = new ApplicationDbContext())
            {
                var l2 = db.Events.Where(e => e.UserID == this.Id).Include(e => e.Category).ToList();
                list = l2;
            }
            return list;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public ApplicationDbContext()
            : base("DigitalPlannerModels")
        {
            /*
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = false;
            */
        }
        

        //public virtual DbSet<DPUser> DPUsers { get; set; }
        //public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Availability> Availabilities { get; set; }

        public virtual DbSet<IdentityUserLogin> UserLogins { get; set; }
        public virtual DbSet<IdentityUserClaim> UserClaims { get; set; }
        public virtual DbSet<IdentityUserRole> UserRoles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Availabilities)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Categories)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Events)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserID);
                
        }*/
            /*
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
                modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();

                // Configure Asp Net Identity Tables
                modelBuilder.Entity<ApplicationUser>().ToTable("User");
                modelBuilder.Entity<ApplicationUser>().Property(u => u.PasswordHash).HasMaxLength(500);
                //TC - got this code from: /u/'Augusto Baretto' 's second option from their answer of: https://stackoverflow.com/questions/28531201/entitytype-identityuserlogin-has-no-key-defined-define-the-key-for-this-entit
                modelBuilder.Entity<ApplicationUser>().Property(u => u.SecurityStamp).HasMaxLength(500);
                modelBuilder.Entity<ApplicationUser>().Property(u => u.PhoneNumber).HasMaxLength(50);

                modelBuilder.Entity<IdentityRole>().ToTable("Role");
                modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
                modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
                modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
                modelBuilder.Entity<IdentityUserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
                modelBuilder.Entity<IdentityUserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);
            }
            */
        }
    }