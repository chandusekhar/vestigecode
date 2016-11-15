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
    public class InterceptControllerTests : ControllerTestBase
    {
        #region Test Setup

        private InterceptController _controller;

        [TestInitialize]
        public void MyTestInitialize()
        {
            BaseTestInitialize();

            _controller = new InterceptController();
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