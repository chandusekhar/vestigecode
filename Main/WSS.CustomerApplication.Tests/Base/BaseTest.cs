using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UtilityBilling.Data;
using WSS.AuditTransaction.Interfaces;
using WSS.Data;
using WSS.Email.Service;
using WSS.Identity;
using Action = WSS.Data.Action;
using IUnitOfWork = UtilityBilling.Data.IUnitOfWork;

//using Status = UtilityBilling.Data.Status;

namespace WSS.CustomerApplication.Tests.Base
{
    [TestClass]
    public class BaseTest
    {
        // Basic Test Data
        protected const string TestUtilityAccountNumber = "9876543210";

        protected const int TestWssAccountId = 1;

        protected const string TestIdentityUserId = "1";
        protected const string TestIdentityUserName = "TestName";
        protected const string TestEmailAddress = "bob@server.com";
        protected const string TestEmailAddress2 = "bob2@server.com";
        protected const string TestAuthenticationId = "TestAuthenticationId";
        protected const string TestPassword1 = "passw0rd";
        protected const string TestPassword2 = "passw1rd";
        protected static readonly Guid TestGuid = new Guid();

        protected Mock<WssIdentityUser> MockWssIdentityUser;

        // Mock Data Repositories
        protected Mock<Data.IDataRepository<Action>> MockActionRepository;
        protected Mock<UtilityBilling.Data.IDataRepository<UtilityAccount>> MockUtilityAccountRepository;
        protected Mock<Data.IDataRepository<WssAccount>> MockWssAccountRepository;
        protected Mock<Data.IDataRepository<LinkedUtilityAccount>> MockWssLinkedUtilityAccountRepository;
        protected Mock<Data.IDataRepository<AdditionalEmailAddress>> MockAdditionalEmailAddressRepository;

        protected Mock<IUnitOfWork> MockUtilityUnitOfWork;

        
        protected Mock<Data.IUnitOfWork> MockWssUnitOfWork;

        protected Mock<ISendEmail> MockSendMail;
        protected Mock<IAuditTransaction> MockAuditTransaction;

        protected Mock<IWssIdentityUserManager> MockUserManager;

        [TestInitialize]
        public void Initialize()
        {
            // App Settings
            ConfigurationManager.AppSettings["WSS.LDAP.Bypass"] = "true";
            ConfigurationManager.AppSettings["BaseUrl"] = "http://testurl/";

            // Data
            var mockWssAccountData = new List<WssAccount>
            {
                new WssAccount {WSSAccountId = TestWssAccountId, PrimaryEmailAddress = TestEmailAddress, AuthenticationId = TestAuthenticationId, IsActive = true}
            }.AsQueryable();
            var mockWssLinkedUtilityAccountsData = new List<LinkedUtilityAccount>().AsQueryable();

            var mockAdditionalEmailAddressData = new List<AdditionalEmailAddress>()
            {
                new AdditionalEmailAddress() { AdditionalEmailAddressId = 1, WssAccountId = 1, EmailAddress = TestEmailAddress2}
            }.AsQueryable();

            var mockUtilityAccountData = new List<UtilityAccount>
            {
                new UtilityAccount {UtilityAccountId = 1, ccb_acct_id = TestUtilityAccountNumber}
            }.AsQueryable();

            // Repository
            MockWssAccountRepository = new Mock<Data.IDataRepository<WssAccount>>();
            MockWssAccountRepository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((int p) => mockWssAccountData.FirstOrDefault(x => x.WSSAccountId == p));
            MockWssAccountRepository.Setup(x => x.FindAll()).Returns(mockWssAccountData);
            MockWssAccountRepository.Setup(x => x.Insert(It.IsAny<WssAccount>())).Verifiable();
            MockWssAccountRepository.Setup(x => x.Delete(It.IsAny<WssAccount>())).Verifiable();

            MockActionRepository = new Mock<Data.IDataRepository<Action>>();
            MockActionRepository.Setup(x => x.Insert(It.IsAny<Action>())).Verifiable();

            MockWssLinkedUtilityAccountRepository = new Mock<Data.IDataRepository<LinkedUtilityAccount>>();
            MockWssLinkedUtilityAccountRepository.Setup(x => x.FindAll()).Returns(mockWssLinkedUtilityAccountsData);
            MockWssLinkedUtilityAccountRepository.Setup(x => x.Insert(It.IsAny<LinkedUtilityAccount>())).Verifiable();
            MockWssLinkedUtilityAccountRepository.Setup(x => x.Delete(It.IsAny<LinkedUtilityAccount>())).Verifiable();

            MockAdditionalEmailAddressRepository = new Mock<Data.IDataRepository<AdditionalEmailAddress>>();
            MockAdditionalEmailAddressRepository.Setup(x => x.FindAll()).Returns(mockAdditionalEmailAddressData);

            MockUtilityAccountRepository = new Mock<UtilityBilling.Data.IDataRepository<UtilityAccount>>();
            MockUtilityAccountRepository.Setup(x => x.Get(It.IsAny<UtilityAccount>()))
                .Returns((int p) => mockUtilityAccountData.FirstOrDefault(x => x.UtilityAccountId == p));
            MockUtilityAccountRepository.Setup(x => x.FindAll()).Returns(mockUtilityAccountData);
            MockUtilityAccountRepository.Setup(x => x.Insert(It.IsAny<UtilityAccount>())).Verifiable();
            MockUtilityAccountRepository.Setup(x => x.Delete(It.IsAny<UtilityAccount>())).Verifiable();

            // Unit of work
            MockWssUnitOfWork = new Mock<Data.IUnitOfWork>();
            MockWssUnitOfWork.Setup(x => x.WssAccountRepository).Returns(MockWssAccountRepository.Object);
            MockWssUnitOfWork.Setup(x => x.LinkedUtilityAccountsRepository)
                .Returns(MockWssLinkedUtilityAccountRepository.Object);
            MockWssUnitOfWork.Setup(x => x.ActionDataRepository).Returns(MockActionRepository.Object);
            MockWssUnitOfWork.Setup(x => x.AdditionalEmailAddressRepository)
                .Returns(MockAdditionalEmailAddressRepository.Object);

            MockUtilityUnitOfWork = new Mock<IUnitOfWork>();
            MockUtilityUnitOfWork.Setup(x => x.UtilityAccountRepository).Returns(MockUtilityAccountRepository.Object);

            MockSendMail = new Mock<ISendEmail>();
            MockSendMail.Setup(m => m.AddSingleEmail("WSS.Unsubscribe", $"TO={1}"));

            MockAuditTransaction = new Mock<IAuditTransaction>();
            MockAuditTransaction.Setup(m => m.AddAuditRecord(It.IsAny<AuditRecord>()));

            MockWssIdentityUser = new Mock<WssIdentityUser>();
            MockWssIdentityUser.Setup(m => m.Id).Returns(TestIdentityUserId);
            MockWssIdentityUser.Setup(m => m.UserName).Returns(TestEmailAddress);
            MockWssIdentityUser.Setup(m => m.Email).Returns(TestEmailAddress);

            var testIdentityUsers = new List<WssIdentityUser>() { MockWssIdentityUser.Object};

            MockUserManager = new Mock<IWssIdentityUserManager>();
            MockUserManager.Setup(m => m.FindByEmail(It.IsAny<string>()))
                .Returns((string p) => testIdentityUsers.FirstOrDefault(x => x.Email.Equals(p))).Verifiable();

            MockUserManager.Setup(m => m.FindById(It.IsAny<string>()))
               .Returns((string p) => testIdentityUsers.FirstOrDefault(x => x.Id.Equals(p))).Verifiable();

            MockUserManager.Setup(m => m.ChangePassword(TestIdentityUserId, TestPassword1, TestPassword2))
                .Returns(IdentityResult.Success).Verifiable();

            var context = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            context.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
           // context.SetupGet(x => x.User.Identity.GetUserId()).Returns(TestIdentityUserId);
            mockIdentity.Setup(x => x.Name).Returns(TestIdentityUserName);
        }
    }
}