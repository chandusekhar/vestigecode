namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWSSAccountStatusCode : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "wss.WssAccount", name: "StatusId", newName: "Status_StatusId");
            RenameIndex(table: "wss.WssAccount", name: "IX_StatusId", newName: "IX_Status_StatusId");
            AddColumn("wss.WssAccount", "WssAccountStatusCode", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            DropColumn("wss.WssAccount", "WssAccountStatusCode");
            RenameIndex(table: "wss.WssAccount", name: "IX_Status_StatusId", newName: "IX_StatusId");
            RenameColumn(table: "wss.WssAccount", name: "Status_StatusId", newName: "StatusId");
        }
    }
}
