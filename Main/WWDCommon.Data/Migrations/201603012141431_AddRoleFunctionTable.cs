//namespace WWDCommon.Data.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;

//    public partial class AddRoleFunctionTable : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "dbo.RolesFunctions",
//                c => new
//                    {
//                        Id = c.Int(nullable: false),
//                        FunctionId = c.Int(nullable: false),
//                        RoleId = c.Int(nullable: false),
//                    })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.Functions", t => t.Id)
//                .ForeignKey("dbo.Roles", t => t.Id)
//                .Index(t => t.Id);

//        }

//        public override void Down()
//        {
//            DropForeignKey("dbo.RolesFunctions", "Id", "dbo.Roles");
//            DropForeignKey("dbo.RolesFunctions", "Id", "dbo.Functions");
//            DropIndex("dbo.RolesFunctions", new[] { "Id" });
//            DropTable("dbo.RolesFunctions");
//        }
//    }
//}