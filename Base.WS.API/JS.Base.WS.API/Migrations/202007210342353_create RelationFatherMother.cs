namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createRelationFatherMother : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReflectionPracticeDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ReflectionPracticeId = c.Long(nullable: false),
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
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdA, cascadeDelete: false)
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdB, cascadeDelete: false)
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdC, cascadeDelete: false)
                .ForeignKey("dbo.ReflectionPractices", t => t.ReflectionPracticeId, cascadeDelete: true)
                .ForeignKey("dbo.VariableDetails", t => t.VariableDetailId, cascadeDelete: true)
                .Index(t => t.ReflectionPracticeId)
                .Index(t => t.VariableDetailId)
                .Index(t => t.AreaIdA)
                .Index(t => t.IndicadorIdA)
                .Index(t => t.AreaIdB)
                .Index(t => t.IndicadorIdB)
                .Index(t => t.AreaIdC)
                .Index(t => t.IndicadorIdC);
            
            CreateTable(
                "dbo.ReflectionPractices",
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
                "dbo.RelationFatherMotherDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RelationFatherMotherId = c.Long(nullable: false),
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
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdA, cascadeDelete: false)
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdB, cascadeDelete: false)
                .ForeignKey("dbo.Indicators", t => t.IndicadorIdC, cascadeDelete: false)
                .ForeignKey("dbo.RelationFatherMothers", t => t.RelationFatherMotherId, cascadeDelete: true)
                .ForeignKey("dbo.VariableDetails", t => t.VariableDetailId, cascadeDelete: true)
                .Index(t => t.RelationFatherMotherId)
                .Index(t => t.VariableDetailId)
                .Index(t => t.AreaIdA)
                .Index(t => t.IndicadorIdA)
                .Index(t => t.AreaIdB)
                .Index(t => t.IndicadorIdB)
                .Index(t => t.AreaIdC)
                .Index(t => t.IndicadorIdC);
            
            CreateTable(
                "dbo.RelationFatherMothers",
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
            DropForeignKey("dbo.RelationFatherMotherDetails", "VariableDetailId", "dbo.VariableDetails");
            DropForeignKey("dbo.RelationFatherMothers", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.RelationFatherMothers", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.RelationFatherMotherDetails", "RelationFatherMotherId", "dbo.RelationFatherMothers");
            DropForeignKey("dbo.RelationFatherMotherDetails", "IndicadorIdC", "dbo.Indicators");
            DropForeignKey("dbo.RelationFatherMotherDetails", "IndicadorIdB", "dbo.Indicators");
            DropForeignKey("dbo.RelationFatherMotherDetails", "IndicadorIdA", "dbo.Indicators");
            DropForeignKey("dbo.RelationFatherMotherDetails", "AreaIdC", "dbo.Areas");
            DropForeignKey("dbo.RelationFatherMotherDetails", "AreaIdB", "dbo.Areas");
            DropForeignKey("dbo.RelationFatherMotherDetails", "AreaIdA", "dbo.Areas");
            DropForeignKey("dbo.ReflectionPracticeDetails", "VariableDetailId", "dbo.VariableDetails");
            DropForeignKey("dbo.ReflectionPractices", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.ReflectionPractices", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.ReflectionPracticeDetails", "ReflectionPracticeId", "dbo.ReflectionPractices");
            DropForeignKey("dbo.ReflectionPracticeDetails", "IndicadorIdC", "dbo.Indicators");
            DropForeignKey("dbo.ReflectionPracticeDetails", "IndicadorIdB", "dbo.Indicators");
            DropForeignKey("dbo.ReflectionPracticeDetails", "IndicadorIdA", "dbo.Indicators");
            DropForeignKey("dbo.ReflectionPracticeDetails", "AreaIdC", "dbo.Areas");
            DropForeignKey("dbo.ReflectionPracticeDetails", "AreaIdB", "dbo.Areas");
            DropForeignKey("dbo.ReflectionPracticeDetails", "AreaIdA", "dbo.Areas");
            DropIndex("dbo.RelationFatherMothers", new[] { "StatusId" });
            DropIndex("dbo.RelationFatherMothers", new[] { "RequestId" });
            DropIndex("dbo.RelationFatherMotherDetails", new[] { "IndicadorIdC" });
            DropIndex("dbo.RelationFatherMotherDetails", new[] { "AreaIdC" });
            DropIndex("dbo.RelationFatherMotherDetails", new[] { "IndicadorIdB" });
            DropIndex("dbo.RelationFatherMotherDetails", new[] { "AreaIdB" });
            DropIndex("dbo.RelationFatherMotherDetails", new[] { "IndicadorIdA" });
            DropIndex("dbo.RelationFatherMotherDetails", new[] { "AreaIdA" });
            DropIndex("dbo.RelationFatherMotherDetails", new[] { "VariableDetailId" });
            DropIndex("dbo.RelationFatherMotherDetails", new[] { "RelationFatherMotherId" });
            DropIndex("dbo.ReflectionPractices", new[] { "StatusId" });
            DropIndex("dbo.ReflectionPractices", new[] { "RequestId" });
            DropIndex("dbo.ReflectionPracticeDetails", new[] { "IndicadorIdC" });
            DropIndex("dbo.ReflectionPracticeDetails", new[] { "AreaIdC" });
            DropIndex("dbo.ReflectionPracticeDetails", new[] { "IndicadorIdB" });
            DropIndex("dbo.ReflectionPracticeDetails", new[] { "AreaIdB" });
            DropIndex("dbo.ReflectionPracticeDetails", new[] { "IndicadorIdA" });
            DropIndex("dbo.ReflectionPracticeDetails", new[] { "AreaIdA" });
            DropIndex("dbo.ReflectionPracticeDetails", new[] { "VariableDetailId" });
            DropIndex("dbo.ReflectionPracticeDetails", new[] { "ReflectionPracticeId" });
            DropTable("dbo.RelationFatherMothers");
            DropTable("dbo.RelationFatherMotherDetails");
            DropTable("dbo.ReflectionPractices");
            DropTable("dbo.ReflectionPracticeDetails");
        }
    }
}
