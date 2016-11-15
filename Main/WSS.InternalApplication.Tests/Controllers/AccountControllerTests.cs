using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSS.InternalApplication.Controllers;
using UtilityBilling.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSS.Data;
using AutoMapper;
using System.Web.Mvc;
using WSS.InternalApplication.Infrastructure;
using WSS.InternalApplication.Models;
using IUnitOfWork = WSS.Data.IUnitOfWork;
using Moq;

namespace WSS.InternalApplication.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests : ControllerTestBase
    {
        #region Test Setup

        private AccountController _controller;
        private IUnitOfWork _wssUnitOfWork;
        private UtilityBilling.Data.IUnitOfWork _utilityUnitOfWork;
        private IMapper MockMapper;
        private Mock<IMapper> _mockMapper;


        [TestInitialize]
        public void MyTestInitialize()
        {
            BaseTestInitialize();

            _mockMapper = new Mock<IMapper>();

            _controller = new AccountController(MockUnitOfWork, MockUtilityUnitOfWork, MockMapper);
        }


        [TestCleanup]
        public void MyTestCleanup()
        {
            _controller = null;
            BaseTestCleanUp(); // Base Cleanup
        }

        #endregion

        [TestMethod()]
        public void Index()
        {

        }

        [TestMethod()]
        public void ViewAccount()
        {
        }

        [TestMethod()]
        public void _bills()
        {
        }

        [TestMethod()]
        public void GetDocumentDetailFile()
        {
        }

        [TestMethod()]
        public void GetAuditPartialView()
        {
        }

        [TestMethod()]
        public void IndexTest()
        {

        }
    }
}