namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStatusTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("wss.WssAccount", "StatusId", "wss.Status");
            DropIndex("wss.WssAccount", new[] { "Status_StatusId" });
            DropColumn("wss.WssAccount", "Status_StatusId");
            DropTable("wss.Status");
        }
        
        public override void Down()
        {
            CreateTable(
                "wss.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusDomain = c.String(maxLength: 50),
                        StatusName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.StatusId);
            
            AddColumn("wss.WssAccount", "Status_StatusId", c => c.Int());
            CreateIndex("wss.WssAccount", "Status_StatusId");
            AddForeignKey("wss.WssAccount", "StatusId", "wss.Status", "StatusId");
        }
    }
}
