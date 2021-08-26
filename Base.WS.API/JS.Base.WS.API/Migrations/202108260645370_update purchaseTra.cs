namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepurchaseTra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseTransactions", "TransactionTypeId", c => c.Int(nullable: false, defaultValue: 2));
            CreateIndex("dbo.PurchaseTransactions", "TransactionTypeId");
            AddForeignKey("dbo.PurchaseTransactions", "TransactionTypeId", "dbo.PurchaseTransactionTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseTransactions", "TransactionTypeId", "dbo.PurchaseTransactionTypes");
            DropIndex("dbo.PurchaseTransactions", new[] { "TransactionTypeId" });
            DropColumn("dbo.PurchaseTransactions", "TransactionTypeId");
        }
    }
}
