namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Relation_OnetoMany_Article_Edition2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Article", "EditionId", "dbo.Edition");
            AddForeignKey("dbo.Article", "EditionId", "dbo.Edition", "EditionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "EditionId", "dbo.Edition");
            AddForeignKey("dbo.Article", "EditionId", "dbo.Edition", "EditionId", cascadeDelete: true);
        }
    }
}
