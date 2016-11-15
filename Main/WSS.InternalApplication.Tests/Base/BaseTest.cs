using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UtilityBilling.Data;
using WSS.Data;
using WSS.User.Service;
using Action = WSS.Data.Action;
using IUnitOfWork = UtilityBilling.Data.IUnitOfWork;
//using Status = UtilityBilling.Data.Status;

namespace WSS.InternalApplication.Tests.Base
{
    [TestClass]
    public class BaseTest
    {
        // Basic Test Data
        protected const string TestUtilityAccountNumber = "9876543210";

        protected const string TestEmailAddress = "bob@server.com";
        protected const string TestEmailAddress2 = "bob2@server.com";
        protected const string TestAuthenticationId = "TestAuthenticationId";
        protected static readonly Guid TestGuid = new Guid();
        protected static readonly LDAPUser TestLdapUser = new LDAPUser { Guid = new Guid(), UId = TestEmailAddress };
        protected Mock<Data.IDataRepository<Action>> _mockActionRepository;
        protected Mock<UtilityBilling.Data.IDataRepository<UtilityAccount>> _mockUtilityAccountRepository;
        //protected Mock<UtilityBilling.Data.IDataRepository<Status>> _mockUtilityStatusRepository;

        protected Mock<IUnitOfWork> _mockUtilityUnitOfWork;

        protected Mock<Data.IDataRepository<WssAccount>> _mockWssAccountRepository;
        protected Mock<Data.IDataRepository<LinkedUtilityAccount>> _mockWssLinkedUtilityAccountRepository;
        //protected Mock<Data.IDataRepository<Data.Status>> _mockWssStatusRepository;
        protected Mock<Data.IDataRepository<AdditionalEmailAddress>> _mockAdditionalEmailAddressRepository;
        protected Mock<Data.IUnitOfWork> _mockWssUnitOfWork;
        protected Mock<IWssUserManager> _mockWssUserManager;

        [TestInitialize]
        public void Initialize()
        {
            // App Settings
            ConfigurationManager.AppSettings["WSS.LDAP.Bypass"] = "true";
            ConfigurationManager.AppSettings["BaseUrl"] = "http://testurl/";

            // Data
            var mockWssAccountData = new List<WssAccount>
            {
                new WssAccount {WSSAccountId = 1, PrimaryEmailAddress = TestEmailAddress, AuthenticationId = TestAuthenticationId, IsActive = true}
            }.AsQueryable();
            var mockWssLinkedUtilityAccountsData = new List<LinkedUtilityAccount>().AsQueryable();
            //var mockWssStatusData = new List<Data.Status>
            //{
            //    new Data.Status {StatusId = 1, StatusDomain = "Account", StatusName = "Not Registered"},
            //    new Data.Status {StatusId = 2, StatusDomain = "Account", StatusName = "Registered"},
            //    new Data.Status {StatusId = 3, StatusDomain = "Account", StatusName = "Active"},
            //    new Data.Status {StatusId = 4, StatusDomain = "Account", StatusName = "Inactive"},
            //    new Data.Status {StatusId = 5, StatusDomain = "Account", StatusName = "Locked"}
            //}.AsQueryable();

            var mockAdditionalEmailAddressData = new List<AdditionalEmailAddress>()
            {
                new AdditionalEmailAddress() { AdditionalEmailAddressId = 1, WssAccountId = 1, EmailAddress = TestEmailAddress2}
            }.AsQueryable();

            var mockUtilityAccountData = new List<UtilityAccount>
            {
                new UtilityAccount {UtilityAccountId = 1, ccb_acct_id = TestUtilityAccountNumber}
            }.AsQueryable();

            //var mockUtilitySatusData = new List<Status>
            //{
            //    new Status {StatusID = 1, StatusName = "Active", StatusDomain = "Account"}
            //}.AsQueryable();

            // Repository
            _mockWssAccountRepository = new Mock<Data.IDataRepository<WssAccount>>();
            _mockWssAccountRepository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((int p) => mockWssAccountData.FirstOrDefault(x => x.WSSAccountId == p));
            _mockWssAccountRepository.Setup(x => x.FindAll()).Returns(mockWssAccountData);
            _mockWssAccountRepository.Setup(x => x.Insert(It.IsAny<WssAccount>())).Verifiable();
            _mockWssAccountRepository.Setup(x => x.Delete(It.IsAny<WssAccount>())).Verifiable();

            _mockActionRepository = new Mock<Data.IDataRepository<Action>>();
            _mockActionRepository.Setup(x => x.Insert(It.IsAny<Action>())).Verifiable();

            //_mockWssStatusRepository = new Mock<Data.IDataRepository<Data.Status>>();
            //_mockWssStatusRepository.Setup(x => x.Get(It.IsAny<int>()))
            //    .Returns((int p) => mockWssStatusData.FirstOrDefault(x => x.StatusId == p));
            //_mockWssStatusRepository.Setup(x => x.FindAll()).Returns(mockWssStatusData);

            _mockWssLinkedUtilityAccountRepository = new Mock<Data.IDataRepository<LinkedUtilityAccount>>();
            _mockWssLinkedUtilityAccountRepository.Setup(x => x.FindAll()).Returns(mockWssLinkedUtilityAccountsData);
            _mockWssLinkedUtilityAccountRepository.Setup(x => x.Insert(It.IsAny<LinkedUtilityAccount>())).Verifiable();
            _mockWssLinkedUtilityAccountRepository.Setup(x => x.Delete(It.IsAny<LinkedUtilityAccount>())).Verifiable();

            _mockAdditionalEmailAddressRepository = new Mock<Data.IDataRepository<AdditionalEmailAddress>>();
            _mockAdditionalEmailAddressRepository.Setup(x => x.FindAll()).Returns(mockAdditionalEmailAddressData);

            _mockUtilityAccountRepository = new Mock<UtilityBilling.Data.IDataRepository<UtilityAccount>>();
            _mockUtilityAccountRepository.Setup(x => x.Get(It.IsAny<UtilityAccount>()))
                .Returns((int p) => mockUtilityAccountData.FirstOrDefault(x => x.UtilityAccountId == p));
            _mockUtilityAccountRepository.Setup(x => x.FindAll()).Returns(mockUtilityAccountData);
            _mockUtilityAccountRepository.Setup(x => x.Insert(It.IsAny<UtilityAccount>())).Verifiable();
            _mockUtilityAccountRepository.Setup(x => x.Delete(It.IsAny<UtilityAccount>())).Verifiable();

            //_mockUtilityStatusRepository = new Mock<UtilityBilling.Data.IDataRepository<Status>>();
            //_mockUtilityStatusRepository.Setup(x => x.Get(It.IsAny<Status>()))
            //    .Returns((int p) => mockUtilitySatusData.FirstOrDefault(x => x.StatusID == p));
            //_mockUtilityStatusRepository.Setup(x => x.FindAll()).Returns(mockUtilitySatusData);

            // Unit of work
            _mockWssUnitOfWork = new Mock<Data.IUnitOfWork>();
            _mockWssUnitOfWork.Setup(x => x.WssAccountRepository).Returns(_mockWssAccountRepository.Object);
            //_mockWssUnitOfWork.Setup(x => x.StatusRepository).Returns(_mockWssStatusRepository.Object);
            _mockWssUnitOfWork.Setup(x => x.LinkedUtilityAccountsRepository)
                .Returns(_mockWssLinkedUtilityAccountRepository.Object);
            _mockWssUnitOfWork.Setup(x => x.ActionDataRepository).Returns(_mockActionRepository.Object);
            _mockWssUnitOfWork.Setup(x => x.AdditionalEmailAddressRepository)
                .Returns(_mockAdditionalEmailAddressRepository.Object);

            _mockUtilityUnitOfWork = new Mock<IUnitOfWork>();
            _mockUtilityUnitOfWork.Setup(x => x.UtilityAccountRepository).Returns(_mockUtilityAccountRepository.Object);
            //_mockUtilityUnitOfWork.Setup(x => x.Status).Returns(_mockUtilityStatusRepository.Object);

            // User Manager
            _mockWssUserManager = new Mock<IWssUserManager>();
            _mockWssUserManager.Setup(x => x.AddUser(It.IsAny<LDAPUser>())).Verifiable();
            _mockWssUserManager.Setup(x => x.FindUser(It.IsAny<string>())).Returns(TestLdapUser).Verifiable();
            _mockWssUserManager.Setup(x => x.DeleteUser(It.IsAny<LDAPUser>())).Verifiable();
        }
    }
}