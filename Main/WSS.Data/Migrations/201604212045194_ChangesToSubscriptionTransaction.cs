namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToSubscriptionTransaction : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("wss.SubscriptionTransactions");
            RenameTable(name: "wss.SubscriptionTransactions", newName: "SubscriptionTransaction");
            DropColumn("wss.SubscriptionTransaction", "SubscriptionId");
            DropColumn("wss.SubscriptionTransaction", "SubscriptionDate");
            DropColumn("wss.SubscriptionTransaction", "TransactionType");
            AddColumn("wss.SubscriptionTransaction", "SubscriptionTxId", c => c.Int(nullable: false, identity: true));
            AddColumn("wss.SubscriptionTransaction", "SubscriptionTxDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("wss.SubscriptionTransaction", "SubscriptionTxTypeCode", c => c.String(nullable: false, maxLength: 25));
            AddColumn("wss.SubscriptionTransaction", "SubscriptionTxStatusId", c => c.Int(nullable: false));
            AddColumn("wss.SubscriptionTransaction", "ETL_UpdateTimestamp", c => c.DateTime());
            AddColumn("wss.SubscriptionTransaction", "ETL_UpdateProcessName", c => c.String(maxLength: 100));
            AddColumn("wss.SubscriptionTransaction", "ETL_UpdateProcessId", c => c.Int());
            AlterColumn("wss.SubscriptionTransaction", "ccb_acct_id", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("wss.SubscriptionTransaction", "SubscriptionTxId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("wss.SubscriptionTransaction");
            AlterColumn("wss.SubscriptionTransaction", "ccb_acct_id", c => c.String(maxLength: 50));
            DropColumn("wss.SubscriptionTransaction", "ETL_UpdateProcessId");
            DropColumn("wss.SubscriptionTransaction", "ETL_UpdateProcessName");
            DropColumn("wss.SubscriptionTransaction", "ETL_UpdateTimestamp");
            DropColumn("wss.SubscriptionTransaction", "SubscriptionTxStatusId");
            DropColumn("wss.SubscriptionTransaction", "SubscriptionTxTypeCode");
            DropColumn("wss.SubscriptionTransaction", "SubscriptionTxDate");
            DropColumn("wss.SubscriptionTransaction", "SubscriptionTxId");
            AddColumn("wss.SubscriptionTransaction", "TransactionType", c => c.String(maxLength: 50));
            AddColumn("wss.SubscriptionTransaction", "SubscriptionDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("wss.SubscriptionTransaction", "SubscriptionId", c => c.Int(nullable: false, identity: true));
            RenameTable(name: "wss.SubscriptionTransaction", newName: "SubscriptionTransactions");
            AddPrimaryKey("wss.SubscriptionTransactions", "SubscriptionId");
        }
    }
}
