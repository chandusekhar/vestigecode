using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UtilityBilling.Data;
using WSS.CustomerApplication.Infrastructure;
using WSS.CustomerApplication.Tests.Base;

namespace WSS.CustomerApplication.Tests.Infrastructure
{
    [TestClass]
    public class CommonMethodsTests : BaseTest
    {
        [TestMethod]
        public void TestValidateUtilityAccountNumberAndMeterNumber_Success_MeteredAccount()
        {
            // Arrange
            const string accountNumber = "1234567890";
            const string meterNumber = "87654321";

            var fakeAccountMeterLookup = new AccountMeterLookup()
            {
                CcbAcctId = accountNumber,
                CcbBadgeNbr = meterNumber
            };

            var mockAccountMeterLookupRepository = new Mock<IDataRepository<AccountMeterLookup>>();
            mockAccountMeterLookupRepository.Setup(m => m.FindAll()).Returns(new List<AccountMeterLookup> {fakeAccountMeterLookup}.AsQueryable()).Verifiable();

            MockUtilityUnitOfWork.Setup(m => m.AccountMeterLookupRepository).Returns(mockAccountMeterLookupRepository.Object).Verifiable();


            // Act
            var result = CommonMethods.ValidateUtilityAccountNumberAndMeterNumber(MockUtilityUnitOfWork.Object, accountNumber,
                meterNumber, false);

            // Assert
            Assert.IsTrue(result);
            MockUtilityUnitOfWork.Verify(m => m.AccountMeterLookupRepository, Times.Once);
            mockAccountMeterLookupRepository.Verify(m => m.FindAll(), Times.Once);
        }

        [TestMethod]
        public void TestValidateUtilityAccountNumberAndMeterNumber_Failure_MeterNumberNotFound()
        {
            // Arrange
            const string accountNumber = "1234567890";
            const string meterNumber = "87654321";
            const string wrongMeterNumber = "987654322";

            var fakeAccountMeterLookup = new AccountMeterLookup()
            {
                CcbAcctId = accountNumber,
                CcbBadgeNbr = meterNumber
            };

            var mockAccountMeterLookupRepository = new Mock<IDataRepository<AccountMeterLookup>>();
            mockAccountMeterLookupRepository.Setup(m => m.FindAll()).Returns(new List<AccountMeterLookup> { fakeAccountMeterLookup }.AsQueryable()).Verifiable();

            MockUtilityUnitOfWork.Setup(m => m.AccountMeterLookupRepository).Returns(mockAccountMeterLookupRepository.Object).Verifiable();


            // Act
            var result = CommonMethods.ValidateUtilityAccountNumberAndMeterNumber(MockUtilityUnitOfWork.Object, accountNumber,
                wrongMeterNumber, false);

            // Assert
            Assert.IsFalse(result);
            MockUtilityUnitOfWork.Verify(m => m.AccountMeterLookupRepository, Times.Once);
            mockAccountMeterLookupRepository.Verify(m => m.FindAll(), Times.Once);
        }

        [TestMethod]
        public void TestValidateUtilityAccountNumberAndMeterNumber_Success_UnmeteredAccount()
        {
            // Arrange
            const string accountNumber = "1234567890";
            const string meterNumber = "UNMETERED";

            var fakeAccountMeterLookup = new AccountMeterLookup()
            {
                CcbAcctId = accountNumber,
                CcbBadgeNbr = meterNumber
            };

            var mockAccountMeterLookupRepository = new Mock<IDataRepository<AccountMeterLookup>>();
            mockAccountMeterLookupRepository.Setup(m => m.FindAll()).Returns(new List<AccountMeterLookup> { fakeAccountMeterLookup }.AsQueryable()).Verifiable();

            MockUtilityUnitOfWork.Setup(m => m.AccountMeterLookupRepository).Returns(mockAccountMeterLookupRepository.Object).Verifiable();


            // Act
            var result = CommonMethods.ValidateUtilityAccountNumberAndMeterNumber(MockUtilityUnitOfWork.Object, accountNumber,
                "", true);

            // Assert
            Assert.IsTrue(result);
            MockUtilityUnitOfWork.Verify(m => m.AccountMeterLookupRepository, Times.Once);
            mockAccountMeterLookupRepository.Verify(m => m.FindAll(), Times.Once);
        }

        [TestMethod]
        public void TestValidateUtilityAccountNumberAndMeterNumber_Failure_UnmeteredCheckedAndMeterNumberEntered()
        {
            // Arrange
            const string accountNumber = "1234567890";
            const string meterNumber = "87654321";

            var fakeAccountMeterLookup = new AccountMeterLookup()
            {
                CcbAcctId = accountNumber,
                CcbBadgeNbr = meterNumber
            };

            var mockAccountMeterLookupRepository = new Mock<IDataRepository<AccountMeterLookup>>();
            mockAccountMeterLookupRepository.Setup(m => m.FindAll()).Returns(new List<AccountMeterLookup> { fakeAccountMeterLookup }.AsQueryable()).Verifiable();

            MockUtilityUnitOfWork.Setup(m => m.AccountMeterLookupRepository).Returns(mockAccountMeterLookupRepository.Object).Verifiable();

            // Act
            var result = CommonMethods.ValidateUtilityAccountNumberAndMeterNumber(MockUtilityUnitOfWork.Object, accountNumber,
                meterNumber, true);

            // Assert
            Assert.IsFalse(result);
            MockUtilityUnitOfWork.Verify(m => m.AccountMeterLookupRepository, Times.Never);
            mockAccountMeterLookupRepository.Verify(m => m.FindAll(), Times.Never);
        }

        [TestMethod]
        public void TestValidateUtilityAccountNumberAndMeterNumber_Failure_UnmeteredNotCheckedAndNoMeterNumberEntered()
        {
            // Arrange
            const string accountNumber = "1234567890";
            const string meterNumber = "87654321";

            var fakeAccountMeterLookup = new AccountMeterLookup()
            {
                CcbAcctId = accountNumber,
                CcbBadgeNbr = meterNumber
            };

            var mockAccountMeterLookupRepository = new Mock<IDataRepository<AccountMeterLookup>>();
            mockAccountMeterLookupRepository.Setup(m => m.FindAll()).Returns(new List<AccountMeterLookup> { fakeAccountMeterLookup }.AsQueryable()).Verifiable();

            MockUtilityUnitOfWork.Setup(m => m.AccountMeterLookupRepository).Returns(mockAccountMeterLookupRepository.Object).Verifiable();

            // Act
            var result = CommonMethods.ValidateUtilityAccountNumberAndMeterNumber(MockUtilityUnitOfWork.Object, null,
                null, true);

            // Assert
            Assert.IsFalse(result);
            MockUtilityUnitOfWork.Verify(m => m.AccountMeterLookupRepository, Times.Never);
            mockAccountMeterLookupRepository.Verify(m => m.FindAll(), Times.Never);
        }

        [TestMethod]
        public void TestValidateUtilityAccountNumberAndMeterNumber_Failure_UnmeteredCheckedAndMeterNumberExists()
        {
            // Arrange
            const string accountNumber = "1234567890";
            const string meterNumber1 = "87654321";
            const string meterNumber2 = "UNMETERED";

            var fakeAccountMeterLookup1 = new AccountMeterLookup()
            {
                CcbAcctId = accountNumber,
                CcbBadgeNbr = meterNumber1
            };

            var fakeAccountMeterLookup2 = new AccountMeterLookup()
            {
                CcbAcctId = accountNumber,
                CcbBadgeNbr = meterNumber2
            };

            var mockAccountMeterLookupRepository = new Mock<IDataRepository<AccountMeterLookup>>();
            mockAccountMeterLookupRepository.Setup(m => m.FindAll()).Returns(new List<AccountMeterLookup> { fakeAccountMeterLookup1, fakeAccountMeterLookup2 }.AsQueryable()).Verifiable();

            MockUtilityUnitOfWork.Setup(m => m.AccountMeterLookupRepository).Returns(mockAccountMeterLookupRepository.Object).Verifiable();

            // Act
            var result = CommonMethods.ValidateUtilityAccountNumberAndMeterNumber(MockUtilityUnitOfWork.Object, null,
                null, true);

            // Assert
            Assert.IsFalse(result);
            MockUtilityUnitOfWork.Verify(m => m.AccountMeterLookupRepository, Times.Never);
            mockAccountMeterLookupRepository.Verify(m => m.FindAll(), Times.Never);
        }
    }
}
