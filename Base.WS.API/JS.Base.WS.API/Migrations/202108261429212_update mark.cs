namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemark : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Markets", "ProductTypeId", "dbo.ProductTypes");
            DropIndex("dbo.Markets", new[] { "ProductTypeId" });
            AlterColumn("dbo.Markets", "ProductTypeId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Markets", "ProductTypeId");
            AddForeignKey("dbo.Markets", "ProductTypeId", "dbo.ProductTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Markets", "ProductTypeId", "dbo.ProductTypes");
            DropIndex("dbo.Markets", new[] { "ProductTypeId" });
            AlterColumn("dbo.Markets", "ProductTypeId", c => c.Int());
            CreateIndex("dbo.Markets", "ProductTypeId");
            AddForeignKey("dbo.Markets", "ProductTypeId", "dbo.ProductTypes", "Id");
        }
    }
}
