//namespace WWDCommon.Data.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;

//    public partial class AddUserRoles : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "dbo.UserRoles",
//                c => new
//                    {
//                        Id = c.Int(nullable: false),
//                        UserID = c.Int(nullable: false),
//                        RoleId = c.Int(nullable: false),
//                    })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.Roles", t => t.Id)
//                .ForeignKey("dbo.Users", t => t.Id)
//                .Index(t => t.Id);

//        }

//        public override void Down()
//        {
//            DropForeignKey("dbo.UserRoles", "Id", "dbo.Users");
//            DropForeignKey("dbo.UserRoles", "Id", "dbo.Roles");
//            DropIndex("dbo.UserRoles", new[] { "Id" });
//            DropTable("dbo.UserRoles");
//        }
//    }
//}