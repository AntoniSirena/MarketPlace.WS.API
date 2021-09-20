namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepurtdet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseTransactionStatusDetails", "ProviderStatusDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseTransactionStatusDetails", "ProviderStatusDescription");
        }
    }
}
