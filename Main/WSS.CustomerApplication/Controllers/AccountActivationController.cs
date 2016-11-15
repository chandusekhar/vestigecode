using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities;
using WSS.CustomerApplication.Helper;
using WSS.CustomerApplication.Models;
using WSS.CustomerApplication.Properties;
using WSS.Data;
using WSS.Email.Service;
using WSS.Identity;
using WSS.Logging.Service;

namespace WSS.CustomerApplication.Controllers
{
    public class AccountActivationController : BaseController
    {
        private static readonly ILogger Logger = new Logger(typeof(AccountActivationController));

        // GET: AccountActivation
        private readonly IUnitOfWork _wssUnitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _utilityBillingUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditTransaction _auditTransaction;

        private WssIdentitySignInManager _identitySignInManager;
        private WssIdentityUserManager _identityUserManager;

        public WssIdentitySignInManager IdentitySignInManager
        {
            get
            {
                return _identitySignInManager ?? HttpContext.GetOwinContext().Get<WssIdentitySignInManager>();
            }
            private set
            {
                _identitySignInManager = value;
            }
        }

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

        public AccountActivationController(IUnitOfWork wssUnitOfWork, UtilityBilling.Data.IUnitOfWork utilityBillingUnitOfWork, IMapper mapper, ISendEmail sendEmail, IAuditTransaction auditTransaction) : base(sendEmail)
        {
            _wssUnitOfWork = wssUnitOfWork;
            _utilityBillingUnitOfWork = utilityBillingUnitOfWork;
            _mapper = mapper;
            _auditTransaction = auditTransaction;
        }

        public ActionResult Index(string key)
        {
            ViewBag.BannerFileName = GetBanner();
            var objActionData = _wssUnitOfWork.ActionDataRepository.FindAll().FirstOrDefault(x => x.ActionToken == key);

            // check if the request didn't expire 
            if (objActionData != null && objActionData.ExpiryDateTime.HasValue && DateTime.Now.Subtract(objActionData.ExpiryDateTime.Value).TotalMinutes <= 0)
            {
                var wssAccount = _wssUnitOfWork.WssAccountRepository.FindAll().FirstOrDefault(x => x.WSSAccountId == objActionData.WssAccountId);
                if (wssAccount != null)
                {
                    switch (wssAccount.WssAccountStatusCode)
                    {
                        case "REG":
                            Session["IsAccountLocked"] = false;
                            break;
                        case "LCKD":
                            Session["IsAccountLocked"] = true;
                            break;
                        default:
                            var viewData = new System.Web.Routing.RouteValueDictionary();
                            viewData.Add("AccountActivationError", "The account is not in a valid state for activation");
                            return RedirectToAction("ErrorPage", "Error", viewData);
                    }
                }
               
                // not expired 
                ViewBag.Questions = GetQuestions();
                var model = new AccountActivationViewModel();
                model.Token = key;
                return View("Complete", model);
            }
            else
            {
                // expired
                return View("Expired");
            }
        }

        private Dictionary<string, string> GetQuestions()
        {
            var list = new Dictionary<string, string>();

            //     TODO: for now hardcoded until a decision is made on where to store: database, file, enumerable 
            list.Add(Resources.AccountActivationController_GetQuestions_What_was_the_name_of_your_favorite_childhood_friend_, Resources.AccountActivationController_GetQuestions_What_was_the_name_of_your_favorite_childhood_friend_);
            list.Add(Resources.AccountActivationController_GetQuestions_What_was_the_name_of_your_elementary_school_, Resources.AccountActivationController_GetQuestions_What_was_the_name_of_your_elementary_school_);
            list.Add(Resources.AccountActivationController_GetQuestions_Who_is_your_favorite_fictional_character_, Resources.AccountActivationController_GetQuestions_Who_is_your_favorite_fictional_character_);
            list.Add(Resources.AccountActivationController_GetQuestions_What_is_your_mother_s_maiden_name_, Resources.AccountActivationController_GetQuestions_What_is_your_mother_s_maiden_name_);
            list.Add(Resources.AccountActivationController_GetQuestions_What_is_the_name_of_your_first_pet_, Resources.AccountActivationController_GetQuestions_What_is_the_name_of_your_first_pet_);
            list.Add(Resources.AccountActivationController_GetQuestions_Who_is_your_favorite_actor_or_actress_, Resources.AccountActivationController_GetQuestions_Who_is_your_favorite_actor_or_actress_);
            list.Add(Resources.AccountActivationController_GetQuestions_What_is_your_father_s_middle_name_, Resources.AccountActivationController_GetQuestions_What_is_your_father_s_middle_name_);
            list.Add(Resources.AccountActivationController_GetQuestions_What_is_your_favorite_flavor_of_ice_cream_, Resources.AccountActivationController_GetQuestions_What_is_your_favorite_flavor_of_ice_cream_);

            return list;
        }

        public ActionResult Activate(AccountActivationViewModel model)
        {
            model = VerifyCheckboxes(model);

            if (ModelState.IsValid)
            {
                var objActionData = _wssUnitOfWork.ActionDataRepository.FindAll().FirstOrDefault(x => x.ActionToken == model.Token);
                if (objActionData != null)
                {
                    try
                    {
                        model.WssAccountId = objActionData.WssAccountId;
                        var wssAccountModel =
                            _wssUnitOfWork.WssAccountRepository.FindAll()
                                .FirstOrDefault(x => x.WSSAccountId == model.WssAccountId);

                        if (wssAccountModel == null)
                        {
                            Logger.Error($"Unable to find WSS Account for WssAccountId: {model.WssAccountId}");
                            AddMessage(UserMessageType.Error,
                            Resources.AccountActivationController_Activate_The_web_self_service_account_could_not_be_found___Please_try_again___If_the_problem_persists__try_to_resend_your_activation_link__or_try_to_register_again_,
                            Resources.AccountActivationController_Activate_Activation_Failed);
                            return RedirectToAction("Index");
                        }

                        var linkedUtilityAccountIds =
                            _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                                .Where(x => x.WssAccountId == wssAccountModel.WSSAccountId)
                                .Select(x => x.UtilityAccountId).ToList();

                        var utilityAccounts =
                            _utilityBillingUnitOfWork.UtilityAccountRepository.FindAll()
                                .Where(x => linkedUtilityAccountIds.Contains(x.UtilityAccountId));

                        wssAccountModel.AgreeTermsAndConditionsDate = DateTime.Now;
                        wssAccountModel.SecurityQuestion = model.SecurityQuestion;
                        wssAccountModel.SecurityQuestionAnswer = model.SecurityQuestionAnswer;
                        wssAccountModel.WssAccountStatusCode = "ACT";
                        wssAccountModel.IsActive = true;

                        // Create the Account via ASP.Net Identity
                        // var identityResult = CreateIdentityUserAsync(wssAccountModel.PrimaryEmailAddress, model.Password2);
                        IdentityResult identityResult;
                        WssIdentityUser user = null;
                        if (Session["IsAccountLocked"] != null && Convert.ToBoolean(Session["IsAccountLocked"]) == true)
                        {
                            user = IdentityUserManager.FindByEmail(wssAccountModel.PrimaryEmailAddress);
                            var code = IdentityUserManager.GeneratePasswordResetToken(user.Id.ToString());
                            IdentityUserManager.ResetPassword(user.Id, code, model.Password1);
                            identityResult = IdentityUserManager.SetLockoutEnabled(user.Id, false);

                        }
                        else //if (Session["IsAccountLocked"] != null && Convert.ToBoolean(Session["IsAccountLocked"]) == false)
                        {
                            user = new WssIdentityUser() { UserName = wssAccountModel.PrimaryEmailAddress, Email = wssAccountModel.PrimaryEmailAddress };
                            identityResult = IdentityUserManager.Create(user, model.Password2);
                        }
                        if (!identityResult.Succeeded)
                        {
                            Logger.Error($"Failed to create Identity account for authentication.  Username: {user.UserName}, Password: {model.Password2}");
                            AddMessage(UserMessageType.Error,
                            Resources.AccountActivationController_Activate_Unable_to_create_the_username_and_password_for_your_Web_Self_service_account___Please_try_again___If_the_problem_persists__try_to_resend_your_activation_link__or_try_to_register_again_,
                            Resources.AccountActivationController_Activate_Activation_Failed);
                            return RedirectToAction("Index");
                        }

                        // Update the Auth ID in the WssAccount record (need the existing Identity account)
                        var authenticationId = IdentityUserManager.FindByEmail(wssAccountModel.PrimaryEmailAddress)?.Id;
                        if (null == authenticationId)
                        {
                            IdentityUserManager.Delete(
                                IdentityUserManager.FindByEmail(wssAccountModel.PrimaryEmailAddress));

                            Logger.Error($"Failed to retrive Identity account after creation.  Username: {user.UserName}, Password: {model.Password2}");
                            AddMessage(UserMessageType.Error,
                            Resources.AccountActivationController_Activate_Unable_to_create_the_username_and_password_for_your_Web_Self_service_account___Please_try_again___If_the_problem_persists__try_to_resend_your_activation_link__or_try_to_register_again_,
                            Resources.AccountActivationController_Activate_Activation_Failed);
                            return RedirectToAction("Index");
                        }
                        wssAccountModel.AuthenticationId = authenticationId;

                        // Update the SubscriptionTransaction table
                        var subscriptionTransactionRecord = new SubscriptionTransaction
                        {
                            ccb_acct_id = utilityAccounts.FirstOrDefault()?.ccb_acct_id ?? string.Empty,
                            SubscriptionTransactionDate = DateTime.Now,
                            SubscriptionTransactionStatusId = 1,
                            SubscriptionTransactionTypeCode = "SUB"
                        };

                        _wssUnitOfWork.SubscriptionTransactionRepository.Insert(subscriptionTransactionRecord);

                        _auditTransaction.AddAuditRecord(EventType.AccountActivated, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.ACTVTN,
                    AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                            String.Empty, Convert.ToString(objActionData.WssAccountId), objActionData.WssAccountId, objActionData.WssAccount.PrimaryEmailAddress, EventDescriptionHelper.ActivateProfile);

                        // delete all records for the account from Action table
                        var allActionRecordsForTheAccount =
                            _wssUnitOfWork.ActionDataRepository.FindAll()
                                .Where(x => x.WssAccountId == model.WssAccountId)
                                .ToList();

                        foreach (var record in allActionRecordsForTheAccount)
                        {
                            _wssUnitOfWork.ActionDataRepository.Delete(record);
                        }

                        _wssUnitOfWork.Save("0");

                        var ccbAcctId = utilityAccounts.FirstOrDefault()?.ccb_acct_id ?? string.Empty;
                        var reg = new Regex(@"[0-9]");
                        var ccbAccount = reg.Replace(ccbAcctId ?? string.Empty, "X", 4, 3);
                        ccbAccount = ccbAccount + 'X';

                        sendEmail.AddSingleEmail("WSS.AccountActivatedNotification", $"TO!{wssAccountModel.PrimaryEmailAddress},<CCBAccount>!{ccbAccount}");

                        Logger.Info(
                        "Successfully activated Account Id: {0}, E-mail address: {1}", model.WssAccountId, wssAccountModel.PrimaryEmailAddress);

                        AddMessage(UserMessageType.Success,
                            Resources.AccountActivationController_Activate_Web_Self_Serv_account_has_been_successfully_registered,
                            "Registration Successful");

                        return RedirectToAction("Index", "Account", new { id = objActionData.WssAccountId.ToString() });

                        //return Json(new { Message = "Your account has been successfully registered", success=true, relocate="Account/Index/"+ objActionData.WssAccountId }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(
                            $"Exception occurred during registration of the Web Self-Service profile failed for Account Number: {model.WssAccountId}.", ex);
                        AddMessage(UserMessageType.Error, Resources.AccountActivationController_Activate_An_exception_occurred_during_activation_of_the_account___Please_try_again___If_the_problem_persists__please_try_to_resend_your_activation_e_mail__or_try_to_register_again_, Resources.AccountActivationController_Activate_Registration_Unsuccessful);
                        return RedirectToAction("Index");
                    }
                }
            }
            AddMessage(UserMessageType.Error, Resources.AccountActivationController_Activate_There_was_a_problem_processing_the_activation_of_your_account___Please_check_the_information_you_entered_and_try_again_, Resources.AccountActivationController_Activate_Registration_Unsuccessful);
            return RedirectToAction("Index");
        }

        private AccountActivationViewModel VerifyCheckboxes(AccountActivationViewModel model)
        {
            if (!model.AgreeToTermsAndConditions)
            {
                ModelState.AddModelError("CustomError", Resources.AccountActivationController_VerifyCheckboxes_You_have_to_accept_the_terms_and_conditions);
            }

            if (model.SecurityQuestionAnswer != null && model.SecurityQuestionAnswer.Trim().Length <= 0)
            {
                ModelState.AddModelError("CustomError", Resources.AccountActivationController_VerifyCheckboxes_You_have_to_enter_security_question_answer_);
            }
            return model;
        }

        /// <summary>
        /// Loops a folder where banner immages are stored, creates an array 
        /// of banner names and stores the array in TempData
        /// </summary>
        /// <returns></returns>
        private string GetBanner()
        {
            var files = new List<string>();
            var bannerFileName = string.Empty;

            if (TempData["bannerFiles"] == null)
            {
                var path = Path.Combine(Server.MapPath("~/Content/Images/Banners"));
                var dirInfo = new DirectoryInfo(path);

                foreach (FileInfo fInfo in dirInfo.GetFiles())
                {
                    var fileExtention = fInfo.Name.Split('.')[1].ToLower();
                    if (fileExtention == "gif" || fileExtention == "jpg" || fileExtention == "png")
                    {
                        files.Add(fInfo.Name);
                    }
                }
                TempData.Keep("bannerFiles");
            }
            else
            {
                files = (List<string>)TempData["bannerFiles"];
            }

            if (files.Count > 0)
            {
                var random = new Random();
                bannerFileName = files[random.Next(files.Count)];
            }
            return bannerFileName;
        }
    }
}