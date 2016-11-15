namespace WWDCommon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersRolesFunctions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        FunctionId = c.Int(nullable: false, identity: true),
                        FunctionCode = c.String(maxLength: 20),
                        FunctionName = c.String(maxLength: 50),
                        FunctionDescription = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.FunctionId);

            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 50),
                        RoleDescription = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        isActive = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
        }
        
        public override void Down()
        {
            DropTable("common.Users");
            DropTable("common.Roles");
            DropTable("common.Functions");
        }
    }
}
