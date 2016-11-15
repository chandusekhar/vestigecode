using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using UtilityBilling.Data;
using WSS.Common.Utilities.Tests.Base;
using WSS.Common.Utilities.Transactions;
using WSS.Data;

namespace WSS.Common.Utilities.Tests.Transactions
{
    [TestClass]
    public class RegistrationTransactionTest : BaseTest
    {
        #region ExistingAccountTests

        [TestMethod]
        public void TestRegisterExistingUtilityAccount_Success()
        {
            // ----------
            // Arrange
            // ----------

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterExistingUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.Success, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Never);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Once);
        }

        [TestMethod]
        public void TestRegisterExistingAccount_FailureWssAccount()
        {
            // ----------
            // Arrange
            // ----------
            MockWssAccountRepository.Setup(x => x.Insert(It.IsAny<WssAccount>())).Throws<DbEntityValidationException>();

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterExistingUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.FailureWssAccount, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Never);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Never);
        }

        [TestMethod]
        public void TestRegisterExistingAccount_FailureFindExistingUtilityAccount_Exception()
        {
            // ----------
            // Arrange
            // ----------
            MockUtilityAccountRepository.Setup(x => x.FindAll()).Throws<DbEntityValidationException>();

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterExistingUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.FailureFindExistingUtilityAccount, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockWssAccountRepository.Verify(m => m.Delete(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.FindAll(), Times.Once());
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Never);
            MockUtilityAccountRepository.Verify(m => m.Delete(It.IsAny<UtilityAccount>()), Times.Never);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Never);
        }

        [TestMethod]
        public void TestRegisterExistingAccount_FailureFindExistingUtilityAccount_NotFound()
        {
            // ----------
            // Arrange
            // ----------
            MockUtilityAccountRepository.Setup(x => x.FindAll()).Returns(new List<UtilityAccount>().AsQueryable());

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterExistingUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.FailureFindExistingUtilityAccount, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockWssAccountRepository.Verify(m => m.Delete(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.FindAll(), Times.Once());
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Never);
            MockUtilityAccountRepository.Verify(m => m.Delete(It.IsAny<UtilityAccount>()), Times.Never);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Never);
        }

        [TestMethod]
        public void TestRegisterExistingAccount_FailureLinkingAccounts()
        {
            // ----------
            // Arrange
            // ----------
            MockWssLinkedUtilityAccountRepository.Setup(x => x.Insert(It.IsAny<LinkedUtilityAccount>())).Throws<DbEntityValidationException>();

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterExistingUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.FailureLinkingAccounts, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockWssAccountRepository.Verify(m => m.Delete(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.FindAll(), Times.Once());
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Never);
            MockUtilityAccountRepository.Verify(m => m.Delete(It.IsAny<UtilityAccount>()), Times.Never);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Once);
        }

        #endregion ExistingAccountTests

        #region NewAccountTests

        [TestMethod]
        public void TestRegisterNewUtilityAccount_Success()
        {
            // ----------
            // Arrange
            // ----------

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterNewUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.Success, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Once);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Once);
        }

        [TestMethod]
        public void TestRegisterNewAccount_FailureWssAccount()
        {
            // ----------
            // Arrange
            // ----------
            MockWssAccountRepository.Setup(x => x.Insert(It.IsAny<WssAccount>())).Throws<DbEntityValidationException>();

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterNewUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.FailureWssAccount, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Never);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Never);
        }

        [TestMethod]
        public void TestRegisterNewAccount_FailureCreatePlaceholderUtilityAccount()
        {
            // ----------
            // Arrange
            // ----------
            MockUtilityAccountRepository.Setup(x => x.Insert(It.IsAny<UtilityAccount>())).Throws<DbEntityValidationException>();

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterNewUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.FailureCreatePlaceholderUtilityAccount, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockWssAccountRepository.Verify(m => m.Delete(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Once);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Never);
        }

        [TestMethod]
        public void TestRegisterNewAccount_FailureLinkingAccounts()
        {
            // ----------
            // Arrange
            // ----------
            MockWssLinkedUtilityAccountRepository.Setup(x => x.Insert(It.IsAny<LinkedUtilityAccount>())).Throws<DbEntityValidationException>();

            // Class under test
            var registrationTransaction = new RegistrationTransaction(MockWssUnitOfWork.Object,
                MockUtilityUnitOfWork.Object)
            {
                UtilityBillingAccountNumber = TestUtilityAccountNumber,
                EmailAddress = TestEmailAddress
            };

            // ----------
            // Act
            // ----------
            var result = registrationTransaction.RegisterNewUtilityAccount();

            // ----------
            // Assert
            // ----------
            Assert.AreEqual(RegistrationTransaction.RegistrationResult.FailureLinkingAccounts, result);
            MockWssAccountRepository.Verify(m => m.Insert(It.IsAny<WssAccount>()), Times.Once);
            MockWssAccountRepository.Verify(m => m.Delete(It.IsAny<WssAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<UtilityAccount>()), Times.Once);
            MockUtilityAccountRepository.Verify(m => m.Delete(It.IsAny<UtilityAccount>()), Times.Once);
            MockWssLinkedUtilityAccountRepository.Verify(m => m.Insert(It.IsAny<LinkedUtilityAccount>()), Times.Once);
        }

        #endregion NewAccountTests
    }
}