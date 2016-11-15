namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleaningTask556 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ubill.DocumentHeaders", "UtilityAccountId", "ubill.UtilityAccounts");
            DropIndex("ubill.DocumentHeaders", new[] { "UtilityAccountId" });
            RenameColumn(table: "ubill.DocumentHeaders", name: "StatusId", newName: "Status_StatusID");
            RenameIndex(table: "ubill.DocumentHeaders", name: "IX_StatusId", newName: "IX_Status_StatusID");
            AddColumn("ubill.DocumentHeaders", "DocumentIssueDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("ubill.DocumentHeaders", "PublishedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("ubill.DocumentHeaders", "ETLDocumentLoadKey", c => c.Int(nullable: false));
            AddColumn("ubill.DocumentHeaders", "DocumentStatusId", c => c.Int(nullable: false));
            AlterColumn("ubill.DocumentHeaders", "UtilityAccountId", c => c.Int(nullable: false));
            CreateIndex("ubill.DocumentHeaders", "UtilityAccountId");
            AddForeignKey("ubill.DocumentHeaders", "UtilityAccountId", "ubill.UtilityAccounts", "UtilityAccountId", cascadeDelete: true);
            DropColumn("ubill.DocumentDetails", "DocumentTypeId");
            DropColumn("ubill.DocumentHeaders", "ccb_bill_id");
            DropColumn("ubill.DocumentHeaders", "IssueDate");
            DropColumn("ubill.DocumentHeaders", "ReleaseDate");
            DropColumn("ubill.DocumentHeaders", "DocumentId");
            DropColumn("ubill.DocumentHeaders", "DocumentKey");
        }
        
        public override void Down()
        {
            AddColumn("ubill.DocumentHeaders", "DocumentKey", c => c.Int());
            AddColumn("ubill.DocumentHeaders", "DocumentId", c => c.Int());
            AddColumn("ubill.DocumentHeaders", "ReleaseDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("ubill.DocumentHeaders", "IssueDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("ubill.DocumentHeaders", "ccb_bill_id", c => c.String(maxLength: 50));
            AddColumn("ubill.DocumentDetails", "DocumentTypeId", c => c.Int());
            DropForeignKey("ubill.DocumentHeaders", "UtilityAccountId", "ubill.UtilityAccounts");
            DropIndex("ubill.DocumentHeaders", new[] { "UtilityAccountId" });
            AlterColumn("ubill.DocumentHeaders", "UtilityAccountId", c => c.Int());
            DropColumn("ubill.DocumentHeaders", "DocumentStatusId");
            DropColumn("ubill.DocumentHeaders", "ETLDocumentLoadKey");
            DropColumn("ubill.DocumentHeaders", "PublishedDate");
            DropColumn("ubill.DocumentHeaders", "DocumentIssueDate");
            RenameIndex(table: "ubill.DocumentHeaders", name: "IX_Status_StatusID", newName: "IX_StatusId");
            RenameColumn(table: "ubill.DocumentHeaders", name: "Status_StatusID", newName: "StatusId");
            CreateIndex("ubill.DocumentHeaders", "UtilityAccountId");
            AddForeignKey("ubill.DocumentHeaders", "UtilityAccountId", "ubill.UtilityAccounts", "UtilityAccountId");
        }
    }
}
