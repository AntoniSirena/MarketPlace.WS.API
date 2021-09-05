namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajustinorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseTransactionStatusDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        Description = c.String(),
                        Colour = c.String(),
                        ShowToCustomer = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PurchaseTransactionDetails", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseTransactionDetails", "StatusId");
            AddForeignKey("dbo.PurchaseTransactionDetails", "StatusId", "dbo.PurchaseTransactionStatusDetails", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseTransactionDetails", "StatusId", "dbo.PurchaseTransactionStatusDetails");
            DropIndex("dbo.PurchaseTransactionDetails", new[] { "StatusId" });
            DropColumn("dbo.PurchaseTransactionDetails", "StatusId");
            DropTable("dbo.PurchaseTransactionStatusDetails");
        }
    }
}
