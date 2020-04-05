namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparentinrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "Parent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "Parent");
        }
    }
}
