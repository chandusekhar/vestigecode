namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleaningTask556 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("wss.Actions", "AdditionalEmailAddressId", "wss.AdditionalEmailAddress");
            DropForeignKey("wss.AdditionalEmailAddress", "WssAccountId", "wss.WssAccount");
            DropForeignKey("wss.LinkedUtilityAccounts", "WssAccountId", "wss.WssAccount");
            DropIndex("wss.AdditionalEmailAddress", new[] { "WssAccountId" });
            DropIndex("wss.Actions", new[] { "AdditionalEmailAddressId" });
            DropIndex("wss.LinkedUtilityAccounts", new[] { "WssAccountId" });
            AlterColumn("wss.AdditionalEmailAddress", "WssAccountId", c => c.Int(nullable: false));
            AlterColumn("wss.AdditionalEmailAddress", "EmailAddress", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("wss.Actions", "ActionToken", c => c.String(nullable: false));
            AlterColumn("wss.Actions", "ActionName", c => c.String(nullable: false));
            AlterColumn("wss.Actions", "ExpiryDateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("wss.LinkedUtilityAccounts", "WssAccountId", c => c.Int(nullable: false));
            AlterColumn("wss.LinkedUtilityAccounts", "UtilityAccountId", c => c.Int(nullable: false));
            AlterColumn("wss.LinkedUtilityAccounts", "DefaultAccount", c => c.Boolean(nullable: false));
            CreateIndex("wss.AdditionalEmailAddress", "WssAccountId");
            CreateIndex("wss.LinkedUtilityAccounts", "WssAccountId");
            AddForeignKey("wss.AdditionalEmailAddress", "WssAccountId", "wss.WssAccount", "WSSAccountId", cascadeDelete: true);
            AddForeignKey("wss.LinkedUtilityAccounts", "WssAccountId", "wss.WssAccount", "WSSAccountId", cascadeDelete: true);
            DropColumn("wss.Actions", "AdditionalEmailAddressId");
            DropColumn("wss.LinkedUtilityAccounts", "RelationshipCode");
            DropColumn("wss.LinkedUtilityAccounts", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("wss.LinkedUtilityAccounts", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("wss.LinkedUtilityAccounts", "RelationshipCode", c => c.String(maxLength: 50));
            AddColumn("wss.Actions", "AdditionalEmailAddressId", c => c.Int());
            DropForeignKey("wss.LinkedUtilityAccounts", "WssAccountId", "wss.WssAccount");
            DropForeignKey("wss.AdditionalEmailAddress", "WssAccountId", "wss.WssAccount");
            DropIndex("wss.LinkedUtilityAccounts", new[] { "WssAccountId" });
            DropIndex("wss.AdditionalEmailAddress", new[] { "WssAccountId" });
            AlterColumn("wss.LinkedUtilityAccounts", "DefaultAccount", c => c.Boolean());
            AlterColumn("wss.LinkedUtilityAccounts", "UtilityAccountId", c => c.Int());
            AlterColumn("wss.LinkedUtilityAccounts", "WssAccountId", c => c.Int());
            AlterColumn("wss.Actions", "ExpiryDateTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("wss.Actions", "ActionName", c => c.String());
            AlterColumn("wss.Actions", "ActionToken", c => c.String());
            AlterColumn("wss.AdditionalEmailAddress", "EmailAddress", c => c.String(maxLength: 100));
            AlterColumn("wss.AdditionalEmailAddress", "WssAccountId", c => c.Int());
            CreateIndex("wss.LinkedUtilityAccounts", "WssAccountId");
            CreateIndex("wss.Actions", "AdditionalEmailAddressId");
            CreateIndex("wss.AdditionalEmailAddress", "WssAccountId");
            AddForeignKey("wss.LinkedUtilityAccounts", "WssAccountId", "wss.WssAccount", "WSSAccountId");
            AddForeignKey("wss.AdditionalEmailAddress", "WssAccountId", "wss.WssAccount", "WSSAccountId");
            AddForeignKey("wss.Actions", "AdditionalEmailAddressId", "wss.AdditionalEmailAddress", "AdditionalEmailAddressId");
        }
    }
}
