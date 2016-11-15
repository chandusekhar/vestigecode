using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSS.InternalApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace WSS.InternalApplication.Controllers.Tests
{
    [TestClass()]
    public class RegisterControllerTests : ControllerTestBase
    {
        #region Test Setup

        private RegisterController _controller;

        [TestInitialize]
        public void MyTestInitialize()
        {
            BaseTestInitialize();

            _controller = new RegisterController();
        }


        [TestCleanup]
        public void MyTestCleanup()
        {
            _controller = null;
            BaseTestCleanUp(); // Base Cleanup
        }

        #endregion

        [TestMethod()]
        public void IndexTest()
        {

        }
    }
}