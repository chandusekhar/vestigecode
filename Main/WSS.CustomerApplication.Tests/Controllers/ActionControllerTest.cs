using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WSS.CustomerApplication.Controllers;
using WSS.Data;
using System.Web.Http.Results;
using System.Net.Http;

namespace WSS.CustomerApplication.Tests.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ActionControllerTest
    {
        ActionController _actionontroller;
        /// <summary>
        /// Constructors
        /// </summary>
        public ActionControllerTest()
        {
            var mockRequest = new Mock<HttpRequestMessage>();
            mockRequest.Object.RequestUri = new Uri("http://localhost?action=US&email=abc@gmail.com");
            var wssUnitOfWorkMock = new Mock<IUnitOfWork>();
            var actionDataMock = new List<WSS.Data.Action> {
                new Data.Action()
                {
                    ActionId=1, 
                    ActionName="",
                    ActionToken="0b58152e-2d12-4b47-95ae-61390e6a39c3",
                    ExpiryDateTime=DateTime.Now.AddDays(10),
                    WssAccountId=1
                },
                 new Data.Action()
                {
                    ActionId=2,
                    ActionName="",
                    ActionToken="25c7ef2d-c173-400c-8779-875c0bfa6063",
                    ExpiryDateTime=DateTime.Now.AddDays(10),
                    WssAccountId=1
                }
            }.AsQueryable();
            var mockActionRepository = new Mock<WSS.Data.IDataRepository<WSS.Data.Action>>();
            mockActionRepository.Setup(m => m.FindAll()).Returns(() => actionDataMock);

            var additionalDataMock = new List<AdditionalEmailAddress>
            {
                new AdditionalEmailAddress {
                    AdditionalEmailAddressId=1,
                    EmailAddress="abc@gmail.com",
                    WssAccountId=1
                },
                 new AdditionalEmailAddress {
                    AdditionalEmailAddressId=2,
                    EmailAddress="abc2@gmail.com",
                    WssAccountId=1
                }
            }.AsQueryable();

            var mockAdditionalEmailRepository = new Mock<WSS.Data.IDataRepository<AdditionalEmailAddress>>();
            mockAdditionalEmailRepository.Setup(m => m.FindAll()).Returns(() => additionalDataMock);

            wssUnitOfWorkMock.Setup(x => x.ActionDataRepository).Returns(mockActionRepository.Object);
            wssUnitOfWorkMock.Setup(x => x.AdditionalEmailAddressRepository).Returns(mockAdditionalEmailRepository.Object);
            _actionontroller = new ActionController(wssUnitOfWorkMock.Object);
            _actionontroller.Request = mockRequest.Object;
        }

        [TestMethod]
        public void GetActionTokenEmpty()
        {
            var actionToken = string.Empty;
            var result = _actionontroller.Get(actionToken);
            var expected = ((BadRequestErrorMessageResult)result).Message;
            Assert.AreEqual("Invalid Action Token", expected);
        }

        [TestMethod]
        public void GetActionTokenInvalid()
        {
            var actionToken = "test";
            var result = _actionontroller.Get(actionToken);
            var expected = ((BadRequestErrorMessageResult)result).Message;
            Assert.AreEqual("Invalid Action Token", expected);
        }

        [TestMethod]
        public void GetActionTokenValidUnsubscribeSecondaryEmail()
        {
            var actionToken = "0b58152e-2d12-4b47-95ae-61390e6a39c3";
            var result = _actionontroller.Get(actionToken);
            var expected = ((RedirectResult)result).Location.OriginalString;
            Assert.AreEqual("/UnsubscribeSecondary/UnsubscribeSecondaryEmail?wssAdditionalEmailId=1&secondaryEmail=abc@gmail.com", expected);
        }

        [TestMethod]
        public void GetActionTokenValidUnsubscribeSecondaryEmaiActionTokenNotExists()
        {
            var actionToken = "87b4d02c-f89a-4a8d-95c9-87b089281015";
            var result = _actionontroller.Get(actionToken);
            var expected = ((RedirectResult)result).Location.OriginalString;
            Assert.AreEqual("", expected);
        }

        [TestMethod]
        public void GetActionTokenValidUnsubscribeSecondaryEmaiNotExists()
        {
            var actionToken = "0b58152e-2d12-4b47-95ae-61390e6a39c3";
            _actionontroller.Request.RequestUri = new Uri("http://localhost?action=US&email=abc78@gmail.com");
            var result = _actionontroller.Get(actionToken);
            var expected = ((RedirectResult)result).Location.OriginalString;
            Assert.AreEqual("/Login/Index", expected);
        }


    }
}
