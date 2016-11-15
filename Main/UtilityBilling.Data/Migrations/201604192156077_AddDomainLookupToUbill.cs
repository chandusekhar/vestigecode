namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDomainLookupToUbill : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ref.DomainLookup",
                c => new
                    {
                        DomainLookupId = c.Int(nullable: false, identity: true),
                        DomainName = c.String(nullable: false, maxLength: 50),
                        LookupCode = c.String(nullable: false, maxLength: 25),
                        LookupValue = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.DomainLookupId);
            
        }
        
        public override void Down()
        {
            DropTable("ref.DomainLookup");
        }
    }
}
