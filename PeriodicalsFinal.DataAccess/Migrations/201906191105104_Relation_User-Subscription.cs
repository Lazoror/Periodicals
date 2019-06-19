namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relation_UserSubscription : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Subscription", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Subscription", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Subscription", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Subscription", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Subscription", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Subscription", "ApplicationUserId");
        }
    }
}
