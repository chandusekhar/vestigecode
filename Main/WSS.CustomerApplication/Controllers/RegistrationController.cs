using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using UtilityBilling.Data;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities;
using WSS.Common.Utilities.ActionLink;
using WSS.Common.Utilities.Transactions;
using WSS.CustomerApplication.ExtetionClasses;
using WSS.CustomerApplication.Helper;
using WSS.CustomerApplication.Infrastructure;
using WSS.CustomerApplication.Models;
using WSS.CustomerApplication.Properties;
using WSS.Email.Service;
using WSS.Logging.Service;
using WSS.User.Service;

namespace WSS.CustomerApplication.Controllers
{
    public class RegistrationController : BaseController
    {
        private static readonly ILogger Logger = new Logger(typeof(RegistrationController));

        private readonly IUnitOfWork _ubUnitofWork;
        private readonly Data.IUnitOfWork _unitOfWork;
        private readonly IWssUserManager _wssUserManager;
        private readonly IAuditTransaction _auditTransaction;

        public RegistrationController(Data.IUnitOfWork unitOfWork, IUnitOfWork ubUnitofWork, ISendEmail emailService,
            IWssUserManager wssUserManager, IAuditTransaction auditTransaction) : base(emailService)
        {
            _unitOfWork = unitOfWork;
            _ubUnitofWork = ubUnitofWork;
            _auditTransaction = auditTransaction;
            _wssUserManager = wssUserManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new RegisterViewModel();
            ViewBag.TabId = 1;
            ViewBag.AccountQuestions = ViewDisplayHelper.GetAccountHolderRelationshipSelectListItems();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterMyProfile(RegisterViewModel model)
        {
            ViewBag.AccountQuestions = ViewDisplayHelper.GetAccountHolderRelationshipSelectListItems();
            model.CcbAccountNumber = model.CcbAccountNumber.Left(10);
            var existingUtilityAccount = _ubUnitofWork.UtilityAccountRepository.FindAll().FirstOrDefault(x => x.ccb_acct_id == model.CcbAccountNumber);

            if (existingUtilityAccount == null)
            {
                ViewBag.TabId = 1;
                AddMessage(UserMessageType.Error, "SDDFF", "SDF");
                return View("Index", model);

            }
            var isAccountRegistered = _unitOfWork.LinkedUtilityAccountsRepository.FindAll()
                .Any(x => x.UtilityAccountId == existingUtilityAccount.UtilityAccountId);

            if (isAccountRegistered)
            {
                ViewBag.TabId = 1;
                AddMessage(UserMessageType.Error, Resources.RegistrationController_RegisterMyProfile_Utility_account_ + model.CcbAccountNumber + Resources.RegistrationController_RegisterMyProfile__already_registered_, Resources.RegistrationController_RegisterMyProfile_Registration);
            }
            else if (CommonMethods.ValidateUtilityAccountNumberAndMeterNumber(_ubUnitofWork, model.CcbAccountNumber, model.MeterNumberForAccount, model.IsMeter))
            {
                ViewBag.TabId = 2;
            }
            else
            {
                ViewBag.TabId = 1;
                if (model.IsMeter)
                    AddMessage(UserMessageType.Error, Resources.RegistrationController_RegisterMyProfile_Invalid_Utility_account_ + model.CcbAccountNumber + Resources.RegistrationController_RegisterMyProfile__or_Meter_Number + model.MeterNumberForAccount, Resources.RegistrationController_RegisterMyProfile_Registration);
                else
                    AddMessage(UserMessageType.Error, Resources.RegistrationController_RegisterMyProfile_Invalid_Utility_account_ + model.CcbAccountNumber, Resources.RegistrationController_RegisterMyProfile_Registration);
            }
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RegisterInformation(RegisterViewModel model)
        {
            ViewBag.TabId = 3;
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            model.CcbAccountNumber = model.CcbAccountNumber.Left(10);
            if (!ModelState.IsValid)
            {
                ViewBag.TabId = 2;
                return View("Index", model);
            }

            var isAvailabeEmailAddress = _unitOfWork.WssAccountRepository.FindAll().Where(m => m.PrimaryEmailAddress.ToLower().Equals(model.EmailAddress.ToLower()) && m.IsActive);
            if (isAvailabeEmailAddress.Any())
            {
                AddMessage(UserMessageType.Error, Resources.RegistrationController_Register_Utility_account_ + model.CcbAccountNumber + Resources.RegistrationController_Register__already_registered, Resources.RegistrationController_Register_Registration);
                ViewBag.TabId = 1;
                return View("Index", model);
            }
            var registrationTransaction = new RegistrationTransaction(_unitOfWork, _ubUnitofWork)
            {
                UtilityBillingAccountNumber = model.CcbAccountNumber,
                EmailAddress = model.EmailAddress,
                UtilityAccountHolderName = model.CustomerName
            };
            try
            {
                var utilityAccount =
                    _ubUnitofWork.UtilityAccountRepository.FindAll()
                        .FirstOrDefault(x => x.ccb_acct_id.Equals(model.CcbAccountNumber));
                if (utilityAccount == null)
                {
                    Logger.Error("Utility Account {0} does not exist", model.CcbAccountNumber);
                    AddMessage(UserMessageType.Error,
                        Resources.RegistrationController_Register_Failed_to_register_the_Web_Self_Service_profile_for_Account_Number__ + model.CcbAccountNumber +
                        Resources.RegistrationController_Register___E_mail_address__ + model.EmailAddress + Resources.RegistrationController_Register___Invalid_utility_account_number, Resources.RegistrationController_Register_Registration);
                    ViewBag.TabId = 1;
                    return View("Index", model);
                }
                var registrationSuccess = registrationTransaction.RegisterExistingUtilityAccount();
                Logger.Debug(
                    $"Registration result for Utility Account Number: {model.CcbAccountNumber}, Customer Name: {model.CustomerName}, E -mail address: {model.EmailAddress} was {registrationSuccess}");

                if (registrationSuccess.Equals(RegistrationTransaction.RegistrationResult.Success))
                {
                    Logger.Info(
                        "Successfully registered the Web Self-Service profile for Account Number: {0}, Customer Name: {1}, E-mail address: {2}",
                        model.CcbAccountNumber, model.CustomerName, model.EmailAddress);

                    var wssAccount =
                        _unitOfWork.WssAccountRepository.FindAll()
                            .FirstOrDefault(x => x.PrimaryEmailAddress.Equals(model.EmailAddress) && x.IsActive);

                    if (wssAccount == null)
                    {
                        Logger.Error(
                            $"Registration of the Web Self-Service profile failed for Account Number: {model.CcbAccountNumber}, Customer Name: {model.CustomerName}, E-mail address: {model.EmailAddress}");
                        AddMessage(UserMessageType.Error,
                            String.Format(
                                Resources.RegistrationController_Register_Registration_of_the_Web_Self_Service_profile_failed_for_Account_Number___0___Customer_Name___1___E_mail_address___2____Please_verify_the_information_that_was_entered_and_try_again_,
                                model.CcbAccountNumber, model.CustomerName, model.EmailAddress));
                        ViewBag.TabId = 1;
                        return View("Index", model);
                    }
                    var wssaccountId = wssAccount.WSSAccountId;
                    var fieldName = EventType.Register;
                    var wssAccountId = wssaccountId;
                    var userName = model.EmailAddress;
                    var description = string.Format(EventDescriptionHelper.Register, userName, model.CcbAccountNumber, model.AboutAccount);
                    _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.REG, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                        String.Empty, String.Empty, wssAccountId, userName, description);

                    _unitOfWork.Save("0");

                    var sendEmailSuccess = SendActivationEmail(wssAccount.WSSAccountId, wssAccount.PrimaryEmailAddress);

                    if (sendEmailSuccess)
                    {
                        AddMessage(UserMessageType.Success,
                            String.Format(
                                Resources.RegistrationController_Register_Successfully_registered_the_Web_Self_Service_profile_for_Account_Number___0____E_mail_address___1_,
                                model.CcbAccountNumber, model.EmailAddress));
                        return RedirectToAction("Index", "Login");
                    }
                    AddMessage(UserMessageType.Warning,
                        String.Format(
                            Resources.RegistrationController_Register_Successfully_registered_the_Web_Self_Service_profile__but_the_Activation_e_mail_message_failed_to_send___Account_Number___0____E_mail_address___1____Please_attempt_to_resend_the_activation_e_mail_from_the_Web_Self_Service_Profile_page_,
                            model.CcbAccountNumber, model.EmailAddress));
                    return RedirectToAction("Index", "Login");
                }
                Logger.Error(
                    "Failed to register the Web Self-Service profile for Account Number: {0}, E-mail address: {1}. There is an error occured while registering wss account",
                    model.CcbAccountNumber, model.EmailAddress);
                AddMessage(UserMessageType.Error,
                    Resources.RegistrationController_Register_Failed_to_register_the_Web_Self_Service_profile_for_Account_Number__ + model.CcbAccountNumber +
                    Resources.RegistrationController_Register___E_mail_address__ + model.EmailAddress + Resources.RegistrationController_Register___There_is_an_error_occured_while_registering_wss_account, Resources.RegistrationController_Register_Registration);
                ViewBag.TabId = 1;
                return View("Index", model);
            }
            catch (Exception ex)
            {
                Logger.Error(
                    "Exception occurred while registering the Web Self-Service profile for Account Number: {0}, E-mail address: {1}.  Exception: {2}",
                    model.CcbAccountNumber, model.EmailAddress, ex.StackTrace);
                AddMessage(UserMessageType.Error,
                    Resources.RegistrationController_Register_Failed_to_register_the_Web_Self_Service_profile_for_Account_Number__ + model.CcbAccountNumber +
                    Resources.RegistrationController_Register___E_mail_address__ + model.EmailAddress);
                ViewBag.TabId = 1;
                return View("Index", model);
            }
        }

        public bool SendActivationEmail(int accountId, string emailAddress)
        {
            var activationToken = Guid.NewGuid().ToString();
            try
            {
                var objAction = new Data.Action()
                {
                    ActionName = ActionType.AA.ToString(),
                    ActionToken = activationToken,
                    WssAccountId = accountId,
                    ExpiryDateTime = DateTime.Now.AddHours(Constants.ExpiryHoursActivateAccount)
                };
                _unitOfWork.ActionDataRepository.Insert(objAction);
                _unitOfWork.Save("0");

                var activationLink =
                    $"{ConfigurationManager.AppSettings["BaseUrl"]}AccountActivation/?key={objAction.ActionToken}";                
                var keyValuePair =
                   $"TO!{emailAddress},activationLink!{activationLink},<AccountActivationLink>!{activationLink}";
                sendEmail.AddSingleEmail("WSS.Activation", keyValuePair);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurred when resending activation email.  Account ID: {accountId}, Email Address: {emailAddress},  activationToken: {activationToken}", ex);
                return false;
            }
        }
    }
}
