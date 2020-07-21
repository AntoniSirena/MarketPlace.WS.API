namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createClassroomClimate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassroomClimateDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClassroomClimateId = c.Long(nullable: false),
                        VariableDetailId = c.Long(nullable: false),
                        AreaIdA = c.Int(nullable: false),
                        IndicadorIdA = c.Int(nullable: false),
                        AreaIdB = c.Int(nullable: false),
                        IndicadorIdB = c.Int(nullable: false),
                        AreaIdC = c.Int(nullable: false),
                        IndicadorIdC = c.Int(nullable: false),
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
                .ForeignKey("dbo.ClassroomClimates", t => t.ClassroomClimateId, cascadeDelete: true)
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdA, cascadeDelete: false)
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdB, cascadeDelete: false)
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdC, cascadeDelete: false)
                .ForeignKey("dbo.VariableDetails", t => t.VariableDetailId, cascadeDelete: true)
                .Index(t => t.ClassroomClimateId)
                .Index(t => t.VariableDetailId)
                .Index(t => t.AreaIdA)
                .Index(t => t.IndicadorIdA)
                .Index(t => t.AreaIdB)
                .Index(t => t.IndicadorIdB)
                .Index(t => t.AreaIdC)
                .Index(t => t.IndicadorIdC);
            
            CreateTable(
                "dbo.ClassroomClimates",
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
            DropForeignKey("dbo.ClassroomClimateDetails", "VariableDetailId", "dbo.VariableDetails");
            DropForeignKey("dbo.ClassroomClimateDetails", "IndicadorIdC", "dbo.Indicators");
            DropForeignKey("dbo.ClassroomClimateDetails", "IndicadorIdB", "dbo.Indicators");
            DropForeignKey("dbo.ClassroomClimateDetails", "IndicadorIdA", "dbo.Indicators");
            DropForeignKey("dbo.ClassroomClimates", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.ClassroomClimates", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.ClassroomClimateDetails", "ClassroomClimateId", "dbo.ClassroomClimates");
            DropForeignKey("dbo.ClassroomClimateDetails", "AreaIdC", "dbo.Areas");
            DropForeignKey("dbo.ClassroomClimateDetails", "AreaIdB", "dbo.Areas");
            DropForeignKey("dbo.ClassroomClimateDetails", "AreaIdA", "dbo.Areas");
            DropIndex("dbo.ClassroomClimates", new[] { "StatusId" });
            DropIndex("dbo.ClassroomClimates", new[] { "RequestId" });
            DropIndex("dbo.ClassroomClimateDetails", new[] { "IndicadorIdC" });
            DropIndex("dbo.ClassroomClimateDetails", new[] { "AreaIdC" });
            DropIndex("dbo.ClassroomClimateDetails", new[] { "IndicadorIdB" });
            DropIndex("dbo.ClassroomClimateDetails", new[] { "AreaIdB" });
            DropIndex("dbo.ClassroomClimateDetails", new[] { "IndicadorIdA" });
            DropIndex("dbo.ClassroomClimateDetails", new[] { "AreaIdA" });
            DropIndex("dbo.ClassroomClimateDetails", new[] { "VariableDetailId" });
            DropIndex("dbo.ClassroomClimateDetails", new[] { "ClassroomClimateId" });
            DropTable("dbo.ClassroomClimates");
            DropTable("dbo.ClassroomClimateDetails");
        }
    }
}
