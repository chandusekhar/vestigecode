using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UtilityBilling.Data;
using WSS.Data;
using Action = WSS.Data.Action;
using IUnitOfWork = UtilityBilling.Data.IUnitOfWork;

namespace WSS.Common.Utilities.Tests.Base
{
    [TestClass]
    public class BaseTest
    {
        // Basic Test Data
        protected const string TestUtilityAccountNumber = "9876543210";

        protected const int TestWssAccountId = 1;

        protected const string TestIdentityUserId = "1";
        protected const string TestEmailAddress = "bob@server.com";
        protected const string TestEmailAddress2 = "bob2@server.com";
        protected const string TestAuthenticationId = "TestAuthenticationId";
        protected const string TestPassword1 = "passw0rd";
        protected const string TestPassword2 = "passw1rd";
        protected static readonly Guid TestGuid = new Guid();

        protected Mock<Data.IDataRepository<Action>> MockActionRepository;
        protected Mock<UtilityBilling.Data.IDataRepository<UtilityAccount>> MockUtilityAccountRepository;

        protected Mock<IUnitOfWork> MockUtilityUnitOfWork;

        protected Mock<Data.IDataRepository<WssAccount>> MockWssAccountRepository;
        protected Mock<Data.IDataRepository<LinkedUtilityAccount>> MockWssLinkedUtilityAccountRepository;
        protected Mock<Data.IDataRepository<AdditionalEmailAddress>> MockAdditionalEmailAddressRepository;
        protected Mock<Data.IUnitOfWork> MockWssUnitOfWork;

        [TestInitialize]
        public void Initialize()
        {
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
        }
    }
}