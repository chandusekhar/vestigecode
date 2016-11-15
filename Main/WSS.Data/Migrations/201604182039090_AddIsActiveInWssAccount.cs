namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveInWssAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("wss.WssAccount", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("wss.WssAccount", "IsActive");
        }
    }
}
