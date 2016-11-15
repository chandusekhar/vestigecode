//namespace WWDCommon.Data.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;

//    public partial class CreateTableEmailQueue : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "dbo.EmailQueue",
//                c => new
//                    {
//                        Emaild = c.Int(nullable: false, identity: true),
//                        TemplateName = c.String(),
//                        Parameters = c.String(),
//                        EtlBatchNumber = c.String(),
//                        IsActive = c.Boolean(nullable: false),
//                    })
//                .PrimaryKey(t => t.Emaild);

//        }

//        public override void Down()
//        {
//            DropTable("dbo.EmailQueue");
//        }
//    }
//}