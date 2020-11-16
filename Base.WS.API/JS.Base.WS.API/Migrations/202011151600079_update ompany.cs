namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyRegisters", "IsReviewed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyRegisters", "IsReviewed");
        }
    }
}
