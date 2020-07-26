namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateIdentificationDatas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentificationDatas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(nullable: false),
                        RegionalId = c.Int(nullable: false),
                        DistritId = c.Int(nullable: false),
                        CenterId = c.Int(nullable: false),
                        TandaId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                        DocentId = c.Long(nullable: false),
                        CompanionId = c.Long(nullable: false),
                        VisitIdA = c.Int(),
                        VisitDateA = c.DateTime(),
                        QuantityChildrenA = c.Int(),
                        QuantityGirlsA = c.Int(),
                        ExpectedTimeA = c.Int(),
                        RealTimeA = c.Int(),
                        VisitIdB = c.Int(),
                        VisitDateB = c.DateTime(),
                        QuantityChildrenB = c.Int(),
                        QuantityGirlsB = c.Int(),
                        ExpectedTimeB = c.Int(),
                        RealTimeB = c.Int(),
                        VisitIdC = c.Int(),
                        VisitDateC = c.DateTime(),
                        QuantityChildrenC = c.Int(),
                        QuantityGirlsC = c.Int(),
                        ExpectedTimeC = c.Int(),
                        RealTimeC = c.Int(),
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
                .ForeignKey("dbo.Users", t => t.CompanionId, cascadeDelete: true)
                .ForeignKey("dbo.Districts", t => t.DistritId, cascadeDelete: true)
                .ForeignKey("dbo.Docents", t => t.DocentId, cascadeDelete: true)
                .ForeignKey("dbo.EducativeCenters", t => t.CenterId, cascadeDelete: false)
                .ForeignKey("dbo.Grades", t => t.GradeId, cascadeDelete: true)
                .ForeignKey("dbo.Regionals", t => t.RegionalId, cascadeDelete: false)
                .ForeignKey("dbo.AccompanyingInstrumentRequests", t => t.RequestId, cascadeDelete: true)
                .ForeignKey("dbo.Tandas", t => t.TandaId, cascadeDelete: true)
                .ForeignKey("dbo.Visits", t => t.VisitIdA)
                .ForeignKey("dbo.Visits", t => t.VisitIdB)
                .ForeignKey("dbo.Visits", t => t.VisitIdC)
                .Index(t => t.RequestId)
                .Index(t => t.RegionalId)
                .Index(t => t.DistritId)
                .Index(t => t.CenterId)
                .Index(t => t.TandaId)
                .Index(t => t.GradeId)
                .Index(t => t.DocentId)
                .Index(t => t.CompanionId)
                .Index(t => t.VisitIdA)
                .Index(t => t.VisitIdB)
                .Index(t => t.VisitIdC);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentificationDatas", "VisitIdC", "dbo.Visits");
            DropForeignKey("dbo.IdentificationDatas", "VisitIdB", "dbo.Visits");
            DropForeignKey("dbo.IdentificationDatas", "VisitIdA", "dbo.Visits");
            DropForeignKey("dbo.IdentificationDatas", "TandaId", "dbo.Tandas");
            DropForeignKey("dbo.IdentificationDatas", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.IdentificationDatas", "RegionalId", "dbo.Regionals");
            DropForeignKey("dbo.IdentificationDatas", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.IdentificationDatas", "CenterId", "dbo.EducativeCenters");
            DropForeignKey("dbo.IdentificationDatas", "DocentId", "dbo.Docents");
            DropForeignKey("dbo.IdentificationDatas", "DistritId", "dbo.Districts");
            DropForeignKey("dbo.IdentificationDatas", "CompanionId", "dbo.Users");
            DropIndex("dbo.IdentificationDatas", new[] { "VisitIdC" });
            DropIndex("dbo.IdentificationDatas", new[] { "VisitIdB" });
            DropIndex("dbo.IdentificationDatas", new[] { "VisitIdA" });
            DropIndex("dbo.IdentificationDatas", new[] { "CompanionId" });
            DropIndex("dbo.IdentificationDatas", new[] { "DocentId" });
            DropIndex("dbo.IdentificationDatas", new[] { "GradeId" });
            DropIndex("dbo.IdentificationDatas", new[] { "TandaId" });
            DropIndex("dbo.IdentificationDatas", new[] { "CenterId" });
            DropIndex("dbo.IdentificationDatas", new[] { "DistritId" });
            DropIndex("dbo.IdentificationDatas", new[] { "RegionalId" });
            DropIndex("dbo.IdentificationDatas", new[] { "RequestId" });
            DropTable("dbo.IdentificationDatas");
        }
    }
}
