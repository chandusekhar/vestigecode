using System;
using System.Data.Entity.Migrations;
using System.IO;

namespace UtilityBilling.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UtilityBillingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UtilityBillingContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            var account = new UtilityAccount()
            {
                UtilityAccountId = 1,
                PrimaryAccountHolderName = "Nicky Smith",
                ccb_acct_id = "1234567891",
                UtilityAccountSourceCode = "WSS",
            };
            context.UtilityAccounts.AddOrUpdate(account);

            account = new UtilityAccount()
            {
                UtilityAccountId = 2,
                PrimaryAccountHolderName = "Scott Baldwin",
                ccb_acct_id = "1234567892",
                UtilityAccountSourceCode = "WSS",
            };
            context.UtilityAccounts.AddOrUpdate(account);

            account = new UtilityAccount()
            {
                UtilityAccountId = 3,
                PrimaryAccountHolderName = "Aaron Jarvis",
                ccb_acct_id = "1234567893",
                UtilityAccountSourceCode = "WSS",
            };
            context.UtilityAccounts.AddOrUpdate(account);

            account = new UtilityAccount()
            {
                UtilityAccountId = 4,
                PrimaryAccountHolderName = "Lloyd Ashley",
                ccb_acct_id = "1234567894",
                UtilityAccountSourceCode = "WSS",
            };
            context.UtilityAccounts.AddOrUpdate(account);

            account = new UtilityAccount()
            {
                UtilityAccountId = 5,
                PrimaryAccountHolderName = "Alun Wyn Jones",
                ccb_acct_id = "1234567895",
                UtilityAccountSourceCode = "WSS",
            };
            context.UtilityAccounts.AddOrUpdate(account);

            account = new UtilityAccount()
            {
                UtilityAccountId = 6,
                PrimaryAccountHolderName = "Sam Warburton",
                ccb_acct_id = "1234567896",
                UtilityAccountSourceCode = "WSS",
            };
            context.UtilityAccounts.AddOrUpdate(account);

            var docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 1,
                UtilityAccountId = 1,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 4, 30),
                PublishedDate = new DateTime(2015, 4, 30),
                BillDueDate = new DateTime(2015, 6, 1),
                BillAmmountDue = 10,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 1,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 2,
                UtilityAccountId = 2,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 4, 30),
                PublishedDate = new DateTime(2015, 4, 30),
                BillDueDate = new DateTime(2015, 6, 1),
                BillAmmountDue = 20,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 2,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 3,
                UtilityAccountId = 3,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 4, 30),
                PublishedDate = new DateTime(2015, 4, 30),
                BillDueDate = new DateTime(2015, 6, 1),
                BillAmmountDue = 30,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 3,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 4,
                UtilityAccountId = 4,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 4, 30),
                PublishedDate = new DateTime(2015, 4, 30),
                BillDueDate = new DateTime(2015, 6, 1),
                BillAmmountDue = 40,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 4,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 5,
                UtilityAccountId = 5,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 4, 30),
                PublishedDate = new DateTime(2015, 4, 30),
                BillDueDate = new DateTime(2015, 6, 1),
                BillAmmountDue = 50,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 5,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 6,
                UtilityAccountId = 1,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 7, 31),
                PublishedDate = new DateTime(2015, 7, 31),
                BillDueDate = new DateTime(2015, 9, 1),
                BillAmmountDue = 60,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 6,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 7,
                UtilityAccountId = 2,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 7, 31),
                PublishedDate = new DateTime(2015, 7, 31),
                BillDueDate = new DateTime(2015, 9, 1),
                BillAmmountDue = 70,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 7,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 8,
                UtilityAccountId = 3,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 7, 31),
                PublishedDate = new DateTime(2015, 7, 31),
                BillDueDate = new DateTime(2015, 9, 1),
                BillAmmountDue = 80,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 8,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 9,
                UtilityAccountId = 4,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 7, 31),
                PublishedDate = new DateTime(2015, 7, 31),
                BillDueDate = new DateTime(2015, 9, 1),
                BillAmmountDue = 90,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 9,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 10,
                UtilityAccountId = 5,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 7, 31),
                PublishedDate = new DateTime(2015, 7, 31),
                BillDueDate = new DateTime(2015, 9, 1),
                BillAmmountDue = 100,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 10,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 11,
                UtilityAccountId = 1,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 10, 31),
                PublishedDate = new DateTime(2015, 10, 31),
                BillDueDate = new DateTime(2015, 12, 1),
                BillAmmountDue = 110,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 11,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 12,
                UtilityAccountId = 2,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 10, 31),
                PublishedDate = new DateTime(2015, 10, 31),
                BillDueDate = new DateTime(2015, 12, 1),
                BillAmmountDue = 120,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 12,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 13,
                UtilityAccountId = 3,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 10, 31),
                PublishedDate = new DateTime(2015, 10, 31),
                BillDueDate = new DateTime(2015, 12, 1),
                BillAmmountDue = 130,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 13,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 14,
                UtilityAccountId = 4,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 10, 31),
                PublishedDate = new DateTime(2015, 10, 31),
                BillDueDate = new DateTime(2015, 12, 1),
                BillAmmountDue = 140,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 14,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 15,
                UtilityAccountId = 5,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2015, 10, 31),
                PublishedDate = new DateTime(2015, 10, 31),
                BillDueDate = new DateTime(2015, 12, 1),
                BillAmmountDue = 150,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 15,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 16,
                UtilityAccountId = 1,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2016, 1, 31),
                PublishedDate = new DateTime(2016, 1, 31),
                BillDueDate = new DateTime(2016, 3, 1),
                BillAmmountDue = 160,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 16,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 17,
                UtilityAccountId = 2,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2016, 1, 31),
                PublishedDate = new DateTime(2016, 1, 31),
                BillDueDate = new DateTime(2016, 3, 1),
                BillAmmountDue = 170,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 17,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 18,
                UtilityAccountId = 3,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2016, 1, 31),
                PublishedDate = new DateTime(2016, 1, 31),
                BillDueDate = new DateTime(2016, 3, 1),
                BillAmmountDue = 180,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 18,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 19,
                UtilityAccountId = 4,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2016, 1, 31),
                PublishedDate = new DateTime(2016, 1, 31),
                BillDueDate = new DateTime(2016, 3, 1),
                BillAmmountDue = 190,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 19,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            docHeader = new DocumentHeader()
            {
                DocumentHeaderId = 20,
                UtilityAccountId = 5,
                DocumentTypeCode = "BILL",
                DocumentIssueDate = new DateTime(2016, 1, 31),
                PublishedDate = new DateTime(2016, 1, 31),
                BillDueDate = new DateTime(2016, 3, 1),
                BillAmmountDue = 200,
                DocumentStatusCode = "REL",
                EtlDocumentLoadKey = 20,
                RouteTypeCode = "POSTAL",
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentHeaders.AddOrUpdate(docHeader);

            //Load the PDF data
            //var appDataPath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            //var appDataPath = HostingEnvironment.ApplicationPhysicalPath;
            var appDataPath = AppDomain.CurrentDomain.BaseDirectory;

            var filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill001.pdf");
            var fileBytes = File.ReadAllBytes(filePath);
            var docDetail = new DocumentDetail()
            {
                DocumentDetailId = 1,
                DocumentHeaderId = 1,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill002.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 2,
                DocumentHeaderId = 2,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill003.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 3,
                DocumentHeaderId = 3,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill004.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 4,
                DocumentHeaderId = 4,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill005.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 5,
                DocumentHeaderId = 5,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill006.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 6,
                DocumentHeaderId = 6,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill007.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 7,
                DocumentHeaderId = 7,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill008.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 8,
                DocumentHeaderId = 8,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill009.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 9,
                DocumentHeaderId = 9,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill010.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 10,
                DocumentHeaderId = 10,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill011.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 11,
                DocumentHeaderId = 11,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill012.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 12,
                DocumentHeaderId = 12,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill013.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 13,
                DocumentHeaderId = 13,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill014.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 14,
                DocumentHeaderId = 14,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill015.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 15,
                DocumentHeaderId = 15,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill016.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 16,
                DocumentHeaderId = 16,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill017.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 17,
                DocumentHeaderId = 17,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill018.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 18,
                DocumentHeaderId = 18,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill019.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 19,
                DocumentHeaderId = 19,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            filePath = Path.Combine(appDataPath, "TestData", "Bills", "Bill020.pdf");
            fileBytes = File.ReadAllBytes(filePath);
            docDetail = new DocumentDetail()
            {
                DocumentDetailId = 20,
                DocumentHeaderId = 20,
                DocumentPdf = fileBytes,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.DocumentDetails.AddOrUpdate(docDetail);

            //DF - The values below are for the new DomainLookup table.  
            //Once we have completed migration to the Domain Lookup table from the various individual status tables, 
            //the entries above should be cleaned up (i.e. Status) as they will no longer be required.
            var domainLookup = new DomainLookup
            {
                DomainLookupId = 1,
                DomainName = "DOCUMENT_TYPE",
                LookupCode = "BILL",
                LookupValue = "Bill"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 2,
                DomainName = "DOCUMENT_TYPE",
                LookupCode = "REMIND",
                LookupValue = "Reminder"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 3,
                DomainName = "UTILITY_ACCOUNT_SOURCE",
                LookupCode = " WSS",
                LookupValue = "Pending Verification"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 4,
                DomainName = "UTILITY_ACCOUNT_SOURCE",
                LookupCode = "CCB",
                LookupValue = "Verified"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 5,
                DomainName = "DOCUMENT_STATUS",
                LookupCode = "HLD",
                LookupValue = "Held"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 6,
                DomainName = "DOCUMENT_STATUS",
                LookupCode = "PUB",
                LookupValue = "Published"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 7,
                DomainName = "DOCUMENT_STATUS",
                LookupCode = "REL",
                LookupValue = "Released"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 8,
                DomainName = "DOCUMENT_STATUS",
                LookupCode = "REM",
                LookupValue = "Removed"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 9,
                DomainName = "DOCUMENT_STATUS",
                LookupCode = "REJ",
                LookupValue = "Rejected"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 10,
                DomainName = "DOC1_TO_WSS_DOCTYPE_MAP",
                LookupCode = "UTILITY BILL",
                LookupValue = "BILL"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            domainLookup = new DomainLookup
            {
                DomainLookupId = 11,
                DomainName = "DOC1_TO_WSS_DOCTYPE_MAP",
                LookupCode = "REMINDERNTCE",
                LookupValue = "REMIND"
            };
            context.DomainLookups.AddOrUpdate(domainLookup);

            var accountMeterLookup = new AccountMeterLookup
            {
                AccountMeterLookupId = 1,
                CcbAcctId = "1234567891",
                CcbBadgeNbr = "UNMETERED",
                UtilityAccountId = 1,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.AccountMeterLookup.AddOrUpdate(accountMeterLookup);

            accountMeterLookup = new AccountMeterLookup
            {
                AccountMeterLookupId = 2,
                CcbAcctId = "1234567892",
                CcbBadgeNbr = "UNMETERED",
                UtilityAccountId = 2,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.AccountMeterLookup.AddOrUpdate(accountMeterLookup);

            accountMeterLookup = new AccountMeterLookup
            {
                AccountMeterLookupId = 3,
                CcbAcctId = "1234567893",
                CcbBadgeNbr = "UNMETERED",
                UtilityAccountId = 3,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.AccountMeterLookup.AddOrUpdate(accountMeterLookup);

            accountMeterLookup = new AccountMeterLookup
            {
                AccountMeterLookupId = 4,
                CcbAcctId = "1234567894",
                CcbBadgeNbr = "UNMETERED",
                UtilityAccountId = 4,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.AccountMeterLookup.AddOrUpdate(accountMeterLookup);

            accountMeterLookup = new AccountMeterLookup
            {
                AccountMeterLookupId = 5,
                CcbAcctId = "1234567895",
                CcbBadgeNbr = "UNMETERED",
                UtilityAccountId = 5,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.AccountMeterLookup.AddOrUpdate(accountMeterLookup);

            accountMeterLookup = new AccountMeterLookup
            {
                AccountMeterLookupId = 6,
                CcbAcctId = "1234567896",
                CcbBadgeNbr = "UNMETERED",
                UtilityAccountId = 6,
                EtlInsRunNumber = 1,
                EtlInsProcessName = "EF_SEED_METHOD",
                EtlInsTimestamp = DateTime.Now
            };
            context.AccountMeterLookup.AddOrUpdate(accountMeterLookup);
        }
    }
}