namespace UtilityBilling.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeDocumentKeyAndIdTypeToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ubill.DocumentHeaders", "DocumentId", c => c.Int());
            AlterColumn("ubill.DocumentHeaders", "DocumentKey", c => c.Int());
        }

        public override void Down()
        {
            AlterColumn("ubill.DocumentHeaders", "DocumentKey", c => c.String(maxLength: 50));
            AlterColumn("ubill.DocumentHeaders", "DocumentId", c => c.String(maxLength: 50));
        }
    }
}