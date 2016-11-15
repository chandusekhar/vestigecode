using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using WSS.Data;
using WSS.InternalApplication.Controllers;

namespace WSS.InternalApplication.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTest
    {
        private TestContext _testContextInstance;
        private SearchController _searchController;

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

        public SearchControllerTest()
        {
            var linkedUaMockData = new List<LinkedUtilityAccount>()
            {
                new LinkedUtilityAccount()
                {
                    LinkedUtilityAccountId=1,
                    NickName="Test Nick Name",
                    UtilityAccountId=1,
                    WssAccountId=1,
                    WssAccount=   new WssAccount() {
                          WSSAccountId=1,
                    PrimaryEmailAddress="abc@gmial.com",
                    WssAccountStatusCode="REG",
                    FailedResendActivationAttempts=0
                    }
                    ,
                    DefaultAccount=true
                }
            }.AsQueryable();
            var mockLinkedaccountRepository = new Mock<IDataRepository<LinkedUtilityAccount>>();
            mockLinkedaccountRepository.Setup(m => m.FindAll()).Returns(() => linkedUaMockData);
            var wssUnitOfWork = new Mock<IUnitOfWork>();
            wssUnitOfWork.Setup(m => m.LinkedUtilityAccountsRepository).Returns(mockLinkedaccountRepository.Object);
            wssUnitOfWork.Verify(m => m.Save("0"), Times.Never, "Save was not called");

            var wssAccountMockData = new List<WssAccount>()
            {
                new WssAccount()
                {
                    WSSAccountId=1,
                    PrimaryEmailAddress="abc@gmial.com",
                    WssAccountStatusCode="ACT",
                    FailedResendActivationAttempts=0,
                    //LinkedUtilityAccounts=new List<LinkedUtilityAccount>
                    //{
                    //     new LinkedUtilityAccount()
                    //        {
                    //            LinkedUtilityAccountId=1,
                    //            NickName="Test Nick Name",
                    //            RelationshipCode="098gh89",
                    //            UtilityAccountId=1,
                    //            WssAccountId=1,
                    //            IsActive=true,

                    //            DefaultAccount=true
                    //        }
                    //}
                }
            }.AsQueryable();
            var mockWssAccountRepository = new Mock<IDataRepository<WssAccount>>();
            mockWssAccountRepository.Setup(m => m.FindAll()).Returns(() => wssAccountMockData);
            wssUnitOfWork.Setup(m => m.WssAccountRepository).Returns(mockWssAccountRepository.Object);

            var wssAccountStatusMockData = new List<WssAccountStatus>
            {
                new WssAccountStatus()
                {
                    WssAccountStatusId = 6,
                    WssAccountStatusCode = "REG",
                    WssAccountStatusDesc = "Registered"
                },
                 new WssAccountStatus()
                {
                    WssAccountStatusId = 7,
                    WssAccountStatusCode = "ACT",
                    WssAccountStatusDesc = "Activate"
                },
                  new WssAccountStatus()
                {
                    WssAccountStatusId = 8,
                    WssAccountStatusCode = "UNSUB",
                    WssAccountStatusDesc = "Unsubscribe"
                },
                   new WssAccountStatus()
                {
                    WssAccountStatusId = 9,
                    WssAccountStatusCode = "LCKD",
                    WssAccountStatusDesc = "Locked"
                },
            }.AsQueryable();

            var mockWssAccountStatusRepository = new Mock<IDataRepository<WssAccountStatus>>();
            mockWssAccountStatusRepository.Setup(m => m.FindAll()).Returns(() => wssAccountStatusMockData);
            wssUnitOfWork.Setup(m => m.WssAccountStatusRepository).Returns(mockWssAccountStatusRepository.Object);

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

            //var auditTransaction = new AuditTransaction.Implementation.AuditTransaction(wssUnitOfWork.Object);

            //var config = new MapperConfiguration(cfg => { cfg.AddProfile<AutomapperProfile>(); });
            //var mapper = config.CreateMapper();
            _searchController = new SearchController(wssUnitOfWork.Object, ubUnitOfWork.Object);
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();

            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            //session.Setup(m => m["UtilityAccountListRowsPerPage"]).Returns("");
            //session.Setup(m => m["UtilityAccountListSortDirection"]).Returns("");
            _searchController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _searchController);
        }

        [TestMethod]
        public void IndexhavingNullQuery()
        {
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();

            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            session.Setup(m => m["UtilityAccountListRowsPerPage"]).Returns("");
            session.Setup(m => m["UtilityAccountListSortDirection"]).Returns("");
            _searchController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _searchController);

            var result = _searchController.Index(null, null, null);
            var expected = (ViewResult)result;

            var recordcount = expected.ViewData.Count;
            Assert.AreEqual(recordcount, 0);
        }

        [TestMethod]
        public void IndexhavingAccountIdSearch()
        {
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();
            var server = new Mock<System.Web.HttpServerUtilityBase>();
            server.Setup(m => m.UrlDecode("0123456789")).Returns("0123456789");
            httpCnxt.Setup(m => m.Server).Returns(server.Object);

            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            session.Setup(m => m["UtilityAccountListRowsPerPage"]).Returns("");
            session.Setup(m => m["UtilityAccountListSortDirection"]).Returns("");
            _searchController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _searchController);

            var result = _searchController.Index("0123456789", 1, "1", "REG");
            var expected = (ViewResult)result;

            var recordcount = expected.ViewData.Count;
            Assert.AreNotEqual(recordcount, 0);
        }

        [TestMethod]
        public void IndexhavingCustomerNameSearch()
        {
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();
            var server = new Mock<System.Web.HttpServerUtilityBase>();
            server.Setup(m => m.UrlDecode("Test")).Returns("Test");
            httpCnxt.Setup(m => m.Server).Returns(server.Object);
            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            session.Setup(m => m["UtilityAccountListRowsPerPage"]).Returns("");
            session.Setup(m => m["UtilityAccountListSortDirection"]).Returns("");
            _searchController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _searchController);

            var result = _searchController.Index("Test", 1, "1");
            var expected = (ViewResult)result;

            var recordcount = expected.ViewData.Count;
            Assert.AreNotEqual(recordcount, 0);
        }

        [TestMethod]
        public void IndexhavingCustomerNameSearchSortedOnMainCostumerName()
        {
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();
            var server = new Mock<System.Web.HttpServerUtilityBase>();
            server.Setup(m => m.UrlDecode("Test")).Returns("Test");
            httpCnxt.Setup(m => m.Server).Returns(server.Object);
            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            session.Setup(m => m["UtilityAccountListRowsPerPage"]).Returns("");
            session.Setup(m => m["UtilityAccountListSortDirection"]).Returns("");
            _searchController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _searchController);

            var result = _searchController.Index("Test", 1, "1", "MainCustomerName", "Change");
            var expected = (ViewResult)result;

            var recordcount = expected.ViewData.Count;
            Assert.AreNotEqual(recordcount, 0);
        }

        [TestMethod]
        public void IndexhavingCustomerNameSearchSortedOnStatus()
        {
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();
            var server = new Mock<System.Web.HttpServerUtilityBase>();
            server.Setup(m => m.UrlDecode("Test")).Returns("Test");
            httpCnxt.Setup(m => m.Server).Returns(server.Object);
            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            session.Setup(m => m["UtilityAccountListRowsPerPage"]).Returns("");
            session.Setup(m => m["UtilityAccountListSortDirection"]).Returns("");
            _searchController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _searchController);

            var result = _searchController.Index("Test", 1, "1", "Status", "Change");
            var expected = (ViewResult)result;

            var recordcount = expected.ViewData.Count;
            Assert.AreNotEqual(recordcount, 0);
        }

        [TestMethod]
        public void IndexhavingCustomerNameSearchSortedOnPrimaryEmailAddress()
        {
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();
            var server = new Mock<System.Web.HttpServerUtilityBase>();
            server.Setup(m => m.UrlDecode("Test")).Returns("Test");
            httpCnxt.Setup(m => m.Server).Returns(server.Object);
            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            session.Setup(m => m["UtilityAccountListRowsPerPage"]).Returns("");
            session.Setup(m => m["UtilityAccountListSortDirection"]).Returns("");
            _searchController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _searchController);

            var result = _searchController.Index("Test", 1, "1", "PrimaryEmailAddress", "Change");
            var expected = (ViewResult)result;

            var recordcount = expected.ViewData.Count;
            Assert.AreNotEqual(recordcount, 0);
        }

        [TestMethod]
        public void IndexhavingCustomerNameSearchSortedOnAccountNumber()
        {
            var httpCnxt = new Mock<System.Web.HttpContextBase>();
            var session = new Mock<System.Web.HttpSessionStateBase>();
            var server = new Mock<System.Web.HttpServerUtilityBase>();
            server.Setup(m => m.UrlDecode("Test")).Returns("Test");
            httpCnxt.Setup(m => m.Server).Returns(server.Object);
            httpCnxt.Setup(m => m.Session).Returns(session.Object);
            session.Setup(m => m["UtilityAccountListRowsPerPage"]).Returns("");
            session.Setup(m => m["UtilityAccountListSortDirection"]).Returns("");
            _searchController.ControllerContext = new ControllerContext(new RequestContext(httpCnxt.Object, new RouteData()), _searchController);

            var result = _searchController.Index("Test", 1, "1", "AccountNumber", "Change");
            var expected = (ViewResult)result;

            var recordcount = expected.ViewData.Count;
            Assert.AreNotEqual(recordcount, 0);
        }

        [TestMethod]
        public void Edit()
        {
            var result = _searchController.Edit(1);
            var expected = (ViewResult)result;

            var recordcount = expected.ViewData.Count;
            Assert.AreEqual(recordcount, 0);
        }
    }
}