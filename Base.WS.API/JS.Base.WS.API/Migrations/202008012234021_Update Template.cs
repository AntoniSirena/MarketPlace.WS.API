namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTemplate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Templateds", newName: "Templates");
            AddColumn("dbo.Templates", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Templates", "Name");
            RenameTable(name: "dbo.Templates", newName: "Templateds");
        }
    }
}
