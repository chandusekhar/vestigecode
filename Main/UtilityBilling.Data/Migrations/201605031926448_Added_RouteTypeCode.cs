namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_RouteTypeCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("ubill.DocumentHeaders", "RouteTypeCode", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("ubill.DocumentHeaders", "RouteTypeCode");
        }
    }
}
