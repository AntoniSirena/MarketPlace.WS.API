namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "CanRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "CanDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "CanDelete");
            DropColumn("dbo.Roles", "CanRead");
        }
    }
}
