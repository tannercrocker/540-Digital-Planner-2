namespace Digital_Planner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Availability",
                c => new
                    {
                        AvailabilityID = c.Int(nullable: false, identity: true),
                        OccursAt = c.DateTime(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        DPUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AvailabilityID)
                .ForeignKey("dbo.DPUser", t => t.DPUserID)
                .Index(t => t.DPUserID);
            
            CreateTable(
                "dbo.DPUser",
                c => new
                    {
                        DPUserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DPUserID)
                .ForeignKey("dbo.User", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 500),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(maxLength: 50),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(maxLength: 150),
                        ClaimValue = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        DPUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.DPUser", t => t.DPUserID)
                .Index(t => t.DPUserID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        OccursAt = c.DateTime(nullable: false),
                        CompleteBy = c.DateTime(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        IsComplete = c.Boolean(nullable: false),
                        AutoAssign = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        Location = c.String(),
                        DPUserID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.DPUser", t => t.DPUserID)
                .Index(t => t.DPUserID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Availability", "DPUserID", "dbo.DPUser");
            DropForeignKey("dbo.Event", "DPUserID", "dbo.DPUser");
            DropForeignKey("dbo.Event", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Category", "DPUserID", "dbo.DPUser");
            DropForeignKey("dbo.DPUser", "ApplicationUserID", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.Event", new[] { "CategoryID" });
            DropIndex("dbo.Event", new[] { "DPUserID" });
            DropIndex("dbo.Category", new[] { "DPUserID" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.DPUser", new[] { "ApplicationUserID" });
            DropIndex("dbo.Availability", new[] { "DPUserID" });
            DropTable("dbo.Role");
            DropTable("dbo.Event");
            DropTable("dbo.Category");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.DPUser");
            DropTable("dbo.Availability");
        }
    }
}
