namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Relation_OnetoMany_Article_Edition : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Edition", "ArticleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Edition", "ArticleId", c => c.Guid(nullable: false));
        }
    }
}
