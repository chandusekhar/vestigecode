namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUtiltyAccountSourceCode : DbMigration
    {
        public override void Up()
        {           

            DropForeignKey("ubill.DocumentHeaders", "StatusId", "ubill.Status");
            DropForeignKey("ubill.UtilityAccounts", "StatusId", "ubill.Status");
            DropIndex("ubill.UtilityAccounts", new[] { "StatusId" });
            DropIndex("ubill.DocumentHeaders", new[] { "Status_StatusID" });
            AddColumn("ubill.UtilityAccounts", "UtilityAccountSourceCode", c => c.String(nullable: false, maxLength: 10, defaultValue: "WSS"));
            AlterColumn("ubill.UtilityAccounts", "PrimaryAccountHolderName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("ubill.UtilityAccounts", "ccb_acct_id", c => c.String(nullable: false, maxLength: 20));
            DropColumn("ubill.UtilityAccounts", "Balance");
            DropColumn("ubill.UtilityAccounts", "BalanceDate");
            DropColumn("ubill.UtilityAccounts", "LastBillDate");
            DropColumn("ubill.UtilityAccounts", "StatusId");
            DropColumn("ubill.DocumentHeaders", "Status_StatusID");
            DropTable("ubill.Status");
        }
        
        public override void Down()
        {
            CreateTable(
                "ubill.Status",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        StatusDomain = c.String(maxLength: 50),
                        StatusName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.StatusID);
            
            AddColumn("ubill.DocumentHeaders", "Status_StatusID", c => c.Int());
            AddColumn("ubill.UtilityAccounts", "StatusId", c => c.Int());
            AddColumn("ubill.UtilityAccounts", "LastBillDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("ubill.UtilityAccounts", "BalanceDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("ubill.UtilityAccounts", "Balance", c => c.Decimal(storeType: "money"));
            AlterColumn("ubill.UtilityAccounts", "ccb_acct_id", c => c.String(maxLength: 20));
            AlterColumn("ubill.UtilityAccounts", "PrimaryAccountHolderName", c => c.String(maxLength: 100));
            DropColumn("ubill.UtilityAccounts", "UtilityAccountSourceCode");                   
            CreateIndex("ubill.DocumentHeaders", "Status_StatusID");
            CreateIndex("ubill.UtilityAccounts", "StatusId");
            AddForeignKey("ubill.UtilityAccounts", "StatusId", "ubill.Status", "StatusID");
            AddForeignKey("ubill.DocumentHeaders", "StatusId", "ubill.Status", "StatusID");
        }
    }
}
