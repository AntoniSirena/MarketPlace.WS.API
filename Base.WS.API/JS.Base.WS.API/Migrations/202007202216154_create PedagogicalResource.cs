namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createPedagogicalResource : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PedagogicalResourceDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PedagogicalResourceId = c.Long(nullable: false),
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
                .ForeignKey("dbo.PedagogicalResources", t => t.PedagogicalResourceId, cascadeDelete: true)
                .ForeignKey("dbo.VariableDetails", t => t.VariableDetailId, cascadeDelete: true)
                .Index(t => t.PedagogicalResourceId)
                .Index(t => t.VariableDetailId)
                .Index(t => t.AreaIdA)
                .Index(t => t.IndicadorIdA)
                .Index(t => t.AreaIdB)
                .Index(t => t.IndicadorIdB)
                .Index(t => t.AreaIdC)
                .Index(t => t.IndicadorIdC);
            
            CreateTable(
                "dbo.PedagogicalResources",
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
            DropForeignKey("dbo.PedagogicalResourceDetails", "VariableDetailId", "dbo.VariableDetails");
            DropForeignKey("dbo.PedagogicalResources", "StatusId", "dbo.RequestStatus");
            DropForeignKey("dbo.PedagogicalResources", "RequestId", "dbo.AccompanyingInstrumentRequests");
            DropForeignKey("dbo.PedagogicalResourceDetails", "PedagogicalResourceId", "dbo.PedagogicalResources");
            DropForeignKey("dbo.PedagogicalResourceDetails", "IndicadorIdC", "dbo.Indicators");
            DropForeignKey("dbo.PedagogicalResourceDetails", "IndicadorIdB", "dbo.Indicators");
            DropForeignKey("dbo.PedagogicalResourceDetails", "IndicadorIdA", "dbo.Indicators");
            DropForeignKey("dbo.PedagogicalResourceDetails", "AreaIdC", "dbo.Areas");
            DropForeignKey("dbo.PedagogicalResourceDetails", "AreaIdB", "dbo.Areas");
            DropForeignKey("dbo.PedagogicalResourceDetails", "AreaIdA", "dbo.Areas");
            DropIndex("dbo.PedagogicalResources", new[] { "StatusId" });
            DropIndex("dbo.PedagogicalResources", new[] { "RequestId" });
            DropIndex("dbo.PedagogicalResourceDetails", new[] { "IndicadorIdC" });
            DropIndex("dbo.PedagogicalResourceDetails", new[] { "AreaIdC" });
            DropIndex("dbo.PedagogicalResourceDetails", new[] { "IndicadorIdB" });
            DropIndex("dbo.PedagogicalResourceDetails", new[] { "AreaIdB" });
            DropIndex("dbo.PedagogicalResourceDetails", new[] { "IndicadorIdA" });
            DropIndex("dbo.PedagogicalResourceDetails", new[] { "AreaIdA" });
            DropIndex("dbo.PedagogicalResourceDetails", new[] { "VariableDetailId" });
            DropIndex("dbo.PedagogicalResourceDetails", new[] { "PedagogicalResourceId" });
            DropTable("dbo.PedagogicalResources");
            DropTable("dbo.PedagogicalResourceDetails");
        }
    }
}
