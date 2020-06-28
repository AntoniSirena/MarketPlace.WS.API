namespace JS.Base.WS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameproperty : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PlanningDetails", name: "VriableDetailId", newName: "VariableDetailId");
            RenameIndex(table: "dbo.PlanningDetails", name: "IX_VriableDetailId", newName: "IX_VariableDetailId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PlanningDetails", name: "IX_VariableDetailId", newName: "IX_VriableDetailId");
            RenameColumn(table: "dbo.PlanningDetails", name: "VariableDetailId", newName: "VriableDetailId");
        }
    }
}
