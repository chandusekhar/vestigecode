namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentHeadertablechanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("ubill.DocumentHeaders", "DocumentTypeCode", c => c.String(maxLength: 25));
            AddColumn("ubill.DocumentHeaders", "DocumentStatusCode", c => c.String(nullable: false, maxLength: 25));
            DropColumn("ubill.DocumentHeaders", "DocumentType");
            DropColumn("ubill.DocumentHeaders", "DocumentStatusId");
        }
        
        public override void Down()
        {
            AddColumn("ubill.DocumentHeaders", "DocumentStatusId", c => c.Int(nullable: false));
            AddColumn("ubill.DocumentHeaders", "DocumentType", c => c.Int());
            DropColumn("ubill.DocumentHeaders", "DocumentStatusCode");
            DropColumn("ubill.DocumentHeaders", "DocumentTypeCode");
        }
    }
}
