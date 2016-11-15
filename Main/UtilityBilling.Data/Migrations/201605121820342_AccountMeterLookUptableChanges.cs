namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountMeterLookUptableChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ubill.AccountMeterLookup", "UtilityAccountId", "ubill.UtilityAccounts");
            DropIndex("ubill.AccountMeterLookup", new[] { "UtilityAccountId" });
            AlterColumn("ubill.AccountMeterLookup", "CcbAcctId", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("ubill.AccountMeterLookup", "CcbBadgeNbr", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("ubill.AccountMeterLookup", "EtlInsProcessName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("ubill.AccountMeterLookup", "CcbMtrId");
            DropColumn("ubill.AccountMeterLookup", "CcbSaId");
            DropColumn("ubill.AccountMeterLookup", "EtlUpdRunNumber");
            DropColumn("ubill.AccountMeterLookup", "EtlUpdTimestamp");
            DropColumn("ubill.AccountMeterLookup", "EtlUpdProcessName");
        }
        
        public override void Down()
        {
            AddColumn("ubill.AccountMeterLookup", "EtlUpdProcessName", c => c.String(maxLength: 50));
            AddColumn("ubill.AccountMeterLookup", "EtlUpdTimestamp", c => c.DateTime(nullable: false));
            AddColumn("ubill.AccountMeterLookup", "EtlUpdRunNumber", c => c.Int(nullable: false));
            AddColumn("ubill.AccountMeterLookup", "CcbSaId", c => c.String(maxLength: 10));
            AddColumn("ubill.AccountMeterLookup", "CcbMtrId", c => c.String(maxLength: 10));
            AlterColumn("ubill.AccountMeterLookup", "EtlInsProcessName", c => c.String(maxLength: 50));
            AlterColumn("ubill.AccountMeterLookup", "CcbBadgeNbr", c => c.String(maxLength: 30));
            AlterColumn("ubill.AccountMeterLookup", "CcbAcctId", c => c.String(maxLength: 10));
            CreateIndex("ubill.AccountMeterLookup", "UtilityAccountId");
            AddForeignKey("ubill.AccountMeterLookup", "UtilityAccountId", "ubill.UtilityAccounts", "UtilityAccountId", cascadeDelete: true);
        }
    }
}
