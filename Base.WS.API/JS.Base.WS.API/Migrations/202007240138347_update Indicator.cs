namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateIndicator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicators", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Indicators", "IsEvaluationFactor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Indicators", "IsEvaluationFactor");
            DropColumn("dbo.Indicators", "Value");
        }
    }
}
