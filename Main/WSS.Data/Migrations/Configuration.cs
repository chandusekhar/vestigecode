using System;
using System.Data.Entity.Migrations;
using WSS.Data.Enum;

namespace WSS.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WssApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WssApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            ////
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );

            //var status = new Status
            //{
            //    StatusId = 1,
            //    StatusDomain = "Account",
            //    StatusName = "Not Registered"
            //};
            //context.Status.AddOrUpdate(status);

            //status = new Status
            //{
            //    StatusId = 2,
            //    StatusDomain = "Account",
            //    StatusName = "Registered"
            //};
            //context.Status.AddOrUpdate(status);

            //status = new Status
            //{
            //    StatusId = 3,
            //    StatusDomain = "Account",
            //    StatusName = "Active"
            //};
            //context.Status.AddOrUpdate(status);

            //status = new Status
            //{
            //    StatusId = 4,
            //    StatusDomain = "Account",
            //    StatusName = "Locked"
            //};
            //context.Status.AddOrUpdate(status);

            var wssAccount = new WssAccount
            {
                WSSAccountId = 1,
                PrimaryEmailAddress = "nsmith@gmail.com",
                WssAccountStatusCode = "ACT",
                FailedResendActivationAttempts = 0,
                AuthenticationId = "7226679b-2d05-41a6-bbea-b45f7773f300",
                IsActive = true,
                SecurityQuestion = "What was the name of your favorite childhood friend?",
                SecurityQuestionAnswer = "test"
            };
            context.WssAccounts.AddOrUpdate(wssAccount);

            wssAccount = new WssAccount
            {
                WSSAccountId = 2,
                PrimaryEmailAddress = "ajarvis@yahoo.com",
                WssAccountStatusCode = "ACT",
                FailedResendActivationAttempts = 0,
                AuthenticationId = "7c07e6ef-8cad-4c0e-be59-1c5701c81909",
                IsActive = true,
                SecurityQuestion = "What was the name of your favorite childhood friend?",
                SecurityQuestionAnswer = "test"
            };
            context.WssAccounts.AddOrUpdate(wssAccount);

            wssAccount = new WssAccount
            {
                WSSAccountId = 3,
                PrimaryEmailAddress = "lashley@yahoo.com",
                WssAccountStatusCode = "ACT",
                FailedResendActivationAttempts = 0,
                AuthenticationId = "ecab06c0-3494-41e3-95fc-63ed1496eddd",
                IsActive = true,
                SecurityQuestion = "What was the name of your favorite childhood friend?",
                SecurityQuestionAnswer = "test"
            };
            context.WssAccounts.AddOrUpdate(wssAccount);

            var linkedUtilityAccount = new LinkedUtilityAccount
            {
                LinkedUtilityAccountId = 1,
                NickName = "ACCT-1",
                WssAccountId = 1,
                UtilityAccountId = 1,
                DefaultAccount = true
            };
            context.LinkedUtilityAccounts.AddOrUpdate(linkedUtilityAccount);

            linkedUtilityAccount = new LinkedUtilityAccount
            {
                LinkedUtilityAccountId = 2,
                NickName = "Other-1",
                WssAccountId = 1,
                UtilityAccountId = 2,
                DefaultAccount = false
            };
            context.LinkedUtilityAccounts.AddOrUpdate(linkedUtilityAccount);

            linkedUtilityAccount = new LinkedUtilityAccount
            {
                LinkedUtilityAccountId = 3,
                NickName = "ACCT-2",
                WssAccountId = 2,
                UtilityAccountId = 3,
                DefaultAccount = true
            };
            context.LinkedUtilityAccounts.AddOrUpdate(linkedUtilityAccount);

            linkedUtilityAccount = new LinkedUtilityAccount
            {
                LinkedUtilityAccountId = 4,
                NickName = "ACCT-3",
                WssAccountId = 3,
                UtilityAccountId = 4,
                DefaultAccount = true
            };
            context.LinkedUtilityAccounts.AddOrUpdate(linkedUtilityAccount);



            //DF - The values below are for the new DomainLookup table.  
            //Once we have completed migration to the Domain Lookup table from the various individual status tables, 
            //the entries above should be cleaned up (i.e. Status) as they will no longer be required.
            var domainLookup = new DomainLookup
            {
                DomainLookupId = 1,
                DomainName = "SUBSCRIBE_TX_STATUS",
                LookupCode = "PENDING",
                LookupValue = "Pending"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 2,
                DomainName = "SUBSCRIBE_TX_STATUS",
                LookupCode = "HOLD",
                LookupValue = "Held"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 3,
                DomainName = "SUBSCRIBE_TX_STATUS",
                LookupCode = "PROCESSED",
                LookupValue = "Processed"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 4,
                DomainName = "SUBSCRIBE_TX_TYPE",
                LookupCode = "SUB",
                LookupValue = "Subscribed"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 5,
                DomainName = "SUBSCRIBE_TX_TYPE",
                LookupCode = "UNSUB",
                LookupValue = "Unsubscribed"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 6,
                DomainName = "WSS_ACCOUNT_STATUS",
                LookupCode = "REG",
                LookupValue = "Registered"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 7,
                DomainName = "WSS_ACCOUNT_STATUS",
                LookupCode = "ACT",
                LookupValue = "Active"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 8,
                DomainName = "WSS_ACCOUNT_STATUS",
                LookupCode = "UNSUB",
                LookupValue = "Unsubscribed"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 9,
                DomainName = "WSS_ACCOUNT_STATUS",
                LookupCode = "LCKD",
                LookupValue = "Locked"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);




            domainLookup = new DomainLookup
            {
                DomainLookupId = 10,
                DomainName = "AUDIT_SUBJECT",
                LookupCode = "WSS_ACCT",
                LookupValue = "WSS Account"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 11,
                DomainName = "AUDIT_SUBJECT",
                LookupCode = "WSS_INTBILL",
                LookupValue = "WSS Intercepted Bill"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);




            domainLookup = new DomainLookup
            {
                DomainLookupId = 12,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "CHNGUID",
                LookupValue = "Change User Id"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 13,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "UNSUB",
                LookupValue = "Unsubscribe"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 14,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "ACCTLOCK",
                LookupValue = "Account Unlocked"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 15,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "RSNDACT",
                LookupValue = "Resend Activation"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 16,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "REG",
                LookupValue = "Register"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 17,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "RSTPWD",
                LookupValue = "Changed Password"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 18,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "LNKACCT",
                LookupValue = "Linked Account"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 19,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "ULINKACCT",
                LookupValue = "Unlinked Account"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 20,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "CHNGNN",
                LookupValue = "Nickname Changed"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 21,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "ADDEMAIL",
                LookupValue = "Added Notification only User"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 22,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "REMEMAIL",
                LookupValue = "Remove Notification only User"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 23,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "CHSECQ",
                LookupValue = "Security Question Changed"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);


            domainLookup = new DomainLookup
            {
                DomainLookupId = 24,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "RSTPWDEMAIL",
                LookupValue = "Reset Password"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 25,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "ACTVTN",
                LookupValue = "Activate Profile"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 26,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "ACCTLOCKD",
                LookupValue = "Account Locked"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 27,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "LGOUT",
                LookupValue = "Logout"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 28,
                DomainName = "AUDIT_EVENT_WSS_ACCT",
                LookupCode = "OPTOUT",
                LookupValue = "Optout"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);
        }
    }
}