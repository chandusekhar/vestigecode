//namespace WWDCommon.Data.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;

//    public partial class AddApplicationRoles : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "dbo.ApplicationRoles",
//                c => new
//                    {
//                        Id = c.Int(nullable: false),
//                        ApplicationId = c.Int(nullable: false),
//                        RoleId = c.Int(nullable: false),
//                    })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.Application", t => t.Id)
//                .ForeignKey("dbo.Roles", t => t.Id)
//                .Index(t => t.Id);

//        }

//        public override void Down()
//        {
//            DropForeignKey("dbo.ApplicationRoles", "Id", "dbo.Roles");
//            DropForeignKey("dbo.ApplicationRoles", "Id", "dbo.Application");
//            DropIndex("dbo.ApplicationRoles", new[] { "Id" });
//            DropTable("dbo.ApplicationRoles");
//        }
//    }
//}