using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WSS.Common.Utilities.ActionLink;
using WSS.Common.Utilities.Tests.Base;
// ReSharper disable RedundantNameQualifier
// Leaving qualifiers in for readability -- helps differentiate between WSS.Data and UtilityBilling.Data

namespace WSS.Common.Utilities.Tests.ActionLink
{
    [TestClass]
    public class ActionLinkManagerTest : BaseTest
    {
        [TestMethod]
        public void TestGenerateResetPasswordLink_Success()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateResetPasswordLink(1);

            // Assert
            Assert.IsNotNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Once);
        }

        [TestMethod]
        public void TestGenerateResetPasswordLink_FailNotFound()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateResetPasswordLink(-1);

            // Assert
            Assert.IsNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Never);
        }

        [TestMethod]
        public void TestGenerateActivateAccountLink_Success()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateActivateAccountLink(1);

            // Assert
            Assert.IsNotNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Once);
        }

        [TestMethod]
        public void TestGenerateActivateAccountLink_FailNotFound()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateActivateAccountLink(-1);

            // Assert
            Assert.IsNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Never);
        }

        [TestMethod]
        public void TestGenerateUnsubscribeSecondaryLink_Success()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateUnsubscribeSecondaryLink(1, "bob2@server.com");

            // Assert
            Assert.IsNotNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Never);
        }

        [TestMethod]
        public void TestGenerateUnsubscribeSecondaryLink_FailAccountNotFound()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateUnsubscribeSecondaryLink(-1, "bob2@server.com");

            // Assert
            Assert.IsNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Never);
        }

        [TestMethod]
        public void TestGenerateUnsubscribeSecondaryLink_FailAddressNotFound()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateUnsubscribeSecondaryLink(1, "idontexist@nowhere.com");

            // Assert
            Assert.IsNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Never);
        }

        [TestMethod]
        public void TestGenerateUnsubscribeSecondaryLink_FailAddressNull()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateUnsubscribeSecondaryLink(1, null);

            // Assert
            Assert.IsNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Never);
        }

        [TestMethod]
        public void TestGenerateUnsubscribePrimaryLink_Success()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateUnsubscribePrimaryLink(1);

            // Assert
            Assert.IsNotNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Never);
        }

        [TestMethod]
        public void TestGenerateUnsubscribePrimaryLink_FailNotFound()
        {
            // Arrange
            var actionLinkManager = new ActionLinkManager(MockWssUnitOfWork.Object, "http://localhost/");

            // Act
            var url = actionLinkManager.GenerateUnsubscribePrimaryLink(-1);

            // Assert
            Assert.IsNull(url);
            MockActionRepository.Verify(m => m.Insert(It.IsAny<WSS.Data.Action>()), Times.Never);
        }
    }
}