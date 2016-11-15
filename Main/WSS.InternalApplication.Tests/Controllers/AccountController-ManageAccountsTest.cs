using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using WSS.Common.Utilities.ActionLink;
using WSS.Data;
using WSS.InternalApplication.Controllers;
using WSS.InternalApplication.Infrastructure;

namespace WSS.InternalApplication.Tests.Controllers
{
    [TestClass]
    public class AccountControllerManageAccountsTest
    {
        private TestContext _testContextInstance;
        private AccountController _accountController;

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
        { }

        #endregion Additional test attributes

        public AccountControllerManageAccountsTest()
        {
            var linkedUaMockData = new List<LinkedUtilityAccount>()
            {
                new LinkedUtilityAccount()
                {
                    LinkedUtilityAccountId=1,
                    NickName="Test Nick Name",
                    UtilityAccountId=1,
                    WssAccountId=1,
                    DefaultAccount=true,
                    WssAccount=new WssAccount
                    {
                        IsActive = true,
                        WSSAccountId = 1,
                        PrimaryEmailAddress = "nsmith@gmail.com",
                        FailedResendActivationAttempts = 0,
                        AgreeTermsAndConditionsDate = DateTime.Now,
                        SecurityQuestion = "What was the name of your favorite childhood friend?",
                        SecurityQuestionAnswer = "test",
                        ActivationToken = null,
                        AuthenticationId ="7226679b-2d05-41a6-bbea-b45f7773f300",
                        WssAccountStatusCode = "ACT"
                    }
                }
            }.AsQueryable();

            var mockLinkedaccountRepository = new Mock<IDataRepository<LinkedUtilityAccount>>();
            mockLinkedaccountRepository.Setup(m => m.FindAll()).Returns(() => linkedUaMockData);


            var wssAuditRecordMockData = new List<AuditRecord>().AsQueryable();
            var mockAuditRecordRepository = new Mock<IDataRepository<AuditRecord>>();
            mockAuditRecordRepository.Setup(m => m.FindAll()).Returns(() => wssAuditRecordMockData);


            var wssUnitOfWork = new Mock<IUnitOfWork>();
            wssUnitOfWork.Setup(m => m.LinkedUtilityAccountsRepository).Returns(mockLinkedaccountRepository.Object);
            wssUnitOfWork.Setup(m => m.WssAuditRecordRepository).Returns(mockAuditRecordRepository.Object);
            wssUnitOfWork.Verify(m => m.Save("0"), Times.Never, "Save was not called");

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
            var ubUnitOfWork = new Mock<UtilityBilling.Data.IUnitOfWork>();
            ubUnitOfWork.Setup(m => m.UtilityAccountRepository).Returns(mockUtilityAccountRepository.Object);

            var auditTransaction = new AuditTransaction.Implementation.AuditTransaction(wssUnitOfWork.Object);

            var config = new MapperConfiguration(cfg => { cfg.AddProfile<AutomapperProfile>(); });
            var mapper = config.CreateMapper();
            _accountController = new AccountController(wssUnitOfWork.Object, ubUnitOfWork.Object, mapper, auditTransaction, null, new ActionLinkManager(wssUnitOfWork.Object, "http://localhost/"));
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();
            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            _accountController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _accountController);
        }

        [TestMethod]
        public void ManageIdDoesNotExist()
        {
            var result = _accountController._manage(0);
            var expected = (PartialViewResult)result;
            var recordcount = ((List<Models.ManageUtilityAccountListViewModel>)expected.Model).Count;
            Assert.AreEqual(recordcount, 0);
        }

        [TestMethod]
        public void ManageIdExists()
        {
            var result = _accountController._manage(1);
            var expected = (PartialViewResult)result;
            var recordcount = ((List<Models.ManageUtilityAccountListViewModel>)expected.Model).Count;
            Assert.AreNotEqual(recordcount, 0);
        }

        [TestMethod]
        public void DeleteLinkedUtilityAccountAccIdDoesNotExist()
        {
            var result = _accountController.DeleteLinkedUtilityAccount(null);
            Assert.IsNotNull(result);
            string expected = ((JsonResult)result).Data.ToString();
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void DeleteLinkedUtilityAccountAccIdExists()
        {
            var result = _accountController.DeleteLinkedUtilityAccount("3");
            Assert.IsNotNull(result);
            var expected = ((JsonResult)result).Data.ToString();
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void UpdateLinkedUtilityAccountAccIdDoesNotExist()
        {
            var result = _accountController.UpdateLinkedUtilityAccount(null, null, false);
            Assert.IsNotNull(result);
            string expected = ((JsonResult)result).Data.ToString();
            Assert.AreEqual(expected, "{ isUpdated = False }");
        }

        [TestMethod]
        public void UpdateLinkedUtilityAccountAccIdExists()
        {
            var result = _accountController.UpdateLinkedUtilityAccount("1", null, false);
            Assert.IsNotNull(result);
            string expected = ((JsonResult)result).Data.ToString();
            Assert.AreEqual(expected, "{ isUpdated = True }");
        }
    }
}