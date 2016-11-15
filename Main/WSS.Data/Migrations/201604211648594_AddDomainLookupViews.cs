namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDomainLookupViews : DbMigration
    {
        public override void Up()
        {
            var ctx = new WssApplicationContext();

            var script = @"CREATE VIEW [ref].vwDLU_SUBSCRIBE_TX_STATUS
                            AS
                            SELECT 
	                            DomainLookupID as SubscriptionTransactionStatusId, 
	                            LookupCode as SubscriptionTransactionStatusCode, 
	                            LookupValue as SubscriptionTransactionStatusDesc
                            FROM ref.DomainLookup
                            WHERE (DomainName = 'SUBSCRIBE_TX_STATUS');";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"CREATE VIEW [ref].vwDLU_SUBSCRIBE_TX_TYPE
                        AS
                        SELECT 
	                        DomainLookupID as SubscriptionTransactionTypeId, 
	                        LookupCode as SubscriptionTransactionTypeCode, 
	                        LookupValue as SubscriptionTransactionTypeDesc
                        FROM ref.DomainLookup
                        WHERE (DomainName = 'SUBSCRIBE_TX_TYPE');";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"CREATE VIEW [ref].vwDLU_WSS_ACCOUNT_STATUS
                        AS
                        SELECT 
	                        DomainLookupID as WssAccountStatusId, 
	                        LookupCode as WssAccountStatusCode, 
	                        LookupValue as WssAccountStatusDesc
                        FROM ref.DomainLookup
                        WHERE (DomainName = 'WSS_ACCOUNT_STATUS');";
            ctx.Database.ExecuteSqlCommand(script);
        }
        
        public override void Down()
        {
            var ctx = new WssApplicationContext();

            var script = @"drop view ref.vwDLU_WSS_ACCOUNT_STATUS;";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"drop view ref.vwDLU_SUBSCRIBE_TX_TYPE;";
            ctx.Database.ExecuteSqlCommand(script);

            script = @"drop view ref.vwDLU_SUBSCRIBE_TX_STATUS;";
            ctx.Database.ExecuteSqlCommand(script);
        }
    }
}
