namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCenterEducative : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EducativeCenters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        DistrictId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EducativeCenters", "DistrictId", "dbo.Districts");
            DropIndex("dbo.EducativeCenters", new[] { "DistrictId" });
            DropTable("dbo.EducativeCenters");
        }
    }
}
