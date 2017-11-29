namespace Digital_Planner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixSecurityStamp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "SecurityStamp", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "SecurityStamp", c => c.String());
        }
    }
}
