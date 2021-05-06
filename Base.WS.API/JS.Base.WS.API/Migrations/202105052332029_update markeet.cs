namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemarkeet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markets", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Markets", "Description");
        }
    }
}
