namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveColumnToWssAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("wss.WssAccount", "ActivationToken", c => c.String());
            AddColumn("wss.WssAccount", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("wss.WssAccount", "IsActive");
            DropColumn("wss.WssAccount", "ActivationToken");
        }
    }
}
