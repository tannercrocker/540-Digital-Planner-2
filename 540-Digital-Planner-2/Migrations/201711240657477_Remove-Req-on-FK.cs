namespace Digital_Planner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveReqonFK : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DPUser", new[] { "UserID" });
            AlterColumn("dbo.DPUser", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.DPUser", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DPUser", new[] { "UserID" });
            AlterColumn("dbo.DPUser", "UserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.DPUser", "UserID");
        }
    }
}
