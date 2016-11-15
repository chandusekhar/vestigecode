namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAuditRecords : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Audit.AuditRecords", "AuditEventTypeId", "ref.vwDLU_AUDIT_EVENT_WSS_ACCT");
            DropForeignKey("Audit.AuditRecords", "AuditSubjectId", "ref.vwDLU_AUDIT_SUBJECT");
            DropIndex("Audit.AuditRecords", new[] { "AuditSubjectId" });
            DropIndex("Audit.AuditRecords", new[] { "AuditEventTypeId" });
            AddColumn("Audit.AuditRecords", "AuditSubjectCode", c => c.String(nullable: false, maxLength: 25));
            AddColumn("Audit.AuditRecords", "AuditEventWssAccountCode", c => c.String(nullable: false, maxLength: 25));
            DropColumn("Audit.AuditRecords", "AuditSubjectId");
            DropColumn("Audit.AuditRecords", "AuditEventTypeId");
        }
        
        public override void Down()
        {
            AddColumn("Audit.AuditRecords", "AuditEventTypeId", c => c.Int(nullable: false));
            AddColumn("Audit.AuditRecords", "AuditSubjectId", c => c.Int(nullable: false));
            DropColumn("Audit.AuditRecords", "AuditEventWssAccountCode");
            DropColumn("Audit.AuditRecords", "AuditSubjectCode");
            CreateIndex("Audit.AuditRecords", "AuditEventTypeId");
            CreateIndex("Audit.AuditRecords", "AuditSubjectId");
            AddForeignKey("Audit.AuditRecords", "AuditSubjectId", "ref.vwDLU_AUDIT_SUBJECT", "AuditSubjectId", cascadeDelete: true);
            AddForeignKey("Audit.AuditRecords", "AuditEventTypeId", "ref.vwDLU_AUDIT_EVENT_WSS_ACCT", "AuditEventWssAccountId", cascadeDelete: true);
        }
    }
}
