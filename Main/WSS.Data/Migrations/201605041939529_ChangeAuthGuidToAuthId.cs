namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAuthGuidToAuthId : DbMigration
    {
        public override void Up()
        {
            AddColumn("wss.WssAccount", "AuthenticationId", c => c.String(maxLength: 128));
            DropColumn("wss.WssAccount", "AuthenticationGuid");
        }
        
        public override void Down()
        {
            AddColumn("wss.WssAccount", "AuthenticationGuid", c => c.Guid(nullable: false));
            DropColumn("wss.WssAccount", "AuthenticationId");
        }
    }
}
