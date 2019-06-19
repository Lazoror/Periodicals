namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Short_Description_To_Article : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "ShortDescription", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "ShortDescription");
        }
    }
}
