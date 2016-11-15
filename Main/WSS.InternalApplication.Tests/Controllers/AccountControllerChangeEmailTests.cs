using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using UtilityBilling.Data;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities.ActionLink;
using WSS.Data;
using WSS.Email.Service;
using WSS.InternalApplication.Controllers;
using IUnitOfWork = UtilityBilling.Data.IUnitOfWork;
using WssAccount = WSS.Data.WssAccount;
using WSS.Identity;

namespace WSS.InternalApplication.Tests.Controllers
{
    [TestClass()]
    public class AccountControllerChangeEmailTests
    {
        // Basic Test Data
        private const string TestOldEmailAddress = "bob@server.com";

        private const string TestUtilityAccountNumber = "9876543210";

        private readonly Mock<IUnitOfWork> _mockUtilityUnitOfWork;
        private readonly Mock<Data.IUnitOfWork> _mockWssUnitOfWork;
        private readonly Mock<ISendEmail> _mockEmailService;

        private readonly Mock<Data.IDataRepository<WssAccount>> _mockWssAccountRepository;        
        private Mock<UtilityBilling.Data.IDataRepository<UtilityAccount>> _mockUtilityAccountRepository;
        //private Mock<UtilityBilling.Data.IDataRepository<Status>> _mockUtilityStatusRepository;
        private Mock<Data.IDataRepository<AuditRecord>> _mockAuditRecordRepository;
        private Mock<Data.IDataRepository<AdditionalEmailAddress>> _mockadditionalemailrepository;
        private readonly Mock<Data.IDataRepository<LinkedUtilityAccount>> _mockLinkedUtilityAccountRepository;
        private readonly Mock<IAuditTransaction> _mockAuditTransaction;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IActionLinkManager> _mockActionLinkManager;

        private  readonly Mock<UserStore<WssIdentityUser>> _mockIdnetityUser;

        private readonly AccountController _accountController;

        public AccountControllerChangeEmailTests()
        {
            _mockWssUnitOfWork = new Mock<Data.IUnitOfWork>();
            _mockUtilityUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockAuditTransaction = new Mock<IAuditTransaction>();
            _mockEmailService = new Mock<ISendEmail>();
            _mockWssAccountRepository = new Mock<Data.IDataRepository<WssAccount>>();
            _mockLinkedUtilityAccountRepository = new Mock<Data.IDataRepository<LinkedUtilityAccount>>();
            _mockActionLinkManager = new Mock<IActionLinkManager>();
            _mockadditionalemailrepository=new Mock<Data.IDataRepository<AdditionalEmailAddress>>();
            _mockIdnetityUser=new Mock<UserStore<WssIdentityUser>>();
            _accountController = new AccountController(_mockWssUnitOfWork.Object, _mockUtilityUnitOfWork.Object, _mockMapper.Object, _mockAuditTransaction.Object, _mockEmailService.Object, _mockActionLinkManager.Object);

            
        }

        [TestInitialize]
        public void Initialize()
        {
            var mockWssAccountData = new List<WssAccount>()
            {
                new WssAccount() {WSSAccountId = 1,PrimaryEmailAddress = TestOldEmailAddress},
            }.AsQueryable();

            var mockWssAuditData = new List<AuditRecord>()
            {
                new AuditRecord() {AuditRecordId = 1}
            }.AsQueryable();
            var mockUtilityAccountData = new List<UtilityAccount>()
            {
                new UtilityAccount() { UtilityAccountId = 1, ccb_acct_id = TestUtilityAccountNumber }
            }.AsQueryable();
            var mockLinkedUtilityAccountData = new List<LinkedUtilityAccount>()
            {
                new LinkedUtilityAccount() { UtilityAccountId = 1, WssAccountId = 1}
            }.AsQueryable();

            

            var mockAdditionalemailData = new List<AdditionalEmailAddress>()
            {
                new AdditionalEmailAddress()
                {
                    WssAccountId = 1,
                    AdditionalEmailAddressId = 1,
                    EmailAddress = "bob@server.com"
                }
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

            _mockAuditRecordRepository = new Mock<Data.IDataRepository<AuditRecord>>();           
            _mockAuditRecordRepository.Setup(x => x.FindAll()).Returns(mockWssAuditData);
            _mockAuditRecordRepository.Setup(x => x.Insert(It.IsAny<AuditRecord>())).Verifiable();
            _mockAuditRecordRepository.Setup(x => x.Delete(It.IsAny<AuditRecord>())).Verifiable();

            _mockadditionalemailrepository.Setup(x => x.Delete(It.IsAny<AdditionalEmailAddress>())).Verifiable();
            _mockadditionalemailrepository.Setup(x => x.Insert(It.IsAny<AdditionalEmailAddress>())).Verifiable();

            _mockWssUnitOfWork.Setup(x => x.WssAccountRepository).Returns(_mockWssAccountRepository.Object);            
            _mockWssUnitOfWork.Setup(x => x.LinkedUtilityAccountsRepository).Returns(_mockLinkedUtilityAccountRepository.Object);
            _mockWssUnitOfWork.Setup(x => x.AdditionalEmailAddressRepository)
                .Returns(_mockadditionalemailrepository.Object);

            _mockUtilityUnitOfWork.Setup(x => x.UtilityAccountRepository).Returns(_mockUtilityAccountRepository.Object);
           

            _mockEmailService.Setup(x => x.AddSingleEmail(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            _mockAuditTransaction.Setup(x => x.AddAuditRecord(It.IsAny<AuditRecord>()));
            _mockadditionalemailrepository.Setup(x => x.FindAll()).Returns(mockAdditionalemailData);

            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();
            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            session.Setup(m => m["LinkedUtilityAccountId"]).Returns("1");
            _accountController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _accountController);
        }

        [TestMethod]
        public void ChangeEmail()
        {
            // ----------
            // Act
            // ----------

            object[] args = new object[2] {"test@test.com", It.IsAny<int>()};

            var mockIdnetityUserData = new List<WssIdentityUser>()
            {
                new WssIdentityUser() { Email = "test@test.com"}
            }.AsQueryable();

            _mockIdnetityUser.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new WssIdentityUser() {Email = "test@test.com",Id = "1212121212"}));

            PrivateObject objAccountController = new PrivateObject(_accountController);
            objAccountController.SetFieldOrProperty("_identityContext", new WssIdentityContext());
            objAccountController.SetFieldOrProperty("_identityUserManager",new WssIdentityUserManager( _mockIdnetityUser.Object));
            objAccountController.Invoke("ChangeEmail",args);          

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(objAccountController);
        }

        [TestMethod]
        public void ChangeEmailWithExistingAccount()
        {
            // ----------
            // Act
            // ----------

            object[] args = new object[2] { "test@test.com", 1 };

            var mockIdnetityUserData = new List<WssIdentityUser>()
            {
                new WssIdentityUser() { Email = "test@test.com"}
            }.AsQueryable();

            _mockIdnetityUser.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new WssIdentityUser() { Email = "test@test.com", Id = "1212121212" }));

            PrivateObject objAccountController = new PrivateObject(_accountController);
            objAccountController.SetFieldOrProperty("_identityContext", new WssIdentityContext());
            objAccountController.SetFieldOrProperty("_identityUserManager", new WssIdentityUserManager(_mockIdnetityUser.Object));
            objAccountController.Invoke("ChangeEmail", args);           

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(objAccountController);
        }

        [TestMethod]
        public void ChangeEmailWithExistingEmail()
        {
            // ----------
            // Act
            // ----------
            object[] args = new object[2] { "test@test.com", It.IsAny<int>() };

            var mockIdnetityUserData = new List<WssIdentityUser>()
            {
                new WssIdentityUser() { Email = "test@test.com"}
            }.AsQueryable();

            _mockIdnetityUser.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new WssIdentityUser() { Email = "test@test.com", Id = "1212121212" }));

            
            PrivateObject objAccountController = new PrivateObject(_accountController);
            objAccountController.SetFieldOrProperty("_identityContext", new WssIdentityContext());
            objAccountController.SetFieldOrProperty("_identityUserManager", new WssIdentityUserManager(_mockIdnetityUser.Object));
            objAccountController.Invoke("ChangeEmail", args);
                        
            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(objAccountController);
        }

        [TestMethod]
        public void ResendActivationLink()
        {
            // ----------
            // Act
            // ----------
            var actual = _accountController.ResendActivation(1);

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void UnlockAccount()
        {
            // ----------
            // Act
            // ----------
            var actual = _accountController.UnlockAccount(1);

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void AdditionalEmailWithInvalidAccount()
        {
            // ----------
            // Act
            // ----------
            var actual = _accountController.AdditionalEmail("test@test.com",2);

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void AdditionalEmailWithExistingAccount()
        {
            // ----------
            // Act
            // ----------
            var actual = _accountController.AdditionalEmail(It.IsAny<string>(), 1);

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void AdditionalEmailWithExistingEmail()
        {
            // ----------
            // Act
            // ----------
            var actual = _accountController.AdditionalEmail("bob@server.com", 1);

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(actual);
        }
    }
}