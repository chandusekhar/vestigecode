namespace UtilityBilling.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitalCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ubill.DocumentDetails",
                c => new
                {
                    DocumentDetailId = c.Int(nullable: false, identity: true),
                    DocumentHeaderId = c.Int(nullable: false),
                    DocumentPdf = c.Binary(),
                    DocumentTypeId = c.Int(),
                })
                .PrimaryKey(t => t.DocumentDetailId)
                .ForeignKey("ubill.DocumentHeaders", t => t.DocumentHeaderId)
                .Index(t => t.DocumentHeaderId);

            CreateTable(
                "ubill.DocumentHeaders",
                c => new
                {
                    DocumentHeaderId = c.Int(nullable: false, identity: true),
                    ccb_bill_id = c.String(maxLength: 50),
                    UtilityAccountId = c.Int(),
                    DocumentType = c.Int(),
                    IssueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    ReleaseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    DocumentId = c.String(maxLength: 50),
                    DocumentKey = c.String(maxLength: 50),
                    StatusId = c.Int(),
                    BillDueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    BillAmmountDue = c.Decimal(storeType: "money"),
                    BillInterceptCode = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.DocumentHeaderId)
                .ForeignKey("ubill.Status", t => t.StatusId)
                .ForeignKey("ubill.UtilityAccounts", t => t.UtilityAccountId)
                .Index(t => t.UtilityAccountId)
                .Index(t => t.StatusId);

            CreateTable(
                "ubill.Status",
                c => new
                {
                    StatusID = c.Int(nullable: false, identity: true),
                    StatusDomain = c.String(maxLength: 50),
                    StatusName = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.StatusID);

            CreateTable(
                "ubill.UtilityAccounts",
                c => new
                {
                    UtilityAccountId = c.Int(nullable: false, identity: true),
                    PrimaryAccountHolderName = c.String(maxLength: 100),
                    Balance = c.Decimal(storeType: "money"),
                    BalanceDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    LastBillDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    ccb_acct_id = c.String(maxLength: 20),
                    StatusId = c.Int(),
                })
                .PrimaryKey(t => t.UtilityAccountId)
                .ForeignKey("ubill.Status", t => t.StatusId)
                .Index(t => t.StatusId);

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
                .PrimaryKey(t => new { t.ccb_mtr_id, t.ccb_sa_id })
                .ForeignKey("ubill.UtilityAccounts", t => t.AccountId)
                .Index(t => t.AccountId);
        }

        public override void Down()
        {
            DropForeignKey("ubill.UtilityAccounts", "StatusId", "ubill.Status");
            DropForeignKey("ubill.MeterServiceAgreement", "AccountId", "ubill.UtilityAccounts");
            DropForeignKey("ubill.DocumentHeaders", "UtilityAccountId", "ubill.UtilityAccounts");
            DropForeignKey("ubill.DocumentHeaders", "StatusId", "ubill.Status");
            DropForeignKey("ubill.DocumentDetails", "DocumentHeaderId", "ubill.DocumentHeaders");
            DropIndex("ubill.MeterServiceAgreement", new[] { "AccountId" });
            DropIndex("ubill.UtilityAccounts", new[] { "StatusId" });
            DropIndex("ubill.DocumentHeaders", new[] { "StatusId" });
            DropIndex("ubill.DocumentHeaders", new[] { "UtilityAccountId" });
            DropIndex("ubill.DocumentDetails", new[] { "DocumentHeaderId" });
            DropTable("ubill.MeterServiceAgreement");
            DropTable("ubill.UtilityAccounts");
            DropTable("ubill.Status");
            DropTable("ubill.DocumentHeaders");
            DropTable("ubill.DocumentDetails");
        }
    }
}