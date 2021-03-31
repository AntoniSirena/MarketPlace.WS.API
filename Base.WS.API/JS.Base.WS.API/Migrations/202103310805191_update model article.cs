namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodelarticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleCategories", "DescriptionFormatted", c => c.String());
            AddColumn("dbo.ArticleSubCategories", "DescriptionFormatted", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleSubCategories", "DescriptionFormatted");
            DropColumn("dbo.ArticleCategories", "DescriptionFormatted");
        }
    }
}
