namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cleaning_Task565 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Audit.AuditRecords", "EventId", "Audit.EventTypes");
            DropForeignKey("Audit.AuditRecords", "SubjectId", "Audit.SubjectTypes");
            DropIndex("Audit.AuditRecords", new[] { "SubjectId" });
            DropIndex("Audit.AuditRecords", new[] { "EventId" });
            RenameColumn(table: "Audit.AuditRecords", name: "WssAccountId", newName: "WssAccount_WSSAccountId");
            RenameIndex(table: "Audit.AuditRecords", name: "IX_WssAccountId", newName: "IX_WssAccount_WSSAccountId");
            AddColumn("Audit.AuditRecords", "AuditSubjectId", c => c.Int(nullable: false));
            AddColumn("Audit.AuditRecords", "AuditEntityId", c => c.Int(nullable: false));
            AddColumn("Audit.AuditRecords", "PerformedBy", c => c.String(nullable: false));
            AddColumn("Audit.AuditRecords", "AuditEventTypeId", c => c.Int(nullable: false));
            DropColumn("Audit.AuditRecords", "SubjectId");
            DropColumn("Audit.AuditRecords", "UserId");
            DropColumn("Audit.AuditRecords", "EventId");
            DropColumn("Audit.AuditRecords", "FieldName");
            DropColumn("Audit.AuditRecords", "OldValue");
            DropColumn("Audit.AuditRecords", "NewValue");
            DropTable("Audit.EventTypes");
            DropTable("Audit.SubjectTypes");

        }




        public override void Down()
        {
          
            CreateTable(
                "Audit.SubjectTypes",
                c => new
                    {
                        SubjectId = c.Int(nullable: false),
                        SubjectTypeName = c.String(),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "Audit.EventTypes",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        EventTypeName = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            AddColumn("Audit.AuditRecords", "NewValue", c => c.String());
            AddColumn("Audit.AuditRecords", "OldValue", c => c.String());
            AddColumn("Audit.AuditRecords", "FieldName", c => c.String());
            AddColumn("Audit.AuditRecords", "EventId", c => c.Int());
            AddColumn("Audit.AuditRecords", "UserId", c => c.String());
            AddColumn("Audit.AuditRecords", "SubjectId", c => c.Int());
            DropColumn("Audit.AuditRecords", "AuditEventTypeId");
            DropColumn("Audit.AuditRecords", "PerformedBy");
            DropColumn("Audit.AuditRecords", "AuditEntityId");
            DropColumn("Audit.AuditRecords", "AuditSubjectId");
            RenameIndex(table: "Audit.AuditRecords", name: "IX_WssAccount_WSSAccountId", newName: "IX_WssAccountId");
            RenameColumn(table: "Audit.AuditRecords", name: "WssAccount_WSSAccountId", newName: "WssAccountId");
            CreateIndex("Audit.AuditRecords", "EventId");
            CreateIndex("Audit.AuditRecords", "SubjectId");
            AddForeignKey("Audit.AuditRecords", "SubjectId", "Audit.SubjectTypes", "SubjectId");
            AddForeignKey("Audit.AuditRecords", "EventId", "Audit.EventTypes", "EventId");
        }
    }
}
