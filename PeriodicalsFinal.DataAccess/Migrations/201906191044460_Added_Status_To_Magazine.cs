namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Status_To_Magazine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Magazine", "MagazineStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Magazine", "MagazineStatus");
        }
    }
}
