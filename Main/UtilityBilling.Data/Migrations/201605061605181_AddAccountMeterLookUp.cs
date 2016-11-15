namespace UtilityBilling.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountMeterLookUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ubill.AccountMeterLookup",
                c => new
                    {
                        AccountMeterLookupId = c.Int(nullable: false, identity: true),
                        CcbAcctId = c.String(maxLength: 10),
                        CcbBadgeNbr = c.String(maxLength: 30),
                        UtilityAccountId = c.Int(nullable: false),
                        CcbMtrId = c.String(maxLength: 10),
                        CcbSaId = c.String(maxLength: 10),
                        EtlInsRunNumber = c.Int(nullable: false),
                        EtlInsTimestamp = c.DateTime(nullable: false),
                        EtlInsProcessName = c.String(maxLength: 50),
                        EtlUpdRunNumber = c.Int(nullable: false),
                        EtlUpdTimestamp = c.DateTime(nullable: false),
                        EtlUpdProcessName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.AccountMeterLookupId)
                .ForeignKey("ubill.UtilityAccounts", t => t.UtilityAccountId, cascadeDelete: true)
                .Index(t => t.UtilityAccountId);
        }
        
        public override void Down()
        {
            DropForeignKey("ubill.AccountMeterLookup", "UtilityAccountId", "ubill.UtilityAccounts");
            DropIndex("ubill.AccountMeterLookup", new[] { "UtilityAccountId" });
            DropTable("ubill.AccountMeterLookup");
        }
    }
}
