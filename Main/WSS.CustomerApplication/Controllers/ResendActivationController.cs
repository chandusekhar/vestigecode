using System;
using System.Linq;
using System.Web.Mvc;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities;
using WSS.Common.Utilities.ActionLink;
using WSS.CustomerApplication.Helper;
using WSS.CustomerApplication.Models;
using WSS.CustomerApplication.Properties;
using WSS.Data;
using WSS.Data.Enum;
using WSS.Email.Service;
using WSS.Logging.Service;

namespace WSS.CustomerApplication.Controllers
{
    public class ResendActivationController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTransaction _auditTransaction;
        private readonly IActionLinkManager _actionLinkManager;
        private static readonly ILogger Logger = new Logger(typeof(PasswordController));

        public ResendActivationController(IUnitOfWork unitOfWork,
           IAuditTransaction auditTransaction, ISendEmail sendEmail, IActionLinkManager actionLinkManager) : base(sendEmail)
        {
            _unitOfWork = unitOfWork;
            _auditTransaction = auditTransaction;
            _actionLinkManager = actionLinkManager;
        }

        // GET: ResendActivation      
        [HttpGet]
        public ActionResult Index(string emailAddress, string attemptsLeft)
        {
            var model = new ResendActivationEmailViewModel();

            ViewBag.IsMenuHidden = "hidden";
            ViewBag.WizardId = "First";
            return View(model);
        }

        [HttpPost]
        public ActionResult ActivationEmail(ResendActivationEmailViewModel model)
        {

            ViewBag.WizardId = "First";
            if (Convert.ToInt32(Session["CurrentAttempt_Resend"]) >= 3 || model.AttemptsLeft < 0)
            {
                ModelState.AddModelError("EmailAddress", Resources.ResendActivationController_ActivationEmail_Attempts_count_Exceeded_Session_Locked);

                return View("Index", model);
            }


            var records =
                _unitOfWork.WssAccountRepository.FindAll()?.Where(x => x.PrimaryEmailAddress == model.EmailAddress);

            if (records == null || !records.Any())
            {

                ModelState.AddModelError("EmailAddress",
                   Resources.ResendActivationController_ActivationEmail_Email_address_is_not_found);

                Logger.Debug("Failed to find the Web Self-Service profile for E-mail address: " + model.EmailAddress);
                AddMessage(UserMessageType.Error,
                   String.Format(Resources.ResendActivationController_ActivationEmail_Failed_to_find_the_Web_Self_Service_profile_for_E_mail_address___0__, model.EmailAddress));
                return View("Index", model);
            }


            var wssAccount = records.FirstOrDefault(x => x.IsActive);

            if (wssAccount == null)
            {
                Logger.Debug($"Failed to find WSS Account when resending activation e-mail.  Email Address: {model.EmailAddress}");
                AddMessage(UserMessageType.Warning,
                    String.Format(
                        Resources.ResendActivationController_ActivationEmail_Could_not_find_the_Web_Self_service_account_for_E_mail_address___0___Please_check_the_e_mail_address_you_entered_and_try_again__If_the_problem_persists__please_try_to_complete_a_new_registration_,
                        model.EmailAddress));
                return View("Index", model);
            }


            if (!wssAccount.WssAccountStatusCode.Equals(AccountStatus.REG.ToString()))
            {
                Logger.Debug($"Account was not in Registered status when resending activation e-mail.  Email Address: {model.EmailAddress}, Account Number: {wssAccount.WSSAccountId}, Account Status: {wssAccount.WssAccountStatusCode}");
                ModelState.AddModelError("EmailAddress",
                    Resources.ResendActivationController_ActivationEmail_The_account_is_not_in_a_state_which_can_be_registered);
                return View("Index", model);
            }

            var sendEmailSuccess = SendResendActivationEmail(wssAccount.WSSAccountId, model.EmailAddress);

            if (!sendEmailSuccess)
            {
                Logger.Error($"Failed to send activation e-mail when resending activation e-mail.  Account ID: {wssAccount.WSSAccountId}, Email Address: {model.EmailAddress}");
                AddMessage(UserMessageType.Error,
                    String.Format(
                        Resources.ResendActivationController_ActivationEmail_The_attempt_to_resend_your_account_activation_link_failed_for_E_mail_address___0___Please_try_again_,
                        model.EmailAddress));
                return View("Index", model);
            }

            Session["CurrentAttempt_Resend"] = Session["CurrentAttempt_Resend"] == null ? 1 : Convert.ToInt32(Session["CurrentAttempt_Resend"]) + 1;
            model.AttemptsLeft = 3 - Convert.ToInt32(Session["CurrentAttempt_Resend"]);
            ModelState.AddModelError("AttemptsLeft",
                String.Format(Resources.ResendActivationController_ActivationEmail_You_have__0__attempts_remaining, model.AttemptsLeft));
            model.IsResendAvtivationMailSent = true;

            var oldValue = Convert.ToString(wssAccount.WSSAccountId);
            var newValue = string.Empty;
            var wssaccountId = wssAccount.WSSAccountId;
            var userName = model.EmailAddress;
            var fieldName = EventType.ResendActivation;
            var description = String.Format(EventDescriptionHelper.ResendActivation, wssaccountId);
            _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.RSNDACT, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                oldValue, newValue, wssAccount.WSSAccountId, userName, description);

            _unitOfWork.Save(User.Identity.Name);

            AddMessage(UserMessageType.Success,
                String.Format(Resources.ResendActivationController_ActivationEmail_Success__A_link_to_activate_your_account__has_been_sent_to_your_E_mail_address___0_,
                    model.EmailAddress));

            return View("Index", model);
        }

        public bool SendResendActivationEmail(int wssAccountId, string emailAddress)
        {
            try
            {
                var activationLink = _actionLinkManager.GenerateActivateAccountLink(wssAccountId);                
                var keyValuePair =
                   $"TO!{emailAddress},activationLink!{activationLink},<AccountActivationLink>!{activationLink}";
                sendEmail.AddSingleEmail("WSS.Activation", keyValuePair);
                return true;

            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurred when resending activation email.  Account ID: {wssAccountId}, Email Address: {emailAddress}", ex);
                return false;
            }
        }
    }
}