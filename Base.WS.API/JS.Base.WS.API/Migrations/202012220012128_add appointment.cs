namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addappointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EnterpriseId = c.Long(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Name = c.String(),
                        DocumentNomber = c.String(),
                        PhoneNomber = c.String(),
                        Comment = c.String(),
                        StartDate = c.String(),
                        ScheduledAppointment = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppointmentStatus", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId, cascadeDelete: true)
                .Index(t => t.EnterpriseId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.AppointmentStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        Description = c.String(),
                        Colour = c.String(),
                        ShowToCustomer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.Appointments", "StatusId", "dbo.AppointmentStatus");
            DropIndex("dbo.Appointments", new[] { "StatusId" });
            DropIndex("dbo.Appointments", new[] { "EnterpriseId" });
            DropTable("dbo.AppointmentStatus");
            DropTable("dbo.Appointments");
        }
    }
}
