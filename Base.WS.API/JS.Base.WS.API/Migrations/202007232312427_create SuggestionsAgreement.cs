namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createSuggestionsAgreement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuggestionsAgreements",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(nullable: false),
                        StatusId = c.Int(nullable: false),
                        AreaIdA = c.Int(nullable: false),
                        DateA = c.String(),
                        CommentA = c.String(),
                        TeacherSignatureA = c.String(),
                        CompanionSignatureA = c.String(),
                        DistrictTechnicianSignatureA = c.String(),
                        AreaIdB = c.Int(nullable: false),
                        DateB = c.String(),
                        CommentB = c.String(),
                        TeacherSignatureB = c.String(),
                        CompanionSignatureB = c.String(),
                        DistrictTechnicianSignatureB = c.String(),
                        AreaIdC = c.Int(nullable: false),
                        DateC = c.String(),
                        CommentC = c.String(),
                        TeacherSignaturC = c.String(),
                        CompanionSignatureC = c.String(),
                        DistrictTechnicianSignatureC = c.String(),
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
            DropForeignKey("dbo.SuggestionsAgreements", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.SuggestionsAgreements", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.SuggestionsAgreements", "AreaIdC", "dbo.Areas");
            DropForeignKey("dbo.SuggestionsAgreements", "AreaIdB", "dbo.Areas");
            DropForeignKey("dbo.SuggestionsAgreements", "AreaIdA", "dbo.Areas");
            DropIndex("dbo.SuggestionsAgreements", new[] { "AreaIdC" });
            DropIndex("dbo.SuggestionsAgreements", new[] { "AreaIdB" });
            DropIndex("dbo.SuggestionsAgreements", new[] { "AreaIdA" });
            DropIndex("dbo.SuggestionsAgreements", new[] { "StatusId" });
            DropIndex("dbo.SuggestionsAgreements", new[] { "RequestId" });
            DropTable("dbo.SuggestionsAgreements");
        }
    }
}
