namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuseraddPersonTypeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PersonTypeId", c => c.Int());
            CreateIndex("dbo.Users", "PersonTypeId");
            AddForeignKey("dbo.Users", "PersonTypeId", "dbo.PersonTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PersonTypeId", "dbo.PersonTypes");
            DropIndex("dbo.Users", new[] { "PersonTypeId" });
            DropColumn("dbo.Users", "PersonTypeId");
        }
    }
}
