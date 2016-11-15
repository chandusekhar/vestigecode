namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUtilityAccountsEtlAuditColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("ubill.UtilityAccounts", "EtlInsRunNumber", c => c.Int());
            AddColumn("ubill.UtilityAccounts", "EtlInsTimestamp", c => c.DateTime());
            AddColumn("ubill.UtilityAccounts", "EtlInsProcessName", c => c.String(maxLength: 50));
            AddColumn("ubill.UtilityAccounts", "EtlUpdRunNumber", c => c.Int());
            AddColumn("ubill.UtilityAccounts", "EtlUpdTimestamp", c => c.DateTime());
            AddColumn("ubill.UtilityAccounts", "EtlUpdProcessName", c => c.String(maxLength: 50));
            AlterColumn("ubill.UtilityAccounts", "ccb_acct_id", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("ubill.UtilityAccounts", "ccb_acct_id", c => c.String(nullable: false, maxLength: 20));
            DropColumn("ubill.UtilityAccounts", "EtlUpdProcessName");
            DropColumn("ubill.UtilityAccounts", "EtlUpdTimestamp");
            DropColumn("ubill.UtilityAccounts", "EtlUpdRunNumber");
            DropColumn("ubill.UtilityAccounts", "EtlInsProcessName");
            DropColumn("ubill.UtilityAccounts", "EtlInsTimestamp");
            DropColumn("ubill.UtilityAccounts", "EtlInsRunNumber");
        }
    }
}
