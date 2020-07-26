namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSuggestionsAgreement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SuggestionsAgreements", "TeacherSignatureC", c => c.String());
            DropColumn("dbo.SuggestionsAgreements", "TeacherSignaturC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SuggestionsAgreements", "TeacherSignaturC", c => c.String());
            DropColumn("dbo.SuggestionsAgreements", "TeacherSignatureC");
        }
    }
}
