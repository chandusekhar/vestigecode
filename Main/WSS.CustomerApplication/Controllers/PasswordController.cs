using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities;
using WSS.Common.Utilities.ActionLink;
using WSS.CustomerApplication.Helper;
using WSS.CustomerApplication.Models;
using WSS.CustomerApplication.Properties;
using WSS.Data;
using WSS.Data.Enum;
using WSS.Email.Service;
using WSS.Identity;
using WSS.Logging.Service;
using Constants = WSS.Common.Utilities.Constants;
using AutoMapper;
using System.Collections.Generic;
using System.IO;

using System.Text.RegularExpressions;

namespace WSS.CustomerApplication.Controllers
{
    public class PasswordController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTransaction _auditTransaction;
        private static readonly ILogger Logger = new Logger(typeof(PasswordController));
        private WssIdentityUserManager _identityUserManager;

        public WssIdentityUserManager IdentityUserManager
        {
            get
            {
                return _identityUserManager ?? HttpContext.GetOwinContext().GetUserManager<WssIdentityUserManager>();
            }
            private set
            {
                _identityUserManager = value;
            }
        }


        public PasswordController(IUnitOfWork unitOfWork, IAuditTransaction auditTransaction, ISendEmail sendEmail) : base(sendEmail)
        {
            _unitOfWork = unitOfWork;
            //_wssUserManager = wssUserManager;
            _auditTransaction = auditTransaction;
        }
        //Get: Credentials
        [HttpGet]
        public ActionResult Index(string emailAddress, string attemptsLeft)
        {
            var model = new ForgotPassowrdViewModel();

            ViewBag.IsMenuHidden = "hidden";
            ViewBag.WizardId = "First";
            return View(model);
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassowrdViewModel model)
        {

            ViewBag.WizardId = "First";
            if (Convert.ToInt32(Session["CurrentAttempt"]) >= 3 || model.AttemptsLeft < 0)
            {
                ModelState.AddModelError("EmailAddress", Resources.PasswordController_ForgotPassword_Attempts_count_Exceeded_Session_Locked);

                return View("Index", model);
            }

            var records =
                _unitOfWork.WssAccountRepository.FindAll()?.Where(x => x.PrimaryEmailAddress == model.EmailAddress);

            if (records == null || !records.Any())
            {

                ModelState.AddModelError("EmailAddress",
                   Resources.PasswordController_ForgotPassword_Email_address_is_not_found);

                Logger.Error("Failed to find the Web Self-Service profile for E-mail address: " + model.EmailAddress);
                AddMessage(UserMessageType.Error,
                    Resources.PasswordController_ForgotPassword_Failed_to_find_the_Web_Self_Service_profile_for_E_mail_address__ + model.EmailAddress);
                return View("Index", model);
            }

            var wssAccount = _unitOfWork.WssAccountRepository.FindAll()
              .FirstOrDefault(x => x.PrimaryEmailAddress == model.EmailAddress);

            if (wssAccount?.WssAccountStatusCode == AccountStatus.ACT.ToString() ||
                wssAccount?.WssAccountStatusCode == AccountStatus.LCKD.ToString())
            {
                var wssAccountId = records.FirstOrDefault().WSSAccountId;
                var actiontoken = Guid.NewGuid().ToString();
                InsertAction(wssAccountId, actiontoken);

                var sendEmailSuccess = SendForgotPasswordEmail(wssAccountId, model.EmailAddress, actiontoken);

                var oldValue = string.Empty;
                var newValue = string.Empty;
                var userName = model.EmailAddress;
                var fieldName = EventType.ResetPassword;
                var description = EventDescriptionHelper.ResetPasswordEmail;
                _auditTransaction.AddAuditRecord(fieldName,
                    AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.RSTPWDEMAIL,
                    AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssAccountId, userName, description);

                _unitOfWork.Save("0");

                if (sendEmailSuccess)
                {
                    Session["CurrentAttempt"] = Session["CurrentAttempt"] == null
                        ? 1
                        : Convert.ToInt32(Session["CurrentAttempt"]) + 1;
                    model.AttemptsLeft = 3 - Convert.ToInt32(Session["CurrentAttempt"]);
                    ModelState.AddModelError("AttemptsLeft",
                        String.Format(Resources.PasswordController_ForgotPassword_You_have__0__attempts_remaining, model.AttemptsLeft));
                    model.IsForgotPasswordMailSent = true;
                    AddMessage(UserMessageType.Success,
                        String.Format(
                            Resources.PasswordController_ForgotPassword_Success__A_link_to_reset_your_password__has_been_sent_to_your_E_mail_address___0_,
                            model.EmailAddress));
                }
            }
            else
            {
                if (wssAccount?.WssAccountStatusCode != null)
                {
                    if (wssAccount.WssAccountStatusCode.Equals(AccountStatus.REG.ToString()))
                    {
                        Logger.Debug(
                            $"Account was not in Activated status when sending Forgot password e-mail.  Email Address: {model.EmailAddress}, Account Number: {wssAccount.WSSAccountId}, Account Status: {wssAccount.WssAccountStatusCode}");
                        ModelState.AddModelError("EmailAddress",
                            Resources.PasswordController_ForgotPassword_The_account_is_not_been_activated_);
                        AddMessage(UserMessageType.Warning,
                            String.Format(Resources.PasswordController_ForgotPassword_Web_Self_service_profile_for_E_mail_address___0__has_not_been_activated_,
                                model.EmailAddress));
                    }
                    if (wssAccount.WssAccountStatusCode.Equals(AccountStatus.UNSUB.ToString()))
                    {
                        Logger.Debug($"Account was not in Activated status when sending Forgot password e-mail.  Email Address: {model.EmailAddress}, Account Number: {wssAccount.WSSAccountId}, Account Status: {wssAccount.WssAccountStatusCode}");
                        ModelState.AddModelError("EmailAddress",
                           Resources.PasswordController_ForgotPassword_Email_address_not_found);
                        AddMessage(UserMessageType.Warning,
                            String.Format(Resources.PasswordController_ForgotPassword_Failed_to_find_Web_self_service_profile_for_E_mail_address___0__,
                                model.EmailAddress));
                    }
                }
                else
                {
                    AddMessage(UserMessageType.Warning,
                        String.Format(
                            Resources.PasswordController_ForgotPassword_Failed_to_send_a_link_to_reset_your_password_for_E_mail_address___0____Please_attempt_to_resend_the_Forgot_Password_Link_again_,
                            model.EmailAddress));
                }
            }
            return View("Index", model);
        }

        // GET: ResetPassword
        [HttpGet]
        public ActionResult ResetPassword(int accountId, string activationToken)
        {
            activationToken = activationToken.Replace("\"", "");

            ViewBag.WizardId = "Second";
            var model = new ForgotPassowrdViewModel
            {
                WssAccountId = accountId,
                Actiontoken = activationToken
            };

            var action =
                _unitOfWork.ActionDataRepository.FindAll()
                    .FirstOrDefault(x => x.WssAccountId == accountId && x.ActionToken == activationToken);
            var wssAccount = _unitOfWork.WssAccountRepository.FindAll()
                .FirstOrDefault(x => x.WSSAccountId == accountId);
            if (action == null || wssAccount == null || action.ExpiryDateTime <= DateTime.Now)
            {
                Logger.Error(
                   $"Failed to reset password of the Web Self-Service profile for E-mail address: { model.EmailAddress}");
                AddMessage(UserMessageType.Error,
                    String.Format(Resources.PasswordController_ResetPassword_Failed_to_reset_password_of_the_Web_Self_Service_profile_for_E_mail_address___0_,
                        model.EmailAddress));

                return View("LinkExpired");
                //ViewBag.WizardId = "First";
            }
            else
            {
                model.EmailAddress = wssAccount.PrimaryEmailAddress;
                model.SecurityQuestion = wssAccount.SecurityQuestion;
                model.WssAccountId = wssAccount.WSSAccountId;
                ViewBag.WizardId = "Second";
            }

            return View("Index", model);
        }

        public ActionResult ResetPasswordNext(ForgotPassowrdViewModel model)
        {
            var action =
                _unitOfWork.ActionDataRepository.FindAll()
                    .FirstOrDefault(x => x.WssAccountId == model.WssAccountId && x.ActionToken == model.Actiontoken);
            var wssAccount = _unitOfWork.WssAccountRepository.FindAll()
                .FirstOrDefault(x => x.WSSAccountId == model.WssAccountId);
            if (action == null || wssAccount == null || action.ExpiryDateTime <= DateTime.Now)
            {
                Logger.Error(
                   $"Failed to reset password of the Web Self-Service profile for E-mail address: { model.EmailAddress}");
                AddMessage(UserMessageType.Error,
                    String.Format(Resources.PasswordController_ResetPasswordNext_Failed_to_reset_password_of_the_Web_Self_Service_profile_for_E_mail_address___0_,
                        model.EmailAddress));
                ViewBag.WizardId = "First";
            }
            else
            {
                if (Convert.ToInt32(Session["CurrentAttempt_SecurityQuestion"]) >= 3)
                {
                    var user = IdentityUserManager.FindByEmail(wssAccount.PrimaryEmailAddress);
                    user.LockoutEnabled = true;
                    user.LockoutEndDateUtc = DateTime.Now.AddHours(48);
                    ModelState.AddModelError("AttemptsLeft", Resources.PasswordController_ResetPasswordNext_Account_Locked);

                    wssAccount.WssAccountStatusCode = AccountStatus.LCKD.ToString();

                    var userName = model.EmailAddress;
                    var wssAccountId = model.WssAccountId;
                    var fieldName = EventType.AccountLocked;
                    var description = EventDescriptionHelper.AccountLocked;
                    _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.ACCTLOCKD, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                        null, null, wssAccountId, userName, description);

                    _unitOfWork.Save(User?.Identity.Name ?? "");

                    ViewBag.WizardId = "Second";
                }
                else
                {
                    Session["CurrentAttempt_SecurityQuestion"] = Session["CurrentAttempt_SecurityQuestion"] == null ? 1 : Convert.ToInt32(Session["CurrentAttempt_SecurityQuestion"]) + 1;


                    var securityQuestionValidation =
                        _unitOfWork.WssAccountRepository.FindAll()?
                            .FirstOrDefault(x => x.SecurityQuestionAnswer.ToLower() == model.SecurityAnswer.ToLower() && x.WSSAccountId == model.WssAccountId);

                    if (securityQuestionValidation != null && (securityQuestionValidation.IsActive && securityQuestionValidation.SecurityQuestionAnswer.ToLower() == model.SecurityAnswer.ToLower()))
                    {
                        model.EmailAddress = securityQuestionValidation.PrimaryEmailAddress;
                        ModelState.Clear();
                        ViewBag.WizardId = "Third";

                    }
                    else
                    {
                        model.AttemptsLeft = 3 - Convert.ToInt32(Session["CurrentAttempt_SecurityQuestion"]);
                        ModelState.AddModelError("AttemptsLeft",
                            String.Format(Resources.PasswordController_ResetPasswordNext_You_have__0__attempts_remaining, model.AttemptsLeft));
                        ViewBag.WizardId = "Second";
                    }
                }
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Confirm(ForgotPassowrdViewModel model)
        {
            ViewBag.WizardId = "Third";
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("confirmpassword", Resources.PasswordController_Confirm_Confirm_password_does_not_match_new_password_);
            }
            if (ModelState.IsValid)
            {
                var wssAccount = _unitOfWork.WssAccountRepository.FindAll().FirstOrDefault(x => x.WSSAccountId == model.WssAccountId);
                var primaryEmailAddress = wssAccount?.PrimaryEmailAddress;
                var user = IdentityUserManager.FindByEmail(primaryEmailAddress);
                string code = IdentityUserManager.GeneratePasswordResetToken(user.Id.ToString());
                if (user.LockoutEnabled)
                {
                    IdentityUserManager.SetLockoutEnabled(user.Id, false);
                }
                var identityResult = IdentityUserManager.ResetPassword(user.Id, code, model.Password);
                if (identityResult.Equals(false))
                {
                    Logger.Error($"Failed to change password of Identity account for authentication. Password: {model.Password}");
                    AddMessage(UserMessageType.Error,
                    Resources.PasswordController_Confirm_Unable_to_change_password_for_your_Web_Self_service_account___Please_try_again___If_the_problem_persists__try_to_resend_your_Forgot_password_link__or_try_to_register_again_,
                    "Activation Failed");
                    return RedirectToAction("Index");
                }
                if (wssAccount.WssAccountStatusCode == "LCKD")
                {
                    wssAccount.WssAccountStatusCode = "ACT";
                    _unitOfWork.Save(User.Identity.Name);
                }
                //Save to Audit
                var oldValue = string.Empty;
                var newValue = string.Empty;
                var userName = model.EmailAddress;
                var wssAccountId = model.WssAccountId;
                var fieldName = EventType.ResetPassword;
                var description = EventDescriptionHelper.ResetPassword;
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.RSTPWD, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssAccountId, userName, description);

                _unitOfWork.Save("0");

                sendEmail.AddSingleEmail("WSS.ChangePasswordNotification", $"TO!{primaryEmailAddress}");
                AddMessage(UserMessageType.Success,
                    String.Format(
                        Resources.PasswordController_Confirm_Success__Password_is_reset_for_the_Web_Self_Service_account_with_the_E_mail_address___0_,
                        model.EmailAddress));
                return RedirectToAction("Index", "Login");
            }
            return View("Index", model);
        }

        public bool SendForgotPasswordEmail(int accountId, string emailAddress, string activationToken)
        {
            try
            {
                var forgotpasswordLink =
                    $"{ConfigurationManager.AppSettings["BaseUrl"]}Password/ResetPassword/?accountId={accountId}&activationToken={activationToken}";
                var keyValuePair = $"TO!{emailAddress},changePasswordLink!{forgotpasswordLink},<resetPasswordLink>!{forgotpasswordLink}";
                sendEmail.AddSingleEmail("WSS.ChangePassword", keyValuePair);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool InsertAction(int accountId, string actiontoken)
        {
            var action = new Data.Action()
            {
                WssAccountId = accountId,
                ActionToken = actiontoken,
                ActionName = ActionType.RP.ToString(),
                ExpiryDateTime = DateTime.Now.AddHours(Constants.ExpiryHoursResetPassword)
            };

            _unitOfWork.ActionDataRepository.Insert(action);
            _unitOfWork.Save("0");
            return true;
        }

    }
}