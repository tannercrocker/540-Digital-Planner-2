namespace Digital_Planner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriorityToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "Priority");
        }
    }
}
