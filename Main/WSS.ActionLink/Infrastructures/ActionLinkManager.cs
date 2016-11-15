using System;
using System.Configuration;
using System.Linq;
using System.Web;
using WSS.Data;
using Action = WSS.Data.Action;

namespace WSS.ActionLink.Infrastructures
{
    public enum ActionType
    {
        UP, // Unsubscribe Primary email address
        US, // Unsubscribe Secondary email address
        AA, // Account Activation
        RP // Reset Password
    }

    public class ActionLinkManager : IActionLinkManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionLinkManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Generates a Reset Password Action and associated link for the WssAccount which has the provided
        ///     primaryEmailAddress.
        /// </summary>
        /// <param name="wssAccountId"></param>
        /// <returns>
        ///     A string containing the generated URL.  Null if the wssAccount for the provided primaryEmailAddress cannot be
        ///     found.
        /// </returns>
        public string GenerateResetPasswordLink(int wssAccountId)
        {
            var wssAccount =
                _unitOfWork.WssAccountRepository
                    .Get(wssAccountId);

            if (wssAccount == null)
            {
                return null;
            }

            var action = new Action
            {
                ActionToken = Guid.NewGuid().ToString().Replace("-", string.Empty),
                ActionName = ActionType.RP.ToString(),
                WssAccountId = wssAccount.WSSAccountId,
                ExpiryDateTime = DateTime.Now.AddHours(Constants.ExpiryHoursResetPassword)
            };

            _unitOfWork.ActionDataRepository.Insert(action);
            _unitOfWork.Save("0");
                   
            return $"{ConfigurationManager.AppSettings["CustomerUrl"] + "Password/ResetPassword?activationToken="}{action.ActionToken}&accountId=" + wssAccountId;
        }

        /// <summary>
        ///     Generates an Activate Account Action and associated link for the WssAccount which has the provided WssAccountId.
        /// </summary>
        /// <param name="wssAccountId"></param>
        /// <returns>A string containing the generated URL.  Null if the wssAccount for the provided wssAccountId cannot be found.</returns>
        public string GenerateActivateAccountLink(int wssAccountId)
        {
            var wssAccount =
                _unitOfWork.WssAccountRepository.Get(wssAccountId);

            if (wssAccount == null)
            {
                return null;
            }

            var currentAction =
                _unitOfWork.ActionDataRepository.FindAll().FirstOrDefault(x => x.WssAccountId == wssAccountId);

            if (currentAction != null)
            {
                _unitOfWork.ActionDataRepository.Delete(currentAction);
            }

            var action = new Action
            {
                ActionToken = Guid.NewGuid().ToString().Replace("-", string.Empty),
                ActionName = ActionType.AA.ToString(),
                WssAccountId = wssAccount.WSSAccountId,
                ExpiryDateTime = DateTime.Now.AddHours(Constants.ExpiryHoursActivateAccount)
            };

            _unitOfWork.ActionDataRepository.Insert(action);
            _unitOfWork.Save("0");

            return $"{ConfigurationManager.AppSettings["BaseUrl"]}ActivateAccount/?key={action.ActionToken}";
        }

        /// <summary>
        ///     Generates an Unsubscribe Secondary Email Address Action and associated link for the WssAccount which has the
        ///     provided WssAccountId, and the provided secondary email address.
        /// </summary>
        /// <param name="wssAccountId"></param>
        /// <param name="secondaryEmailAddress"></param>
        /// <returns>
        ///     A string containing the generated URL.  Null if the wssAccount for the provided primaryEmailAddress cannot be
        ///     found, if the provided secondary email address cannot be found, or if the provided secondary email address is not
        ///     associated with the provided wssAccount account
        /// </returns>
        public string GenerateUnsubscribeSecondaryLink(int wssAccountId, string secondaryEmailAddress)
        {
            var wssAccount = _unitOfWork.WssAccountRepository.Get(wssAccountId);

            if (wssAccount == null)
            {
                return null;
            }

            var additionalEmailAddress = _unitOfWork.AdditionalEmailAddressRepository
                .FindAll()
                .Where(x => x.WssAccountId == wssAccount.WSSAccountId)
                .FirstOrDefault(x => x.EmailAddress.Equals(secondaryEmailAddress));

            return additionalEmailAddress == null
                ? null
                : $"{ConfigurationManager.AppSettings["BaseUrl"]}{wssAccount.AuthenticationId.ToString().Replace("-", string.Empty)}?A=US;SA={HttpUtility.UrlEncode(secondaryEmailAddress)}";
        }

        /// <summary>
        ///     Generates an Unsubscribe Primary Email Address Action and associated link for the WssAccount which has the provided
        ///     WssAccountId.
        /// </summary>
        /// <param name="wssAccountId"></param>
        /// <returns></returns>
        public string GenerateUnsubscribePrimaryLink(int wssAccountId)
        {
            var wssAccount =
                _unitOfWork.WssAccountRepository
                    .FindAll()
                    .FirstOrDefault(x => x.WSSAccountId == wssAccountId);

            return wssAccount == null
                ? null
                : $"{ConfigurationManager.AppSettings["BaseUrl"]}{wssAccount.AuthenticationId.Replace("-", string.Empty)}?A=UP";
        }
    }
}