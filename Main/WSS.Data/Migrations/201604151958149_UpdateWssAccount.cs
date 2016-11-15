namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWssAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("wss.WssAccount", "ActivationToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("wss.WssAccount", "ActivationToken");
        }
    }
}
