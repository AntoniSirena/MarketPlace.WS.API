namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodelent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enterprises", "ScheduleHourCloseId", c => c.Int());
            CreateIndex("dbo.Enterprises", "ScheduleHourCloseId");
            AddForeignKey("dbo.Enterprises", "ScheduleHourCloseId", "dbo.ScheduleHours", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enterprises", "ScheduleHourCloseId", "dbo.ScheduleHours");
            DropIndex("dbo.Enterprises", new[] { "ScheduleHourCloseId" });
            DropColumn("dbo.Enterprises", "ScheduleHourCloseId");
        }
    }
}
