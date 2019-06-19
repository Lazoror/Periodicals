namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Relation_ApplicationUserSubscription : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Subscription", name: "UserId", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Subscription", name: "IX_UserId", newName: "IX_ApplicationUserId");
            DropPrimaryKey("dbo.Subscription");
            AlterColumn("dbo.Subscription", "SubscriptionId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Subscription", "SubscriptionId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Subscription");
            AlterColumn("dbo.Subscription", "SubscriptionId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Subscription", "SubscriptionId");
            RenameIndex(table: "dbo.Subscription", name: "IX_ApplicationUserId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Subscription", name: "ApplicationUserId", newName: "UserId");
        }
    }
}
