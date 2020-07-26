namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRole_ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "CanCreate", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "CanCreate");
        }
    }
}
