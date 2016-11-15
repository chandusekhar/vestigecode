namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rollup1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "wss.AdditionalEmailAddress",
                c => new
                    {
                        AdditionalEmailAddressId = c.Int(nullable: false, identity: true),
                        WssAccountId = c.Int(),
                        EmailAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.AdditionalEmailAddressId)
                .ForeignKey("wss.WssAccount", t => t.WssAccountId)
                .Index(t => t.WssAccountId);
            
            CreateTable(
                "wss.WssAccount",
                c => new
                    {
                        WSSAccountId = c.Int(nullable: false, identity: true),
                        PrimaryEmailAddress = c.String(maxLength: 100),
                        StatusId = c.Int(),
                        FailedResendActivationAttempts = c.Int(),
                        AgreeTermsAndConditionsDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AuthenticationGuid = c.Guid(nullable: false),
                        SecurityQuestion = c.String(maxLength: 500),
                        SecurityQuestionAnswer = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.WSSAccountId)
                .ForeignKey("wss.Status", t => t.StatusId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "wss.Actions",
                c => new
                    {
                        ActionId = c.Int(nullable: false, identity: true),
                        ActionToken = c.String(),
                        ActionName = c.String(),
                        WssAccountId = c.Int(nullable: false),
                        AdditionalEmailAddressId = c.Int(),
                        ExpiryDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ActionId)
                .ForeignKey("wss.AdditionalEmailAddress", t => t.AdditionalEmailAddressId)
                .ForeignKey("wss.WssAccount", t => t.WssAccountId, cascadeDelete: true)
                .Index(t => t.WssAccountId)
                .Index(t => t.AdditionalEmailAddressId);
            
            CreateTable(
                "wss.LinkedUtilityAccounts",
                c => new
                    {
                        LinkedUtilityAccountId = c.Int(nullable: false, identity: true),
                        RelationshipCode = c.String(maxLength: 50),
                        NickName = c.String(maxLength: 50),
                        WssAccountId = c.Int(),
                        UtilityAccountId = c.Int(),
                        DefaultAccount = c.Boolean(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LinkedUtilityAccountId)
                .ForeignKey("wss.WssAccount", t => t.WssAccountId)
                .Index(t => t.WssAccountId);
            
            CreateTable(
                "wss.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusDomain = c.String(maxLength: 50),
                        StatusName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "Audit.AuditRecords",
                c => new
                    {
                        AuditRecordId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(),
                        WssAccountId = c.Int(),
                        date = c.DateTime(nullable: false),
                        UserId = c.String(),
                        EventId = c.Int(),
                        FieldName = c.String(),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AuditRecordId)
                .ForeignKey("Audit.EventTypes", t => t.EventId)
                .ForeignKey("Audit.SubjectTypes", t => t.SubjectId)
                .ForeignKey("wss.WssAccount", t => t.WssAccountId)
                .Index(t => t.SubjectId)
                .Index(t => t.WssAccountId)
                .Index(t => t.EventId);
            
            CreateTable(
                "Audit.EventTypes",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        EventTypeName = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "Audit.SubjectTypes",
                c => new
                    {
                        SubjectId = c.Int(nullable: false),
                        SubjectTypeName = c.String(),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "wss.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Thread = c.String(),
                        Level = c.String(),
                        Logger = c.String(),
                        Message = c.String(),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "config.Settings",
                c => new
                    {
                        SettingId = c.Int(nullable: false, identity: true),
                        SettingName = c.String(maxLength: 50),
                        Value = c.String(maxLength: 100),
                        IsEnabled = c.Boolean(),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SettingId);
            
            CreateTable(
                "wss.SiteContent",
                c => new
                    {
                        ContentId = c.Int(nullable: false, identity: true),
                        PublishedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ContentKey = c.String(maxLength: 50),
                        ExpiryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(),
                        ContentType = c.String(maxLength: 50),
                        Contents = c.String(),
                        BinaryContent = c.Binary(),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ContentId);
            
            CreateTable(
                "wss.SubscriptionTransactions",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false, identity: true),
                        SubscriptionDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ccb_acct_id = c.String(maxLength: 50),
                        TransactionType = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SubscriptionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Audit.AuditRecords", "WssAccountId", "wss.WssAccount");
            DropForeignKey("Audit.AuditRecords", "SubjectId", "Audit.SubjectTypes");
            DropForeignKey("Audit.AuditRecords", "EventId", "Audit.EventTypes");
            DropForeignKey("wss.WssAccount", "StatusId", "wss.Status");
            DropForeignKey("wss.LinkedUtilityAccounts", "WssAccountId", "wss.WssAccount");
            DropForeignKey("wss.AdditionalEmailAddress", "WssAccountId", "wss.WssAccount");
            DropForeignKey("wss.Actions", "WssAccountId", "wss.WssAccount");
            DropForeignKey("wss.Actions", "AdditionalEmailAddressId", "wss.AdditionalEmailAddress");
            DropIndex("Audit.AuditRecords", new[] { "EventId" });
            DropIndex("Audit.AuditRecords", new[] { "WssAccountId" });
            DropIndex("Audit.AuditRecords", new[] { "SubjectId" });
            DropIndex("wss.LinkedUtilityAccounts", new[] { "WssAccountId" });
            DropIndex("wss.Actions", new[] { "AdditionalEmailAddressId" });
            DropIndex("wss.Actions", new[] { "WssAccountId" });
            DropIndex("wss.WssAccount", new[] { "StatusId" });
            DropIndex("wss.AdditionalEmailAddress", new[] { "WssAccountId" });
            DropTable("wss.SubscriptionTransactions");
            DropTable("wss.SiteContent");
            DropTable("config.Settings");
            DropTable("wss.Log");
            DropTable("Audit.SubjectTypes");
            DropTable("Audit.EventTypes");
            DropTable("Audit.AuditRecords");
            DropTable("wss.Status");
            DropTable("wss.LinkedUtilityAccounts");
            DropTable("wss.Actions");
            DropTable("wss.WssAccount");
            DropTable("wss.AdditionalEmailAddress");
        }
    }
}
