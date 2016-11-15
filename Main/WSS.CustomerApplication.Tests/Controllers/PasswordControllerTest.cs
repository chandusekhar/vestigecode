using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WSS.AuditTransaction.Interfaces;
using WSS.CustomerApplication.Controllers;
using WSS.CustomerApplication.Infrastructure;
using WSS.CustomerApplication.Models;
using WSS.CustomerApplication.Tests.Base;
using WSS.Data;
using WSS.Email.Service;
using WSS.Identity;
using WSS.Logging.Service;

namespace WSS.CustomerApplication.Tests.Controllers
{
    [TestClass]
    public class PasswordControllerTest : BaseTest
    {
        // Basic Test Data
        private const string TestOldEmailAddress = "bob@server.com";

        private readonly Mock<UserStore<WssIdentityUser>> _mockIdnetityUser;

        private readonly PasswordController _passwordController;
        private readonly Mock<IAuditTransaction> _mockAuditTransaction;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IDataRepository<WssAccount>> _mockWssAccountRepository;
        private readonly Mock<IDataRepository<Data.Action>> _mockActionRepository;
        private readonly Mock<ISendEmail> _mockEmailService;
        private readonly Mock<IWssIdentityUserManager> _mockIdnetityuserManager;
        private readonly Mock<ILogger> _mockILogger;


        public PasswordControllerTest()
        {
            _mockILogger = new Mock<ILogger>();
            _mockAuditTransaction = new Mock<IAuditTransaction>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockIdnetityUser = new Mock<UserStore<WssIdentityUser>>();
            _mockWssAccountRepository = new Mock<IDataRepository<WssAccount>>();
            _mockEmailService = new Mock<ISendEmail>();
            _mockActionRepository = new Mock<IDataRepository<Data.Action>>();
            _mockIdnetityuserManager = new Mock<IWssIdentityUserManager>();
            _passwordController = new PasswordController(_mockUnitOfWork.Object, _mockAuditTransaction.Object,
                _mockEmailService.Object);

        }

        [TestInitialize]
        public void Initilize()
        {

            var mockWssAccountData = new List<WssAccount>()
            {
                new WssAccount() {WSSAccountId = 1,PrimaryEmailAddress = TestOldEmailAddress,SecurityQuestionAnswer = "Tes",SecurityQuestion = "Test"},
            }.AsQueryable();

            var mockWssAuditData = new List<AuditRecord>()
            {
                new AuditRecord() {AuditRecordId = 1}
            }.AsQueryable();

            var mockActionData = new List<Data.Action>()
            {
               new Data.Action()
               {
                   WssAccountId = 1,
                   ActionId = 1,
                   ActionToken = "7226679b-2d05-41a6-bbea-b45f7773f300",
                   ActionName = "Test",
                   ExpiryDateTime = DateTime.Now.AddDays(1)
               }
            }.AsQueryable();

            _mockIdnetityUser.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(new WssIdentityUser() { Email = "test@test.com", Id = "1212121212" }));

            _mockActionRepository.Setup(x => x.FindAll()).Returns(mockActionData);

            _mockWssAccountRepository.Setup(x => x.FindAll()).Returns(mockWssAccountData);
            _mockAuditTransaction.Setup(x => x.AddAuditRecord(It.IsAny<AuditRecord>()));
            _mockUnitOfWork.Setup(x => x.WssAccountRepository).Returns(_mockWssAccountRepository.Object);
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.SetupGet(s => s["CurrentAttempt"]).Returns("0");
            mockSession.SetupGet(s => s["CurrentAttempt_SecurityQuestion"]).Returns("0");
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            _passwordController.ControllerContext = mockControllerContext.Object;
            _mockUnitOfWork.Setup(x => x.ActionDataRepository).Returns(_mockActionRepository.Object);
        }

        [TestMethod]
        public void ForgotPasswordTest()
        {
            var model = new ForgotPassowrdViewModel
            {
                AttemptsLeft = 3,
                EmailAddress = "test@test.com"
            };
            _passwordController.ForgotPassword(model);
        }

        [TestMethod]
        public void ForgotPasswordWithExistingEmail()
        {
            var model = new ForgotPassowrdViewModel
            {
                AttemptsLeft = 3,
                EmailAddress = "bob@server.com"
            };
            _passwordController.ForgotPassword(model);
        }

        [TestMethod]
        public void ResetPasswordTest()
        {
            _passwordController.ResetPassword(1, "7226679b-2d05-41a6-bbea-b45f7773f300");
        }

        [TestMethod]
        public void ResetPasswordNextTest()
        {
            var model = new ForgotPassowrdViewModel
            {
                AttemptsLeft = 3,
                EmailAddress = "bob@server.com",
                WssAccountId = 1,
                Actiontoken = "7226679b-2d05-41a6-bbea-b45f7773f300",
                SecurityAnswer = "Test"
            };

            _passwordController.ResetPasswordNext(model);
        }

        [TestMethod]
        public void Confirm()
        {

            // ----------
            // Arrange
            // ----------
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperProfile>();
            });
            var mapper = config.CreateMapper();

            // Class under test
            var passwordController = new PasswordController(MockWssUnitOfWork.Object, MockAuditTransaction.Object,
                MockSendMail.Object);
            {
            };

            // ----------
            // Act
            // ----------
            var model = new ForgotPassowrdViewModel
            {
                AttemptsLeft = 3,
                EmailAddress = "bob@server.com",
                WssAccountId = 1,
                Actiontoken = "7226679b-2d05-41a6-bbea-b45f7773f300",
                SecurityAnswer = "Test"
            };

            object[] args = new object[1] { model };        

            _mockIdnetityuserManager.Setup(x => x.FindByEmail(It.IsAny<string>()))
                .Returns(new WssIdentityUser() { Email = "test@test.com" });
            _mockIdnetityuserManager.Setup(
                x => x.ResetPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new IdentityResult() { });

            PrivateObject objAccountController = new PrivateObject(_passwordController);
            objAccountController.SetFieldOrProperty("_identityUserManager", _mockIdnetityuserManager.Object);
            objAccountController.SetFieldOrProperty("_unitOfWork", _mockUnitOfWork.Object);
            objAccountController.SetFieldOrProperty("_auditTransaction", _mockAuditTransaction.Object);
            objAccountController.Invoke("Confirm", args);

            // ----------
            // Assert
            // ----------
            Assert.IsNotNull(objAccountController);
        }
    }
}
