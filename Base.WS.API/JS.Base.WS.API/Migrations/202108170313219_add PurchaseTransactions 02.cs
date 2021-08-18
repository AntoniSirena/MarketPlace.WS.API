namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPurchaseTransactions02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseTransactionDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TransactionId = c.Long(nullable: false),
                        ArticleId = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Markets", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseTransactions", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.TransactionId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.PurchaseTransactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        CurrencyISONumber = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmountPending = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        CreationTime = c.DateTime(),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseTransactionStatus", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseTransactionTypes", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.TransactionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseTransactions", "TransactionId", "dbo.PurchaseTransactionTypes");
            DropForeignKey("dbo.PurchaseTransactions", "StatusId", "dbo.PurchaseTransactionStatus");
            DropForeignKey("dbo.PurchaseTransactionDetails", "TransactionId", "dbo.PurchaseTransactions");
            DropForeignKey("dbo.PurchaseTransactionDetails", "ArticleId", "dbo.Markets");
            DropIndex("dbo.PurchaseTransactions", new[] { "TransactionId" });
            DropIndex("dbo.PurchaseTransactions", new[] { "StatusId" });
            DropIndex("dbo.PurchaseTransactionDetails", new[] { "ArticleId" });
            DropIndex("dbo.PurchaseTransactionDetails", new[] { "TransactionId" });
            DropTable("dbo.PurchaseTransactions");
            DropTable("dbo.PurchaseTransactionDetails");
        }
    }
}
