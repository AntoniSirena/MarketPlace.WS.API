namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateroleaddPersonTypeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "PersonTypeId", "dbo.PersonTypes");
            DropIndex("dbo.Users", new[] { "PersonTypeId" });
            AddColumn("dbo.Roles", "PersonTypeId", c => c.Int());
            CreateIndex("dbo.Roles", "PersonTypeId");
            AddForeignKey("dbo.Roles", "PersonTypeId", "dbo.PersonTypes", "Id");
            DropColumn("dbo.Users", "PersonTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PersonTypeId", c => c.Int());
            DropForeignKey("dbo.Roles", "PersonTypeId", "dbo.PersonTypes");
            DropIndex("dbo.Roles", new[] { "PersonTypeId" });
            DropColumn("dbo.Roles", "PersonTypeId");
            CreateIndex("dbo.Users", "PersonTypeId");
            AddForeignKey("dbo.Users", "PersonTypeId", "dbo.PersonTypes", "Id");
        }
    }
}
