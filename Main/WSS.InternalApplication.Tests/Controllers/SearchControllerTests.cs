using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSS.InternalApplication.Controllers;
using WSS.InternalApplication.Infrastructure;
using WSS.InternalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using AutoMapper;
using WSS.Logging;

namespace WSS.InternalApplication.Controllers.Tests
{
    [TestClass()]
    public class SearchControllerTests : ControllerTestBase
    {
        #region Test Setup

        private SearchController _controller;
        private UtilityBilling.Data.IUnitOfWork _ubUnitofWork;
        private ILogger _logger = new Logger(typeof(SearchController));
        private UtilityBilling.Data.IUnitOfWork _utilityUnitOfWork;
        private IMapper MockMapper;
        private Mock<IMapper> _mockMapper;


        [TestInitialize]
        public void MyTestInitialize()
        {
            BaseTestInitialize();

            _mockMapper = new Mock<IMapper>();

            _controller = new SearchController(MockUnitOfWork, MockUtilityUnitOfWork);
        }


        [TestCleanup]
        public void MyTestCleanup()
        {
            _controller = null;
            BaseTestCleanUp(); // Base Cleanup
        }

        #endregion

        [TestMethod()]
        public void Index_SearchController()
        {

        }

        [TestMethod()]
        public void Edit()
        {

        }
    }
}