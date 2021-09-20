namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepurTranc : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PurchaseTransactions", "UserId");
            AddForeignKey("dbo.PurchaseTransactions", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseTransactions", "UserId", "dbo.Users");
            DropIndex("dbo.PurchaseTransactions", new[] { "UserId" });
        }
    }
}
