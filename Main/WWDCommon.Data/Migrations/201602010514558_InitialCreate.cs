//namespace WWDCommon.Data.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;

//    public partial class InitialCreate : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "common.EmailTemplates",
//                c => new
//                    {
//                        TemplateId = c.Int(nullable: false, identity: true),
//                        TemplateName = c.String(maxLength: 50),
//                        DefaultFrom = c.String(maxLength: 100),
//                        Defaultcc = c.String(maxLength: 100),
//                        Defaultbcc = c.String(maxLength: 100),
//                        Subject = c.String(maxLength: 100),
//                        MessageBody = c.String(),
//                        Version = c.String(maxLength: 50),
//                        LastChangedDate = c.DateTime(precision: 7, storeType: "datetime2"),
//                    })
//                .PrimaryKey(t => t.TemplateId);

//            CreateTable(
//                "common.EmailTransactions",
//                c => new
//                    {
//                        EmailTransactionID = c.Int(nullable: false, identity: true),
//                        EmailTo = c.String(maxLength: 50),
//                        IsProcessed = c.Boolean(),
//                        SourcePackage = c.String(maxLength: 10, fixedLength: true),
//                        TemplateId = c.Int(),
//                        Parameters = c.String(),
//                        ReferenceType = c.String(maxLength: 50),
//                        ReferenceValue = c.String(maxLength: 50),
//                    })
//                .PrimaryKey(t => t.EmailTransactionID);

//        }

//        public override void Down()
//        {
//            DropTable("common.EmailTransactions");
//            DropTable("common.EmailTemplates");
//        }
//    }
//}