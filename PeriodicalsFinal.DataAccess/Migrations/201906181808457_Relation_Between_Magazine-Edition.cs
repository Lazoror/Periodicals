namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relation_Between_MagazineEdition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Edition", "MagazineId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Edition", "MagazineId");
            AddForeignKey("dbo.Edition", "MagazineId", "dbo.Magazine", "MagazineId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Edition", "MagazineId", "dbo.Magazine");
            DropIndex("dbo.Edition", new[] { "MagazineId" });
            DropColumn("dbo.Edition", "MagazineId");
        }
    }
}
