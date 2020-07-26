namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreationDescriptionObservationSupportProvided : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DescriptionObservationSupportProvideds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(nullable: false),
                        StatusId = c.Int(nullable: false),
                        AreaIdA = c.Int(nullable: false),
                        DateA = c.String(),
                        CommentA = c.String(),
                        AreaIdB = c.Int(nullable: false),
                        DateB = c.String(),
                        CommentB = c.String(),
                        AreaIdC = c.Int(nullable: false),
                        DateC = c.String(),
                        CommentC = c.String(),
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
                .ForeignKey("dbo.Areas", t => t.AreaIdA, cascadeDelete: false)
                .ForeignKey("dbo.Areas", t => t.AreaIdB, cascadeDelete: false)
                .ForeignKey("dbo.Areas", t => t.AreaIdC, cascadeDelete: false)
                .ForeignKey("dbo.AccompanyingInstrumentRequests", t => t.RequestId, cascadeDelete: true)
                .ForeignKey("dbo.RequestStatus", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.RequestId)
                .Index(t => t.StatusId)
                .Index(t => t.AreaIdA)
                .Index(t => t.AreaIdB)
                .Index(t => t.AreaIdC);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DescriptionObservationSupportProvideds", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.DescriptionObservationSupportProvideds", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.DescriptionObservationSupportProvideds", "AreaIdC", "dbo.Areas");
            DropForeignKey("dbo.DescriptionObservationSupportProvideds", "AreaIdB", "dbo.Areas");
            DropForeignKey("dbo.DescriptionObservationSupportProvideds", "AreaIdA", "dbo.Areas");
            DropIndex("dbo.DescriptionObservationSupportProvideds", new[] { "AreaIdC" });
            DropIndex("dbo.DescriptionObservationSupportProvideds", new[] { "AreaIdB" });
            DropIndex("dbo.DescriptionObservationSupportProvideds", new[] { "AreaIdA" });
            DropIndex("dbo.DescriptionObservationSupportProvideds", new[] { "StatusId" });
            DropIndex("dbo.DescriptionObservationSupportProvideds", new[] { "RequestId" });
            DropTable("dbo.DescriptionObservationSupportProvideds");
        }
    }
}
