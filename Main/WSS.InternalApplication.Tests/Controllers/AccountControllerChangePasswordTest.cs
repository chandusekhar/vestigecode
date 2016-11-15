using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using UtilityBilling.Data;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities.ActionLink;
using WSS.Data;
using WSS.Email.Service;
using WSS.InternalApplication.Controllers;
using IUnitOfWork = UtilityBilling.Data.IUnitOfWork;
using WssAccount = WSS.Data.WssAccount;

namespace WSS.InternalApplication.Tests.Controllers
{
    [TestClass()]
    public class AccountControllerChangePasswordTest
    {
        // Basic Test Data
        private const string TestEmailAddress = "bob@server.com";

        private const string TestUtilityAccountNumber = "9876543210";

        private readonly Mock<IUnitOfWork> _mockUtilityUnitOfWork;
        private readonly Mock<Data.IUnitOfWork> _mockWssUnitOfWork;
        private readonly Mock<ISendEmail> _mockEmailService;

        private readonly Mock<Data.IDataRepository<WssAccount>> _mockWssAccountRepository;        
        private Mock<UtilityBilling.Data.IDataRepository<UtilityAccount>> _mockUtilityAccountRepository;       
        private readonly Mock<WSS.Data.IDataRepository<LinkedUtilityAccount>> _mockLinkedUtilityAccountRepository;
        private readonly Mock<IAuditTransaction> _mockAuditTransaction;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IActionLinkManager> _mockActionLinkManager;

        private readonly AccountController _accountController;

        public AccountControllerChangePasswordTest()
        {
            _mockWssUnitOfWork = new Mock<Data.IUnitOfWork>();
            _mockUtilityUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockAuditTransaction = new Mock<IAuditTransaction>();
            _mockEmailService = new Mock<ISendEmail>();
            _mockWssAccountRepository = new Mock<Data.IDataRepository<WssAccount>>();
            _mockLinkedUtilityAccountRepository = new Mock<WSS.Data.IDataRepository<LinkedUtilityAccount>>();
            _mockActionLinkManager = new Mock<IActionLinkManager>();
            _accountController = new AccountController(_mockWssUnitOfWork.Object, _mockUtilityUnitOfWork.Object, _mockMapper.Object, _mockAuditTransaction.Object, _mockEmailService.Object, _mockActionLinkManager.Object);
        }

        [TestInitialize]
        public void Initialize()
        {           

            var mockWssAccountData = new List<WssAccount>()
            {
                new WssAccount() {WSSAccountId = 1,PrimaryEmailAddress = TestEmailAddress}
            }.AsQueryable();

            var mockUtilityAccountData = new List<UtilityAccount>()
            {
                new UtilityAccount() { UtilityAccountId = 1, ccb_acct_id = TestUtilityAccountNumber }
            }.AsQueryable();
            var mockLinkedUtilityAccountData = new List<LinkedUtilityAccount>()
            {
                new LinkedUtilityAccount() { UtilityAccountId = 1, WssAccountId = 1}
            }.AsQueryable();          
            // Repository
            _mockWssAccountRepository.Setup(x => x.Get(It.IsAny<WssAccount>())).Returns((int p) => mockWssAccountData.FirstOrDefault(x => x.WSSAccountId == p));
            _mockWssAccountRepository.Setup(x => x.FindAll()).Returns(mockWssAccountData);
            _mockWssAccountRepository.Setup(x => x.Insert(It.IsAny<WssAccount>())).Verifiable();
            _mockWssAccountRepository.Setup(x => x.Delete(It.IsAny<WssAccount>())).Verifiable();
            
            _mockUtilityAccountRepository = new Mock<UtilityBilling.Data.IDataRepository<UtilityAccount>>();
            _mockUtilityAccountRepository.Setup(x => x.Get(It.IsAny<UtilityAccount>())).Returns((int p) => mockUtilityAccountData.FirstOrDefault(x => x.UtilityAccountId == p));
            _mockUtilityAccountRepository.Setup(x => x.FindAll()).Returns(mockUtilityAccountData);
            _mockUtilityAccountRepository.Setup(x => x.Insert(It.IsAny<UtilityAccount>())).Verifiable();
            _mockUtilityAccountRepository.Setup(x => x.Delete(It.IsAny<UtilityAccount>())).Verifiable();

            _mockLinkedUtilityAccountRepository.Setup(x => x.FindAll()).Returns(mockLinkedUtilityAccountData);

            _mockWssUnitOfWork.Setup(x => x.WssAccountRepository).Returns(_mockWssAccountRepository.Object);           
            _mockWssUnitOfWork.Setup(x => x.LinkedUtilityAccountsRepository).Returns(_mockLinkedUtilityAccountRepository.Object);

            _mockUtilityUnitOfWork.Setup(x => x.UtilityAccountRepository).Returns(_mockUtilityAccountRepository.Object);           

            _mockEmailService.Setup(x => x.AddSingleEmail(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            _mockAuditTransaction.Setup(x => x.AddAuditRecord(It.IsAny<AuditRecord>()));
        }

        [TestMethod]
        public void ChangePasswordNoAccountId()
        {
            // ----------
            // Act
            // ----------
            var actual = _accountController.ChangePassword(It.IsAny<int>());

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ChangePasswordWithExistingAccount()
        {
            // ----------
            // Act
            // ----------
            var actual = _accountController.ChangePassword(1);

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(actual);
        }
    }
}