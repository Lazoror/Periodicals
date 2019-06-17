namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Model_For_Magazine_Lists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Magazine",
                c => new
                    {
                        MagazineId = c.Guid(nullable: false),
                        MagazineName = c.String(nullable: false, maxLength: 350),
                    })
                .PrimaryKey(t => t.MagazineId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Magazine");
        }
    }
}
