using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WSS.Data;
using WSS.Email.Service;
using WSS.InternalApplication.Controllers;
using WSS.InternalApplication.Infrastructure;
using WSS.InternalApplication.Models;
//using WWDCommon.Data;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using WSS.Identity;

namespace WSS.InternalApplication.Tests.Controllers
{
    /// <summary>
    /// Summary description for AccountController_ProfileTests
    /// </summary>
    [TestClass]
    public class AccountControllerProfileTests
    {

        private readonly Mock<UserStore<WssIdentityUser>> _mockIdnetityUser;
        public AccountControllerProfileTests()
        {
            _mockIdnetityUser = new Mock<UserStore<WssIdentityUser>>();

            var linkedUaMockData = new List<LinkedUtilityAccount>()
            {
                new LinkedUtilityAccount()
                {
                    LinkedUtilityAccountId=1,
                    NickName="Test Nick Name",
                    UtilityAccountId=1,
                    WssAccount= new WssAccount(){WSSAccountId = 1,WssAccountStatusCode="REG",PrimaryEmailAddress = "abc@gmail.com"},
                    DefaultAccount=true,

                }

            }.AsQueryable();

            var mockLinkedaccountRepository = new Mock<IDataRepository<LinkedUtilityAccount>>();
            mockLinkedaccountRepository.Setup(m => m.FindAll()).Returns(() => linkedUaMockData);


            var wssAccountockData = new List<WssAccount>()
            {
                new WssAccount()
                {
                    //ActivationToken="",
                    AgreeTermsAndConditionsDate=System.DateTime.Now,
                    //ChangePasswordToken="",
                    FailedResendActivationAttempts=0,
                    PrimaryEmailAddress="",
                    WssAccountStatusCode="NOTREG",
                    WSSAccountId=1
                }
            }.AsQueryable();

            var mockWssccountRepository = new Mock<IDataRepository<WssAccount>>();
            mockWssccountRepository.Setup(m => m.FindAll()).Returns(() => wssAccountockData);



            var auditRecordMockData = new List<AuditRecord>()
            {
                new AuditRecord()
            }.AsQueryable();
            var mockAuditRecordRepository = new Mock<IDataRepository<AuditRecord>>();
            mockAuditRecordRepository.Setup(m => m.FindAll()).Returns(() => auditRecordMockData);

            var subscriptionTransactionockData = new List<SubscriptionTransaction>()
            {
                new SubscriptionTransaction()
            }.AsQueryable();
            var mockubscriptionTransactionRepository = new Mock<IDataRepository<SubscriptionTransaction>>();
            mockubscriptionTransactionRepository.Setup(m => m.FindAll()).Returns(() => subscriptionTransactionockData);

            var wssUnitOfWork = new Mock<IUnitOfWork>();
            wssUnitOfWork.Setup(m => m.LinkedUtilityAccountsRepository).Returns(mockLinkedaccountRepository.Object);
            wssUnitOfWork.Setup(m => m.WssAccountRepository).Returns(mockWssccountRepository.Object);
            wssUnitOfWork.Setup(m => m.WssAuditRecordRepository).Returns(mockAuditRecordRepository.Object);
            wssUnitOfWork.Setup(m => m.SubscriptionTransactionRepository).Returns(mockubscriptionTransactionRepository.Object);
            wssUnitOfWork.Verify(m => m.Save("Test"), Times.Never, "Save was not called");
            var utilityAccountMockData = new List<UtilityBilling.Data.UtilityAccount>()
            {
                new UtilityBilling.Data.UtilityAccount()
                {
                    UtilityAccountId=1,
                    ccb_acct_id="0123456789",
                    UtilityAccountSourceCode="WSS",
                    PrimaryAccountHolderName="Test Name",
                }
            }.AsQueryable();
            var mockUtilityAccountRepository = new Mock<UtilityBilling.Data.IDataRepository<UtilityBilling.Data.UtilityAccount>>();
            mockUtilityAccountRepository.Setup(m => m.FindAll()).Returns(() => utilityAccountMockData);

            var accountUnitOfWork = new Mock<UtilityBilling.Data.IUnitOfWork>();
            accountUnitOfWork.Setup(m => m.UtilityAccountRepository).Returns(() => mockUtilityAccountRepository.Object);

            //var commonUnitOfWork = new WWDCommon.Data.UnitOfWork(new WDDCommonContext());
            var auditTransaction = new AuditTransaction.Implementation.AuditTransaction(wssUnitOfWork.Object);

            System.Action<string, string> addSingleMail = Display;

            var sendMail = new Mock<ISendEmail>();
            sendMail.Setup(m => m.AddSingleEmail("WSS.Unsubscribe", $"TO={1}")).Callback(addSingleMail);

            var config = new MapperConfiguration(cfg => { cfg.AddProfile<AutomapperProfile>(); });
            var mapper = config.CreateMapper();

            var userMock = new Mock<IPrincipal>();
            var identitymock = new Mock<IIdentity>() { Name = "Test" };
            userMock.Setup(m => m.Identity).Returns(identitymock.Object);
            var fakeHttpContext = new Mock<HttpContextBase>();
            fakeHttpContext.Setup(t => t.User).Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(fakeHttpContext.Object);

            _accountController = new AccountController(wssUnitOfWork.Object, accountUnitOfWork.Object, mapper,
                auditTransaction, sendMail.Object, null);
            _accountController.ControllerContext = controllerContextMock.Object;

        }

        void Display(string a, string b)
        { }

        private TestContext _testContextInstance;
        private readonly AccountController _accountController;
        private WssAccount _entity;
        private AccountViewModel _model;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            // empty entity - set it in each test
            _entity = new WssAccount();

            // default model, booleans are false by default
            _model = new AccountViewModel
            {
                ShowRegisterAccount = false,
                ShowChangeEmail = false,
                ShowResendActivation = false,
                ShowResetPassword = false,
                ShowUnlockAccount = false,
                ShowRestoreAccount = false,
                ShowUnSubscribe = false,
                ShowLockAccount = false
            };
        }

        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion Additional test attributes

        [TestMethod]
        public void NotRegistered()
        {
            // arrange
            // HACK: EAY Should not use magic string for this!!
            _entity.WssAccountStatusCode = "NOTREG"; // Not Registered

            // act
            var updatedModel = _accountController.SetProfileTabVisibility(_entity, _model);

            // assert
            Assert.AreEqual(true, updatedModel.ShowRegisterAccount);
            Assert.AreEqual(false, updatedModel.ShowChangeEmail);
            Assert.AreEqual(false, updatedModel.ShowResendActivation);
            Assert.AreEqual(false, updatedModel.ShowResetPassword);
            Assert.AreEqual(false, updatedModel.ShowUnlockAccount);
            Assert.AreEqual(false, updatedModel.ShowRestoreAccount);
            Assert.AreEqual(false, updatedModel.ShowUnSubscribe);
            Assert.AreEqual(false, updatedModel.ShowLockAccount);
        }

        [TestMethod]
        public void Registered()
        {
            // arrange
            // HACK: EAY Should not use magic string for this!!
            _entity.WssAccountStatusCode = "REG"; // Registered

            // act
            var updatedModel = _accountController.SetProfileTabVisibility(_entity, _model);

            // assert
            Assert.AreEqual(false, updatedModel.ShowRegisterAccount);
            Assert.AreEqual(true, updatedModel.ShowChangeEmail);
            Assert.AreEqual(true, updatedModel.ShowResendActivation);
            Assert.AreEqual(false, updatedModel.ShowResetPassword);
            Assert.AreEqual(false, updatedModel.ShowUnlockAccount);
            Assert.AreEqual(false, updatedModel.ShowRestoreAccount);
            Assert.AreEqual(false, updatedModel.ShowUnSubscribe);
            Assert.AreEqual(false, updatedModel.ShowLockAccount);
        }

        [TestMethod]
        public void Active()
        {
            // arrange
            // HACK: EAY Should not use magic string for this!!
            _entity.WssAccountStatusCode = "ACT"; // Active

            // act
            var updatedModel = _accountController.SetProfileTabVisibility(_entity, _model);

            // assert
            Assert.AreEqual(false, updatedModel.ShowRegisterAccount);
            Assert.AreEqual(true, updatedModel.ShowChangeEmail);
            Assert.AreEqual(false, updatedModel.ShowResendActivation);
            Assert.AreEqual(true, updatedModel.ShowResetPassword);
            Assert.AreEqual(false, updatedModel.ShowUnlockAccount);
            Assert.AreEqual(false, updatedModel.ShowRestoreAccount);
            Assert.AreEqual(true, updatedModel.ShowUnSubscribe);
            Assert.AreEqual(false, updatedModel.ShowLockAccount);
        }

        [TestMethod]
        public void UnSubscribed()
        {
            // arrange
            // HACK: EAY Should not use magic string for this!!
            _entity.WssAccountStatusCode = "UNSUB"; // Inactive

            // act
            var updatedModel = _accountController.SetProfileTabVisibility(_entity, _model);

            // assert
            Assert.AreEqual(true, updatedModel.ShowRegisterAccount);
            Assert.AreEqual(false, updatedModel.ShowChangeEmail);
            Assert.AreEqual(false, updatedModel.ShowResendActivation);
            Assert.AreEqual(false, updatedModel.ShowResetPassword);
            Assert.AreEqual(false, updatedModel.ShowUnlockAccount);
            Assert.AreEqual(false, updatedModel.ShowRestoreAccount);
            Assert.AreEqual(false, updatedModel.ShowUnSubscribe);
            Assert.AreEqual(false, updatedModel.ShowLockAccount);
        }

        [TestMethod]
        public void Locked()
        {
            // arrange
            // HACK: EAY Should not use magic string for this!!
            _entity.WssAccountStatusCode = "LCKD"; // Locked

            // act
            var updatedModel = _accountController.SetProfileTabVisibility(_entity, _model);

            // assert
            Assert.AreEqual(false, updatedModel.ShowRegisterAccount);
            Assert.AreEqual(false, updatedModel.ShowChangeEmail);
            Assert.AreEqual(true, updatedModel.ShowResendActivation);
            Assert.AreEqual(true, updatedModel.ShowResetPassword);
            Assert.AreEqual(false, updatedModel.ShowUnlockAccount);
            Assert.AreEqual(false, updatedModel.ShowRestoreAccount);
            Assert.AreEqual(false, updatedModel.ShowUnSubscribe);
            Assert.AreEqual(false, updatedModel.ShowLockAccount);
        }

        [TestMethod]
        public void UbscribeUtilityAccountIdIsNull()
        {
            var result = _accountController.Unsubscribe(null, null, null, null);
            Assert.AreEqual("{ isDeleted = False }", result.Data.ToString());
        }

        [TestMethod]
        public void UnsubscribeUtilityAccountDoesNotExists()
        {
            var result = _accountController.Unsubscribe(0, null, null, null);
            Assert.AreEqual("{ isDeleted = False }", result.Data.ToString());
        }

        [TestMethod]
        public void UnsubscribeUtilityAccountExists()
        {
            

            _mockIdnetityUser.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new WssIdentityUser() { Email = "test@test.com", Id = "1212121212" }));

            var objAccountController = new PrivateObject(_accountController);
            objAccountController.SetFieldOrProperty("_identityContext", new WssIdentityContext());
            objAccountController.SetFieldOrProperty("_identityUserManager", new WssIdentityUserManager(_mockIdnetityUser.Object));
            var result = _accountController.Unsubscribe(1, null, null, null);
            Assert.AreEqual("{ isdeleted = true }", result.Data.ToString().ToLower());
        }
    }
}