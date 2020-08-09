namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "CanViewActionsButton", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "CanApprove", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "CanSendToObservation", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "CanProcess", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "CanCancel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "CanCancel");
            DropColumn("dbo.Roles", "CanProcess");
            DropColumn("dbo.Roles", "CanSendToObservation");
            DropColumn("dbo.Roles", "CanApprove");
            DropColumn("dbo.Roles", "CanViewActionsButton");
        }
    }
}
