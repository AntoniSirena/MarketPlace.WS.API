namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColourinUserStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserStatus", "Colour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserStatus", "Colour");
        }
    }
}
