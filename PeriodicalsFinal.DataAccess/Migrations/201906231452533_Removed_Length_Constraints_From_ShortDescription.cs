namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_Length_Constraints_From_ShortDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Article", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Article", "ShortDescription", c => c.String(maxLength: 30));
        }
    }
}
