namespace UtilityBilling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDomainLookupViews : DbMigration
    {
        public override void Up()
        {
            var ctx = new UtilityBillingContext();

            var script = @"CREATE VIEW [ref].vwDLU_DOCUMENT_STATUS
                        AS
                        SELECT 
	                        DomainLookupID as DocumentStatusId, 
	                        LookupCode as DocumentStatusCode, 
	                        LookupValue as DocumentStatusValue
                        FROM ref.DomainLookup
                        WHERE (DomainName = 'DOCUMENT_STATUS');";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"CREATE VIEW [ref].vwDLU_DOCUMENT_TYPE
                            AS
                            SELECT 
	                            DomainLookupID as DocumentTypeId, 
	                            LookupCode as DocumentTypeCode, 
	                            LookupValue as DocumentTypeValue
                            FROM ref.DomainLookup
                            WHERE (DomainName = 'DOCUMENT_TYPE');";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"CREATE VIEW [ref].vwDLU_UTILITY_ACCOUNT_SOURCE
                        AS
                        SELECT 
	                        DomainLookupID as UtilityAccountSourceId, 
	                        LookupCode as UtilityAccountSourceCode, 
	                        LookupValue as UtilityAccountSourceValue
                        FROM ref.DomainLookup
                        WHERE (DomainName = 'UTILITY_ACCOUNT_SOURCE');";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"CREATE VIEW [ref].vwDLU_DOC1_TO_WSS_DOCTYPE_MAP
                        AS
                        SELECT 
	                        DomainLookupID as TypeID, 
	                        LookupCode as Doc1DocTypeCode, 
	                        LookupValue as WssDocTypeCode
                        FROM ref.DomainLookup
                        WHERE (DomainName = 'DOC1_TO_WSS_DOCTYPE_MAP');";
            ctx.Database.ExecuteSqlCommand(script);
        }
        
        public override void Down()
        {
            var ctx = new UtilityBillingContext();

            var script = @"drop view ref.vwDLU_DOCUMENT_STATUS;";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"drop view ref.vwDLU_DOCUMENT_TYPE;";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"drop view ref.vwDLU_UTILITY_ACCOUNT_SOURCE;";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"drop view ref.vwDLU_DOC1_TO_WSS_DOCTYPE_MAP;";
            ctx.Database.ExecuteSqlCommand(script);
        }
    }
}
