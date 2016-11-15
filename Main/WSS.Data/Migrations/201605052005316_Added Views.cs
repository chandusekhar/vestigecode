namespace WSS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedViews : DbMigration
    {
        public override void Up()
        {
            var ctx = new WssApplicationContext();

            var script = @"CREATE VIEW [ref].vwDLU_AUDIT_SUBJECT

            AS

            SELECT 

            DomainLookupID as AuditSubjectId, 

            LookupCode as AuditSubjectCode, 

            LookupValue as AuditSubjectDesc

            FROM ref.DomainLookup

             WHERE (DomainName = 'AUDIT_SUBJECT');";

            ctx.Database.ExecuteSqlCommand(script);




            script = @"CREATE VIEW [ref].vwDLU_AUDIT_EVENT_WSS_ACCT

            AS

            SELECT 

            DomainLookupID as AuditEventWssAccountId, 

            LookupCode as AuditEventWssAccountCode, 

            LookupValue as AuditEventWssAccountDesc

            FROM ref.DomainLookup

             WHERE (DomainName = 'AUDIT_EVENT_WSS_ACCT');";

            ctx.Database.ExecuteSqlCommand(script);

        }
        
        public override void Down()
        {
            var ctx = new WssApplicationContext();

            var script = @"drop view ref.vwDLU_AUDIT_EVENT_WSS_ACCT;";

            ctx.Database.ExecuteSqlCommand(script);

            script = @"drop view ref.vwDLU_AUDIT_SUBJECT;";

            ctx.Database.ExecuteSqlCommand(script);
        }
    }
}
