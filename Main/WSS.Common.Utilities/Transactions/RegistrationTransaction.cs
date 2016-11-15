using System;
using System.Linq;
using UtilityBilling.Data;
using WSS.Data;
using WSS.Data.Enum;
using WSS.Logging.Service;
using IUnitOfWork = WSS.Data.IUnitOfWork;
using UtilityAccountSource = UtilityBilling.Data.Enum.UtilityAccountSource;

namespace WSS.Common.Utilities.Transactions
{
    public class RegistrationTransaction
    {
        public enum RegistrationResult
        {
            Success,
            FailureWssAccount,
            FailureCreatePlaceholderUtilityAccount,
            FailureFindExistingUtilityAccount,
            FailureLinkingAccounts
        }

        private static readonly ILogger Logger = new Logger(typeof(RegistrationTransaction));

        private readonly IUnitOfWork _wssUnitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _utilityUnitOfWork;

        private readonly WssAccount _newWssAccount = new WssAccount();
        private readonly UtilityAccount _newUtilityAccount = new UtilityAccount();
        private readonly LinkedUtilityAccount _newLinkedUtilityAccount = new LinkedUtilityAccount();

        private int _utilityAccountId;

        public string UtilityBillingAccountNumber { get; set; }
        public string EmailAddress { get; set; }
        public string UtilityAccountHolderName { get; set; }

        public RegistrationTransaction(IUnitOfWork wssUnitOfWork, UtilityBilling.Data.IUnitOfWork utilityUnitOfWork)
        {
            _wssUnitOfWork = wssUnitOfWork;
            _utilityUnitOfWork = utilityUnitOfWork;
        }

        /// <summary>
        /// Encapsulates a registration transaction and manages committing and rolling back changes to the database during registration of an account that already exists in the UtilityBilling database.
        ///
        /// Any required checks for whether or not an account already exists should be performed by the caller, and then the appropriate method in th is class should be called.
        /// </summary>
        /// <returns>Returns TRUE if the operation was successful.  Returns FALSE if the operation failed and needed to be rolled back.</returns>
        public RegistrationResult RegisterExistingUtilityAccount()
        {
            var success = InsertWssAccount();
            if (!success)
            {
                return RegistrationResult.FailureWssAccount;
            }

            try
            {
                var existingUtilityAccount = _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                    .FirstOrDefault(x => x.ccb_acct_id.Equals(UtilityBillingAccountNumber));

                if (existingUtilityAccount != null)
                {
                    _utilityAccountId =
                        existingUtilityAccount.UtilityAccountId;
                }
                else
                {
                    RevertWssAccount();
                    return RegistrationResult.FailureFindExistingUtilityAccount;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to find existing Utility Billing account for Utility Billing Account Number: {UtilityBillingAccountNumber}.  An exception occurred.", ex);
                RevertWssAccount();
                return RegistrationResult.FailureFindExistingUtilityAccount;
            }

            success = InsertLinkedUtilityAccount();
            if (!success)
            {
                RevertWssAccount();
                return RegistrationResult.FailureLinkingAccounts;
            }
            return RegistrationResult.Success;
        }

        /// <summary>
        /// Encapsulates a registration transaction and manages committing and rolling back changes to the database during registration of an account that does not exist in the UtilityBilling database.
        ///
        /// This method will take the additional step of creating a placeholder entry for the users Utility Billing account until it is updated from the business system.
        ///
        /// Any required checks for whether or not an account already exists should be performed by the caller, and then the appropriate method in this class should be called.
        /// </summary>
        /// <returns>Returns TRUE if the operation was successful.  Returns FALSE if the operation failed and needed to be rolled back.</returns>
        public RegistrationResult RegisterNewUtilityAccount()
        {
            var success = InsertWssAccount();
            if (!success)
            {
                return RegistrationResult.FailureWssAccount;
            }

            success = InsertUtilityAccount();
            if (!success)
            {
                RevertWssAccount();
                return RegistrationResult.FailureCreatePlaceholderUtilityAccount;
            }

            _utilityAccountId = _newUtilityAccount.UtilityAccountId;

            success = InsertLinkedUtilityAccount();
            if (!success)
            {
                RevertUtilityAccount();
                RevertWssAccount();
                return RegistrationResult.FailureLinkingAccounts;
            }
            return RegistrationResult.Success;
        }

        /// <summary>
        /// Creates an entry in the WSSApplication database based on the UtilityBillingAccountNumber and EmailAddress this instance has assigned.
        /// </summary>
        /// <returns>Returns TRUE if the operation was successful.  Returns FALSE if the operation failed and needed to be rolled back.</returns>
        private bool InsertWssAccount()
        {
            try
            {
                _newWssAccount.PrimaryEmailAddress = EmailAddress;
                _newWssAccount.WssAccountStatusCode = AccountStatus.REG.ToString();
                _wssUnitOfWork.WssAccountRepository.Insert(_newWssAccount);
                //TODO Call this with the actual login of the logged in user
                _wssUnitOfWork.Save("0");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurred when adding WSS Account entry for Account Number: {UtilityBillingAccountNumber}, e-mail address: {EmailAddress}", ex);
                return false;
            }
        }

        /// <summary>
        /// Removes the entry in the WSSApplication database based on the UtilityBillingAccountNumber and EmailAddress this instance has assigned.  Should be used to rollback a previous insertion made by this instance.
        /// </summary>
        private void RevertWssAccount()
        {
            _wssUnitOfWork.WssAccountRepository.Delete(_newWssAccount);
        }

        // DF - Leaving this here just in case, but this logic will likely be handled in the caller (controller)
        //public bool IsUtilityAccountAlreadyLoaded()
        //{
        //        var existingUtilityAccount = _utilityUnitOfWork.UtilityAccountRepository.FindAll()
        //            .FirstOrDefault(x => x.ccb_acct_id.Equals(UtilityBillingAccountNumber));

        //        return existingUtilityAccount != null;
        //}

        /// <summary>
        /// Creates a placeholder entry in the Utility Billing database based on the UtilityBillingAccountNumber and EmailAddress this instance has assigned.
        /// </summary>
        /// <returns>Returns TRUE if the operation was successful.  Returns FALSE if the operation failed and needed to be rolled back.</returns>
        private bool InsertUtilityAccount()
        {
            try
            {
                _newUtilityAccount.PrimaryAccountHolderName = UtilityAccountHolderName; //"NEW WSS CUSTOMER";
                _newUtilityAccount.ccb_acct_id = UtilityBillingAccountNumber;
                _newUtilityAccount.UtilityAccountSourceCode = UtilityAccountSource.WSS.ToString();
                _utilityUnitOfWork.UtilityAccountRepository.Insert(_newUtilityAccount);
                //TODO Call this with the actual login of the logged in user
                _utilityUnitOfWork.Save("0");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurred when adding the placeholder Utility Account entry for Account Number: {UtilityBillingAccountNumber}, e-mail address: {EmailAddress}", ex);
                return false;
            }
        }

        /// <summary>
        /// Removes the entry in the UtilityBilling database based on the UtilityBillingAccountNumber and EmailAddress this instance has assigned.  Should be used to rollback a previous insertion made by this instance.
        /// </summary>
        private void RevertUtilityAccount()
        {
            _utilityUnitOfWork.UtilityAccountRepository.Delete(_newUtilityAccount);
        }

        /// <summary>
        /// Creates an entry in the WSS Database to link the WSS Profile with the Utility Account based on the UtilityBillingAccountNumber and EmailAddress this instance has assigned.
        /// </summary>
        /// <returns>Returns TRUE if the operation was successful.  Returns FALSE if the operation failed and needed to be rolled back.</returns>
        private bool InsertLinkedUtilityAccount()
        {
            try
            {
                _newLinkedUtilityAccount.WssAccountId = _newWssAccount.WSSAccountId;
                _newLinkedUtilityAccount.UtilityAccountId = _utilityAccountId;
                _newLinkedUtilityAccount.DefaultAccount = true;
                _wssUnitOfWork.LinkedUtilityAccountsRepository.Insert(_newLinkedUtilityAccount);
                //TODO Call this with the actual login of the logged in user
                _wssUnitOfWork.Save("0");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurred when adding the LinkedUtilityAccount entry for Account Number: {UtilityBillingAccountNumber}, e-mail address: {EmailAddress}", ex);
                return false;
            }
        }
    }
}