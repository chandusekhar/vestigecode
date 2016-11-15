//namespace WWDCommon.Data.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;

//    public partial class AddSecurityTables : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "dbo.Application",
//                c => new
//                    {
//                        Id = c.Int(nullable: false, identity: true),
//                        ApplicationName = c.String(maxLength: 50),
//                        ApplicationDescription = c.String(maxLength: 50),
//                    })
//                .PrimaryKey(t => t.Id);

//            CreateTable(
//                "dbo.Functions",
//                c => new
//                    {
//                        Id = c.Int(nullable: false, identity: true),
//                        FunctionName = c.String(maxLength: 50),
//                        FunctionDescription = c.String(maxLength: 50),
//                    })
//                .PrimaryKey(t => t.Id);

//            CreateTable(
//                "dbo.Roles",
//                c => new
//                    {
//                        Id = c.Int(nullable: false, identity: true),
//                        RoleName = c.String(maxLength: 50),
//                        RoleDescription = c.String(maxLength: 50),
//                    })
//                .PrimaryKey(t => t.Id);

//            CreateTable(
//                "dbo.Users",
//                c => new
//                    {
//                        Id = c.Int(nullable: false, identity: true),
//                        Username = c.String(maxLength: 50),
//                        FirstName = c.String(maxLength: 50),
//                        LastName = c.String(maxLength: 50),
//                        isActive = c.Boolean(nullable: false),
//                        isDeleted = c.Boolean(nullable: false),
//                    })
//                .PrimaryKey(t => t.Id);

//        }

//        public override void Down()
//        {
//            DropTable("dbo.Users");
//            DropTable("dbo.Roles");
//            DropTable("dbo.Functions");
//            DropTable("dbo.Application");
//        }
//    }
//}