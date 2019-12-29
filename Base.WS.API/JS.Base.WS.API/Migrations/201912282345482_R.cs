namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class R : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "PersonId", "dbo.People");
            DropIndex("dbo.Users", new[] { "PersonId" });
            AlterColumn("dbo.Users", "PersonId", c => c.Long());
            CreateIndex("dbo.Users", "PersonId");
            AddForeignKey("dbo.Users", "PersonId", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PersonId", "dbo.People");
            DropIndex("dbo.Users", new[] { "PersonId" });
            AlterColumn("dbo.Users", "PersonId", c => c.Long(nullable: false));
            CreateIndex("dbo.Users", "PersonId");
            AddForeignKey("dbo.Users", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
