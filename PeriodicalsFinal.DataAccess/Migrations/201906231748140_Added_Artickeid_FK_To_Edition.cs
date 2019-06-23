namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Artickeid_FK_To_Edition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Edition", "ArticleId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Edition", "ArticleId");
        }
    }
}
