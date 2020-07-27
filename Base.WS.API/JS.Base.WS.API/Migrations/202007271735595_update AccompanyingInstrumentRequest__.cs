namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAccompanyingInstrumentRequest__ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccompanyingInstrumentRequests", "VisitBIsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccompanyingInstrumentRequests", "VisitCIsAvailable", c => c.Boolean(nullable: false));
            DropColumn("dbo.AccompanyingInstrumentRequests", "VisitBIsvailable");
            DropColumn("dbo.AccompanyingInstrumentRequests", "VisitCIsvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccompanyingInstrumentRequests", "VisitCIsvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccompanyingInstrumentRequests", "VisitBIsvailable", c => c.Boolean(nullable: false));
            DropColumn("dbo.AccompanyingInstrumentRequests", "VisitCIsAvailable");
            DropColumn("dbo.AccompanyingInstrumentRequests", "VisitBIsAvailable");
        }
    }
}
