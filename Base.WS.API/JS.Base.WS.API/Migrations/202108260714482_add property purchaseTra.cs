namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpropertypurchaseTra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseTransactions", "FormattedDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseTransactions", "FormattedDate");
        }
    }
}
