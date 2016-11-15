namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddETLAuditColsToDocs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ubill.MeterServiceAgreement", "AccountId", "ubill.UtilityAccounts");
            DropIndex("ubill.MeterServiceAgreement", new[] { "AccountId" });
            AddColumn("ubill.DocumentDetails", "EtlInsRunNumber", c => c.Int(nullable: false));
            AddColumn("ubill.DocumentDetails", "EtlInsTimestamp", c => c.DateTime(nullable: false));
            AddColumn("ubill.DocumentDetails", "EtlInsProcessName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("ubill.DocumentHeaders", "EtlInsRunNumber", c => c.Int(nullable: false));
            AddColumn("ubill.DocumentHeaders", "EtlInsTimestamp", c => c.DateTime(nullable: false));
            AddColumn("ubill.DocumentHeaders", "EtlInsProcessName", c => c.String(nullable: false, maxLength: 50));
            DropTable("ubill.MeterServiceAgreement");
        }
        
        public override void Down()
        {
            CreateTable(
                "ubill.MeterServiceAgreement",
                c => new
                    {
                        ccb_mtr_id = c.String(nullable: false, maxLength: 50),
                        ccb_sa_id = c.String(nullable: false, maxLength: 50),
                        ccb_badge_nbr = c.String(maxLength: 50),
                        AccountId = c.Int(),
                        ServiceAgreementStatus = c.String(maxLength: 50),
                        ServiceAgreementLastActiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        MeterId = c.Int(),
                        CcbMeterNumber = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ccb_mtr_id, t.ccb_sa_id });
            
            DropColumn("ubill.DocumentHeaders", "EtlInsProcessName");
            DropColumn("ubill.DocumentHeaders", "EtlInsTimestamp");
            DropColumn("ubill.DocumentHeaders", "EtlInsRunNumber");
            DropColumn("ubill.DocumentDetails", "EtlInsProcessName");
            DropColumn("ubill.DocumentDetails", "EtlInsTimestamp");
            DropColumn("ubill.DocumentDetails", "EtlInsRunNumber");
            CreateIndex("ubill.MeterServiceAgreement", "AccountId");
            AddForeignKey("ubill.MeterServiceAgreement", "AccountId", "ubill.UtilityAccounts", "UtilityAccountId");
        }
    }
}
