namespace PeriodicalsFinal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relation_UserSubscription_Fix1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Subscription", new[] { "ApplicationUserId" });
            RenameColumn(table: "dbo.Subscription", name: "ApplicationUserId", newName: "Id");
            AlterColumn("dbo.Subscription", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Subscription", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Subscription", new[] { "Id" });
            AlterColumn("dbo.Subscription", "Id", c => c.String(nullable: false, maxLength: 128));
            RenameColumn(table: "dbo.Subscription", name: "Id", newName: "ApplicationUserId");
            CreateIndex("dbo.Subscription", "ApplicationUserId");
        }
    }
}
