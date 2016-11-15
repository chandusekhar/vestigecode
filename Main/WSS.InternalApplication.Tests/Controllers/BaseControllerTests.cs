using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSS.InternalApplication.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSS.InternalApplication.Infrastructure;
using WSS.InternalApplication.Models;
using Moq;

namespace WSS.InternalApplication.Controllers.Tests
{
    [TestClass()]
    public class BaseControllerTests : ControllerTestBase
    {
        #region Test Setup

        private BaseController _controller;

        [TestInitialize]
        public void MyTestInitialize()
        {
            BaseTestInitialize();

            _controller = new BaseController();
        }


        [TestCleanup]
        public void MyTestCleanup()
        {
            _controller = null;
            BaseTestCleanUp(); // Base Cleanup
        }

        #endregion

        [TestMethod()]
        public void AddMessage_Test()
        {
            //Act
            _controller.AddMessage(UserMessageType.Success, "This is a success message", string.Empty);
            var notifications = _controller.TempData["Notifications"] as List<UserMessageModel>;

            //Assert
            Assert.IsNotNull(notifications);
            Assert.AreEqual(notifications.Count, 1);

            var messageText = notifications.Select(x => x.Message).FirstOrDefault();
            Assert.AreEqual(messageText, "This is a success message");

            var messageType = notifications.Select(x => x.Type).FirstOrDefault();
            Assert.AreEqual(messageType, UserMessageType.Success);

            _controller.ClearMessages();

            notifications = _controller.TempData["Notifications"] as List<UserMessageModel>;
            Assert.IsNotNull(notifications);
            Assert.AreEqual(notifications.Count, 0);
        }
    }
}