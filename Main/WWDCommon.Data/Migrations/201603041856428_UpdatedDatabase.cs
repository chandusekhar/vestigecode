namespace WWDCommon.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationRoles",
                c => new
                {
                    Id = c.Int(nullable: false),
                    ApplicationId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Application",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ApplicationName = c.String(maxLength: 50),
                    ApplicationDescription = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

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

            CreateTable(
                "dbo.EmailQueue",
                c => new
                {
                    Emaild = c.Int(nullable: false, identity: true),
                    TemplateName = c.String(),
                    Parameters = c.String(),
                    EtlBatchNumber = c.String(),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Emaild);

            CreateTable(
                "common.EmailTemplates",
                c => new
                {
                    TemplateId = c.Int(nullable: false, identity: true),
                    TemplateName = c.String(maxLength: 50),
                    DefaultFrom = c.String(maxLength: 100),
                    Defaultcc = c.String(maxLength: 100),
                    Defaultbcc = c.String(maxLength: 100),
                    Subject = c.String(maxLength: 100),
                    MessageBody = c.String(),
                    Version = c.String(maxLength: 50),
                    LastChangedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.TemplateId);

            CreateTable(
                "common.EmailTransactions",
                c => new
                {
                    EmailTransactionID = c.Int(nullable: false, identity: true),
                    EmailTo = c.String(maxLength: 50),
                    IsProcessed = c.Boolean(),
                    SourcePackage = c.String(maxLength: 10, fixedLength: true),
                    TemplateId = c.Int(),
                    Parameters = c.String(),
                    ReferenceType = c.String(maxLength: 50),
                    ReferenceValue = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.EmailTransactionID);

            CreateTable(
                "dbo.FunctionsRoles",
                c => new
                {
                    Functions_FunctionId = c.Int(nullable: false),
                    Roles_RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Functions_FunctionId, t.Roles_RoleId })
                .ForeignKey("dbo.Functions", t => t.Functions_FunctionId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Roles_RoleId, cascadeDelete: true)
                .Index(t => t.Functions_FunctionId)
                .Index(t => t.Roles_RoleId);

            CreateTable(
                "dbo.UsersRoles",
                c => new
                {
                    Users_UserId = c.Int(nullable: false),
                    Roles_RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Users_UserId, t.Roles_RoleId })
                .ForeignKey("dbo.Users", t => t.Users_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Roles_RoleId, cascadeDelete: true)
                .Index(t => t.Users_UserId)
                .Index(t => t.Roles_RoleId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ApplicationRoles", "Id", "dbo.Roles");
            DropForeignKey("dbo.UsersRoles", "Roles_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UsersRoles", "Users_UserId", "dbo.Users");
            DropForeignKey("dbo.FunctionsRoles", "Roles_RoleId", "dbo.Roles");
            DropForeignKey("dbo.FunctionsRoles", "Functions_FunctionId", "dbo.Functions");
            DropForeignKey("dbo.ApplicationRoles", "Id", "dbo.Application");
            DropIndex("dbo.UsersRoles", new[] { "Roles_RoleId" });
            DropIndex("dbo.UsersRoles", new[] { "Users_UserId" });
            DropIndex("dbo.FunctionsRoles", new[] { "Roles_RoleId" });
            DropIndex("dbo.FunctionsRoles", new[] { "Functions_FunctionId" });
            DropIndex("dbo.ApplicationRoles", new[] { "Id" });
            DropTable("dbo.UsersRoles");
            DropTable("dbo.FunctionsRoles");
            DropTable("common.EmailTransactions");
            DropTable("common.EmailTemplates");
            DropTable("dbo.EmailQueue");
            DropTable("dbo.Users");
            DropTable("dbo.Functions");
            DropTable("dbo.Roles");
            DropTable("dbo.Application");
            DropTable("dbo.ApplicationRoles");
        }
    }
}