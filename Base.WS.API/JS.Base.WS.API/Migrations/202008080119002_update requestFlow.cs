namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaterequestFlow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestFlowAIApproveds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(nullable: false),
                        StatusId = c.Int(nullable: false),
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
                .ForeignKey("dbo.AccompanyingInstrumentRequests", t => t.RequestId, cascadeDelete: true)
                .ForeignKey("dbo.RequestStatus", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.RequestId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.RequestFlowAICancelads",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(nullable: false),
                        StatusId = c.Int(nullable: false),
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
                .ForeignKey("dbo.AccompanyingInstrumentRequests", t => t.RequestId, cascadeDelete: true)
                .ForeignKey("dbo.RequestStatus", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.RequestId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.RequestFlowAIInObservations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(nullable: false),
                        StatusId = c.Int(nullable: false),
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
                .ForeignKey("dbo.AccompanyingInstrumentRequests", t => t.RequestId, cascadeDelete: true)
                .ForeignKey("dbo.RequestStatus", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.RequestId)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestFlowAIInObservations", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.RequestFlowAIInObservations", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.RequestFlowAICancelads", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.RequestFlowAICancelads", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.RequestFlowAIApproveds", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.RequestFlowAIApproveds", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropIndex("dbo.RequestFlowAIInObservations", new[] { "StatusId" });
            DropIndex("dbo.RequestFlowAIInObservations", new[] { "RequestId" });
            DropIndex("dbo.RequestFlowAICancelads", new[] { "StatusId" });
            DropIndex("dbo.RequestFlowAICancelads", new[] { "RequestId" });
            DropIndex("dbo.RequestFlowAIApproveds", new[] { "StatusId" });
            DropIndex("dbo.RequestFlowAIApproveds", new[] { "RequestId" });
            DropTable("dbo.RequestFlowAIInObservations");
            DropTable("dbo.RequestFlowAICancelads");
            DropTable("dbo.RequestFlowAIApproveds");
        }
    }
}
