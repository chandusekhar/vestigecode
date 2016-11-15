using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WSS.CustomerApplication.Controllers;
using WSS.CustomerApplication.Tests.Base;

namespace WSS.CustomerApplication.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest : BaseTest
    {
        [TestMethod]
        public void CustomerChangePassword_Success()
        {
            // Arrange

            // Class under test
            var accountController = new AccountController(MockWssUnitOfWork.Object, MockUtilityUnitOfWork.Object, null, MockAuditTransaction.Object, MockSendMail.Object);
            accountController.IdentityUserManager = MockUserManager.Object;
           // accountController.User = Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity);


            accountController.User.Identity.GetUserId(); //returns "IdOfYourChoosing"
            // Act
            accountController.ChangePassword(TestWssAccountId, TestPassword1, TestPassword2, TestPassword2);

            // Assert
            MockUserManager.Verify(m => m.FindByEmail(It.IsAny<string>()), Times.Once);
            MockUserManager.Verify(m => m.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
