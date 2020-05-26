namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastLoginTimeEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastLoginTimeEnd");
        }
    }
}
