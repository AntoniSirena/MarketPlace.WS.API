namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAccompanyingInstrumentRequest_ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccompanyingInstrumentRequests", "VisitBIsvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccompanyingInstrumentRequests", "VisitCIsvailable", c => c.Boolean(nullable: false));
            DropColumn("dbo.AccompanyingInstrumentRequests", "VisitAIsBvailable");
            DropColumn("dbo.AccompanyingInstrumentRequests", "VisitAIsCvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccompanyingInstrumentRequests", "VisitAIsCvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccompanyingInstrumentRequests", "VisitAIsBvailable", c => c.Boolean(nullable: false));
            DropColumn("dbo.AccompanyingInstrumentRequests", "VisitCIsvailable");
            DropColumn("dbo.AccompanyingInstrumentRequests", "VisitBIsvailable");
        }
    }
}
