namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastLoginTime", c => c.DateTime());
            AddColumn("dbo.Users", "IsOnline", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DiviceIP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DiviceIP");
            DropColumn("dbo.Users", "IsOnline");
            DropColumn("dbo.Users", "LastLoginTime");
        }
    }
}
