namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Relation_ApplicationUserSubscription2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Subscription");
            AlterColumn("dbo.Subscription", "SubscriptionId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Subscription", "SubscriptionId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Subscription");
            AlterColumn("dbo.Subscription", "SubscriptionId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Subscription", "SubscriptionId");
        }
    }
}
