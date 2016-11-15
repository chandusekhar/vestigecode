using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace WWDCommon.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WDDCommonContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WDDCommonContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Roles
            var roleList = new List<Roles>
            {
                new Roles
                {
                    RoleId = 1,
                    RoleName = "CSR",
                    RoleDescription = "General Duties"
                },
                new Roles
                {
                    RoleId = 2,
                    RoleName = "CSR-Admin App",
                    RoleDescription = "Admin Duties"
                },
                new Roles
                {
                    RoleId = 3,
                    RoleName = "Customer Account Staff",
                    RoleDescription = "Bill Intercept: all functions except Remove a document"
                },
                new Roles
                {
                    RoleId = 4,
                    RoleName = "Bill Administrator",
                    RoleDescription = "Bill Intercept: all functions plus Remove a document"
                }
                // EAY - Add Aditional Roles Here
            };
            foreach (var role in roleList)
            {
                context.Roles.AddOrUpdate(role);
            }

            // Functions
            var functionList = new List<Functions>
            {
                new Functions
                {
                    FunctionId = 1,
                    FunctionCode = "AS&R",
                    FunctionName = "Account Search & Results (all functions)",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 2,
                    FunctionCode = "CDP",
                    FunctionName = "Customer Details - Profile (all functions)",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 3,
                    FunctionCode = "CDB",
                    FunctionName = "Customer Details - Bills (all funcitons)",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 4,
                    FunctionCode = "CDMU",
                    FunctionName = "Customer Details - Manage Utility Accounts (all functions)",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 5,
                    FunctionCode = "CDU",
                    FunctionName = "Customer Details - Audit (all functions)",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 6,
                    FunctionCode = "CDR",
                    FunctionName = "Customer Registration (all functions)",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 7,
                    FunctionCode = "EURA",
                    FunctionName = "Edit users and role assignment",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 8,
                    FunctionCode = "AURA",
                    FunctionName = "Add users with role assignment",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 9,
                    FunctionCode = "GA",
                    FunctionName = "Bill Intercept: General Admin, no Remove",
                    FunctionDescription = "General"
                },
                new Functions
                {
                    FunctionId = 10,
                    FunctionCode = "DELB",
                    FunctionName = "Bill Intercept: Remove a document",
                    FunctionDescription = "General"
                }




                //EAY - Add additonal functions here
            };
            foreach (var function in functionList)
            {
                context.Functions.AddOrUpdate(function);
            }

            // Role functions
            var csrRole = context.Roles.Find(1);
            csrRole.Functions.Clear();
            csrRole.Functions.Add(context.Functions.Find(1));
            csrRole.Functions.Add(context.Functions.Find(2));
            csrRole.Functions.Add(context.Functions.Find(3));
            csrRole.Functions.Add(context.Functions.Find(4));
            csrRole.Functions.Add(context.Functions.Find(5));
            csrRole.Functions.Add(context.Functions.Find(6));

            var csrAdminRole = context.Roles.Find(2);
            csrAdminRole.Functions.Add(context.Functions.Find(7));
            csrAdminRole.Functions.Add(context.Functions.Find(8));

            var csrBillInterceptGeneralAdmin = context.Roles.Find(3);
            csrBillInterceptGeneralAdmin.Functions.Clear();
            csrBillInterceptGeneralAdmin.Functions.Add(context.Functions.Find(9));

            var csrBillInterceptAdminWithRemove = context.Roles.Find(4);
            csrBillInterceptAdminWithRemove.Functions.Clear();
            csrBillInterceptAdminWithRemove.Functions.Add(context.Functions.Find(9));
            csrBillInterceptAdminWithRemove.Functions.Add(context.Functions.Find(10));

            //Users
            var usersList = new List<Users>
            {
                new Users
                {
                    UserId = 1,
                    Username = "cowdmp01\\eyoung",
                    FirstName = "Evan",
                    LastName = "Young",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 2,
                    Username = "cowdmp01\\dfardoe",
                    FirstName = "David",
                    LastName = "Fardoe",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 3,
                    Username = "cowdmp01\\awheeler",
                    FirstName = "Arthur",
                    LastName = "Wheeler",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 4,
                    Username = "cowdmp01\\vborovyt",
                    FirstName = "Valerie",
                    LastName = "Borovytska",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 5,
                    Username = "COWDMP01\\kjohnsto",
                    FirstName = "Keith",
                    LastName = "Johnston",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 6,
                    Username = "COWDMP01\\qnguyen",
                    FirstName = "Quyen",
                    LastName = "Nguyen",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 7,
                    Username = "COWDMP01\\x-gdougl",
                    FirstName = "Glen",
                    LastName = "Douglas",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 8,
                    Username = "COWDMP01\\x-tlevas",
                    FirstName = "Tracey",
                    LastName = "Levasseur",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 9,
                    Username = "COWDMP01\\x-svanch",
                    FirstName = "Sriram",
                    LastName = "Vancheeswaran",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 10,
                    Username = "COWDMP01\\x-pvikas",
                    FirstName = "Prem",
                    LastName = "Vikas",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 11,
                    Username = "COWDMP01\\x-kmanch",
                    FirstName = "Kush",
                    LastName = "Manchanda",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 12,
                    Username = "COWDMP01\\x-rnault",
                    FirstName = "Ray",
                    LastName = "Nault",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 13,
                    Username = "COWDMP01\\x-jniess",
                    FirstName = "James",
                    LastName = "Niessen",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 14,
                    Username = "COWDMP01\\lclinton",
                    FirstName = "Liz",
                    LastName = "Clinton",
                    isActive = true,
                    isDeleted = false
                },
                new Users
                {
                    UserId = 15,
                    Username = "COWDMP01\\cdzik",
                    FirstName = "Cody",
                    LastName = "Dzik",
                    isActive = true,
                    isDeleted = false
                },

                // EAY - Add additional Users HERE!
            };
            foreach (var user in usersList)
            {
                context.Users.AddOrUpdate(user);
            }

            // User Roles
            csrRole.Users.Clear();
            csrAdminRole.Users.Clear();
            foreach (var user in context.Users.Local)
            {
                csrRole.Users.Add(user);
                csrAdminRole.Users.Add(user);
            }

            // E-mail Templates
            var emailTemplateList = new List<EmailTemplate>
            {
                new EmailTemplate
                {
                    TemplateId = 1,
                    TemplateName = "WSS.Activation",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Activate Your Profile",
                    MessageBody = LoadEmailTemplate("AccountActivationLink.txt"),
                    Version = "1.0"
                },
                new EmailTemplate
                {
                    TemplateId = 2,
                    TemplateName = "WSS.ChangePassword",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Password Change Requested",
                    MessageBody =  LoadEmailTemplate("PasswordChangeRequested.txt"),
                    Version = "1.0"
                },
                new EmailTemplate
                {
                    TemplateId = 3,
                    TemplateName = "WSS.ChangePasswordNotification",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Password Updated",
                    MessageBody =  LoadEmailTemplate("PasswordChangeSuccessful.txt"),
                    Version = "1.0"
                },
                new EmailTemplate
                {
                    TemplateId = 4,
                    TemplateName = "WSS.ChangeEmail",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Email Address Updated",
                    MessageBody =  LoadEmailTemplate("EmailChangeConfirmation.txt"),
                    Version = "1.0"
                },
                new EmailTemplate
                {
                    TemplateId = 5,
                    TemplateName = "WSS.AccountActivatedNotification",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Welcome!",
                    MessageBody = LoadEmailTemplate("AccountActivatedNotification.txt"),
                    Version = "1.0"
                },
                 new EmailTemplate
                {
                    TemplateId = 6,
                    TemplateName = "WSS.AdditionalEmailAdded",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Email Notifications.",
                    MessageBody = LoadEmailTemplate("AdditionalEmailAdded.txt"),
                    Version = "1.0"
                },
                  new EmailTemplate
                {
                    TemplateId = 7,
                    TemplateName = "WSS.AdditionalEmailRemoved",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Email Notifications",
                    MessageBody = LoadEmailTemplate("AdditionalEmailRemoved.txt"),
                    Version = "1.0"
                },
                   new EmailTemplate
                {
                    TemplateId = 8,
                    TemplateName = "WSS.BillAvailablePrimaryAccountHolder",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - New Bill Now Available.",
                    MessageBody = LoadEmailTemplate("BillAvailablePrimaryAccountHolder.txt"),
                    Version = "1.0"
                },
                   new EmailTemplate
                {
                    TemplateId = 9,
                    TemplateName = "WSS.BillAvailableNotificationOnlyUser",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - New Bill Now Available",
                    MessageBody = LoadEmailTemplate("BillAvailableNotificationOnlyUser.txt"),
                    Version = "1.0"
                },
                     new EmailTemplate
                {
                    TemplateId = 10,
                    TemplateName = "WSS.RemianderNoticePrimaryAccountHolder",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Bill Past Due Reminder",
                    MessageBody = LoadEmailTemplate("RemianderNoticePrimaryAccountHolder.txt"),
                    Version = "1.0"
                },
                      new EmailTemplate
                {
                    TemplateId = 11,
                    TemplateName = "WSS.RemianderNoticeNotificationOnlyUser",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Bill Past Due Reminder",
                    MessageBody = LoadEmailTemplate("RemianderNoticeNotificationOnlyUser.txt"),
                    Version = "1.0"
                },
                        new EmailTemplate
                {
                    TemplateId = 12,
                    TemplateName = "WSS.Unsubscribe",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Profile Unsubscribed",
                    MessageBody = LoadEmailTemplate("Unsubscribed.txt"),
                    Version = "1.0"
                },
                          new EmailTemplate
                {
                    TemplateId = 13,
                    TemplateName = "WSS.LinkedUtilityAccountAdded",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Additional Utility Account Added",
                    MessageBody = LoadEmailTemplate("AdditionalUtilityAccountAdded.txt"),
                    Version = "1.0"
                },
                            new EmailTemplate
                {
                    TemplateId = 14,
                    TemplateName = "WSS.LinkedUtilityAccountRemoved",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Additional Utility Account Removed",
                    MessageBody = LoadEmailTemplate("AdditionalUtilityAccountRemoved.txt"),
                    Version = "1.0"
                },
                              new EmailTemplate
                {
                    TemplateId = 15,
                    TemplateName = "WSS.ChangeEmailRequested",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Email Address Change Requested",
                    MessageBody =  LoadEmailTemplate("EmailAddressChangeRequested.txt"),
                    Version = "1.0"
                },
                              new EmailTemplate
                {
                    TemplateId = 16,
                    TemplateName = "WSS.AdditionalEmailAddedPrimaryAccountHolder",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Email Notifications",
                    MessageBody = LoadEmailTemplate("AdditionalEmailAddedPrimaryAccountNotification.txt"),
                    Version = "1.0"
                },
                              new EmailTemplate
                {
                    TemplateId = 17,
                    TemplateName = "WSS.AdditionalEmailRemovedPrimaryAccountHolder",
                    DefaultFrom = "MyUtilityBill@winnipeg.ca",
                    Defaultcc = string.Empty,
                    Defaultbcc = string.Empty,
                    LastChangedDate = DateTime.Today,
                    Subject = "MyUtilityBill - Email Notifications",
                    MessageBody = LoadEmailTemplate("AdditionalEmailRemovedPrimaryAccountNotification.txt"),
                    Version = "1.0"
                }
            };

            foreach (var emailTemplate in emailTemplateList)
            {
                context.EmailTemplates.AddOrUpdate(emailTemplate);
            }
        }

        private string LoadEmailTemplate(string fileName)
        {
            var body = string.Empty;
            try
            {
                using (var sr = new StreamReader(Path.GetFullPath(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "\\EmailTemplates\\") + fileName))
                {
                    body = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                Trace.WriteLine(ex);
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.InnerException??ex.InnerException);
                throw ;
            }
            
            return body;
        }
    }
}