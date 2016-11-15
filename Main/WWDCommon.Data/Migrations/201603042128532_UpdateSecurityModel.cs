namespace WWDCommon.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateSecurityModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Functions", "FunctionName", c => c.String(maxLength: 100));
        }

        public override void Down()
        {
            AlterColumn("dbo.Functions", "FunctionName", c => c.String(maxLength: 50));
        }
    }
}