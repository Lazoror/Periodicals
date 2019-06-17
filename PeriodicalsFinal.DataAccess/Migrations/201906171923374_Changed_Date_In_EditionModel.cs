namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Date_In_EditionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Edition", "EditionMonth", c => c.Int(nullable: false));
            AddColumn("dbo.Edition", "EditionYear", c => c.Int(nullable: false));
            AlterColumn("dbo.Edition", "EditionImage", c => c.Binary(nullable: false));
            DropColumn("dbo.Edition", "PublishDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Edition", "PublishDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Edition", "EditionImage", c => c.Binary());
            DropColumn("dbo.Edition", "EditionYear");
            DropColumn("dbo.Edition", "EditionMonth");
        }
    }
}
