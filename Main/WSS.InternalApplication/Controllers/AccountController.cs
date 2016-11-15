
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

using System.Net;
using System.Web.Mvc;
using PagedList;
using UtilityBilling.Data;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities;
using WSS.Common.Utilities.ActionLink;
using WSS.Data;
using WSS.Data.Enum;
using WSS.Email.Service;
using WSS.Identity;
using WSS.InternalApplication.CustomAttributes;
using WSS.InternalApplication.Helper;
using WSS.InternalApplication.Models;
using WSS.InternalApplication.Properties;
using WSS.Logging.Service;
using Constants = WSS.Common.Utilities.Constants;
using EventType = WSS.InternalApplication.Helper.EventType;
using Functions = WSS.InternalApplication.Authorization.Functions;
using IUnitOfWork = WSS.Data.IUnitOfWork;
using Roles = WSS.InternalApplication.Authorization.Roles;
using System.Text;
using System.Text.RegularExpressions;

namespace WSS.InternalApplication.Controllers
{
    [Authorize]
    [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Profile)]
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _wssUnitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _utilityUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ISendEmail _sendEmail;
        private readonly IAuditTransaction _auditTransaction;
        private readonly IActionLinkManager _actionLinkManager;
        //        private readonly IAuditEventWssAccount AuditEventWssAccountRepository;

        private WssIdentityContext _identityContext;
        private WssIdentityUserManager _identityUserManager;

        public WssIdentityContext IdentityContext
        {
            get { return _identityContext ?? (_identityContext = new WssIdentityContext()); }
            private set { _identityContext = value; }
        }
        public WssIdentityUserManager IdentityUserManager
        {
            get
            {
                if (_identityUserManager != null)
                {
                    return _identityUserManager;
                }
                else
                {
                    _identityUserManager = new WssIdentityUserManager(new UserStore<WssIdentityUser>(IdentityContext));

                    // Configure validation logic for usernames
                    _identityUserManager.UserValidator = new UserValidator<WssIdentityUser>(_identityUserManager)
                    {
                        AllowOnlyAlphanumericUserNames = false,
                        RequireUniqueEmail = true
                    };

                    // Configure validation logic for passwords
                    _identityUserManager.PasswordValidator = new PasswordValidator
                    {
                        RequiredLength = 7,
                        RequireNonLetterOrDigit = false,
                        RequireDigit = true,
                        RequireLowercase = false,
                        RequireUppercase = false,
                    };

                    // Configure user lockout defaults
                    _identityUserManager.UserLockoutEnabledByDefault = false;
                    return _identityUserManager;
                }
            }

            private set
            {
                _identityUserManager = value;
            }
        }

        private static readonly ILogger Logger = new Logger(typeof(AccountController));

        public AccountController(IUnitOfWork wssUnitOfWork, UtilityBilling.Data.IUnitOfWork utilityUnitOfWork, IMapper mapper, IAuditTransaction auditTransaction, ISendEmail sendEmail, IActionLinkManager actionLinkManager) : base(sendEmail/*,auditTransaction*/)
        {
            _wssUnitOfWork = wssUnitOfWork;
            _utilityUnitOfWork = utilityUnitOfWork;
            _mapper = mapper;
            _sendEmail = sendEmail;
            _auditTransaction = auditTransaction;
            _actionLinkManager = actionLinkManager;
        }

        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Profile)]
        [HttpGet]
        public ActionResult Index(string id)
        {
            // Session["LinkedUtilityAccountId"] = id;            
            if (Request["TabId"] != null)
            {
                switch (Request["TabId"])
                {
                    case "Profile":
                        ViewBag.ActiveTabId = 1;
                        break;

                    case "Bills":
                        ViewBag.ActiveTabId = 2;
                        break;

                    case "Manage Utility Accounts":
                        ViewBag.ActiveTabId = 3;
                        break;

                    case "Audit Records":
                        ViewBag.ActiveTabId = 4;
                        break;

                    default:
                        ViewBag.ActiveTabId = 1;
                        break;
                }
            }

            else
            {
                ViewBag.ActiveTabId = 1;
            }

            if (Request["from"] == Constants.AddLinkedUtilityAccount)
            {
                ViewBag.ActiveTabId = 3;
            }

            return Index(Convert.ToInt32(id));
        }

        [HttpPost]
        public ActionResult Index(int? id)
        {
            if (Request["from"] != Constants.AddLinkedUtilityAccount)
            {
                Session["LinkedUtilityAccountId"] = id;
            }
            else
            {
                id = (int)Session["LinkedUtilityAccountId"];
            }
            ViewBag.IsMenuHidden = "hidden";

            var selectedUtilityAccount =
                _utilityUnitOfWork.UtilityAccountRepository.FindAll().FirstOrDefault(x => x.UtilityAccountId == id);

            var linkedWssAccount =
                _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                    .FirstOrDefault(x => x.UtilityAccountId == selectedUtilityAccount.UtilityAccountId);
            WssAccount selectedWssAccount;

            if (linkedWssAccount != null)
            {
                var allLinkedAccounts = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                    .Where(m => m.WssAccountId == linkedWssAccount.WssAccountId)
                    .Select(x => new { id = x.UtilityAccountId, UtilityAccountName = x.NickName });
                var allLinkedUtilityAccount = new Dictionary<int, string>();

                foreach (var item in allLinkedAccounts)
                {
                    var ccbNumber = _utilityUnitOfWork.UtilityAccountRepository.Get(item.id).ccb_acct_id;
                    if (!string.IsNullOrEmpty(item.UtilityAccountName))
                        allLinkedUtilityAccount.Add(item.id.Value, ccbNumber + " (" + item.UtilityAccountName + ")");
                    else
                        allLinkedUtilityAccount.Add(item.id.Value, ccbNumber);
                }
                

                ViewBag.AllLinkedAccounts = allLinkedUtilityAccount;
                selectedWssAccount =
                    _wssUnitOfWork.WssAccountRepository.FindAll()
                        .FirstOrDefault(x => x.WSSAccountId == linkedWssAccount.WssAccountId);
            }

            else
            {
                selectedWssAccount = null;
            }

            AccountViewModel model;

            if (selectedWssAccount != null)
            {
                // found an existing wss account
                model = _mapper.Map<WssAccount, AccountViewModel>(selectedWssAccount);
                model = SetProfileTabVisibility(selectedWssAccount, model);
                //Get the status text
                model.StatusDescription = _wssUnitOfWork.WssAccountStatusRepository.FindAll().FirstOrDefault(x => x.WssAccountStatusCode == selectedWssAccount.WssAccountStatusCode)?.WssAccountStatusDesc;
                model.UtilityAccountId = selectedUtilityAccount?.UtilityAccountId ?? 0;
                model.CcbAccountNumber = selectedUtilityAccount?.ccb_acct_id ?? string.Empty;
                model.PrimaryAccountHolderName = selectedUtilityAccount?.PrimaryAccountHolderName ?? string.Empty;
            }
            else
            {
                if (selectedUtilityAccount == null)
                {
                    // didn't find an existing WSS Profile or Utility account
                    model = new AccountViewModel()
                    {
                        UtilityAccountId = id ?? 0,
                        PrimaryEmailAddress = "***NEW CUSTOMER***",
                        PrimaryAccountHolderName = "***NEW CUSTOMER***",
                        StatusId = 1,
                        CcbAccountNumber = string.Empty,
                        ShowUnlockAccount = false,
                        ShowRestoreAccount = false,
                        ShowUnSubscribe = false,
                        ShowResendActivation = false,
                        ResendAttempts = 0,
                        TermsDate = null,
                        AccountBalance = 0.0M,
                        StatusDescription = "Not Registered"
                    };
                }
                else
                {
                    model = new AccountViewModel()
                    {
                        UtilityAccountId = selectedUtilityAccount.UtilityAccountId,
                        PrimaryEmailAddress = "Not Registered",
                        PrimaryAccountHolderName = selectedUtilityAccount.PrimaryAccountHolderName,
                        StatusId = 1,
                        ShowRestoreAccount = false,
                        ShowUnlockAccount = false,
                        ShowUnSubscribe = false,
                        ShowResendActivation = false,
                        ResendAttempts = 0,
                        TermsDate = null,
                        AccountBalance = 0.0M,
                        StatusDescription = "Not Registered",
                        CcbAccountNumber = selectedUtilityAccount.ccb_acct_id ?? string.Empty
                    };
                }
            }
            var rowsPerpage = Session["AuditRecordsPerPage"]?.ToString() ?? "10";
            var currentSort = Session["AuditRecordsSortOrder"]?.ToString() ?? "DateTime";
            var pageNumber = ViewBag.PageNumber ?? 1;
            var currentDir = Session["AuditRecordsSortDirection"]?.ToString() ?? "";
            ViewBag.auditRecords = GetAuditRecords(linkedWssAccount?.WssAccountId, pageNumber, rowsPerpage, currentSort, currentDir);
            ViewBag.additionalEmailAddressRecords = GetAdditionalEmailAddresses(linkedWssAccount?.WssAccountId, 1, "10");
            ViewBag.RowsPerPageSelectList = ViewDisplayHelper.GetRowCountSelectListItems(10);
            ViewBag.Query = linkedWssAccount?.WssAccountId;
            return View(model);
        }

        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Profile)]
        [HttpPost]
        public JsonResult ChangeEmail(string emailAddress, int wssAccountId)
        {
            var wssAccountCount = _wssUnitOfWork.WssAccountRepository.FindAll().Count(x => x.PrimaryEmailAddress == emailAddress);
            var existingIdentityAccount = IdentityUserManager.FindByEmail(emailAddress);
            if (wssAccountCount > 0 || existingIdentityAccount != null)
            {
                Logger.Debug($"Failed to update the E-mail address.  Address already exists in the system.  Address: {emailAddress}");

                if (Response != null) Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { Message = Resources.AccountController_ChangeEmail_Failure_AddressAlreadyExists }, JsonRequestBehavior.AllowGet);
            }

            var wssAccount =
                _wssUnitOfWork.WssAccountRepository.FindAll()
                    .FirstOrDefault(x => x.WSSAccountId.Equals(wssAccountId));

            if (wssAccount == null)
            {
                Logger.Error($"Failed to update the E-mail address.  Failed to retrieve WSS Account for update.  Account ID: {wssAccountId}");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { Message = Resources.AccountController_ChangeEmail_Failure_WssAccountNotFound }, JsonRequestBehavior.AllowGet);
            }

            //CSR's should be able to change the users email before they activate their account
            //The Identity account gets added at activation though so we want to ignore identity if they are just in REG status
            if (wssAccount.WssAccountStatusCode != "REG")
            {
                // Update the Identity user
                var identityUser = IdentityUserManager.FindByEmail(wssAccount.PrimaryEmailAddress);

                // Only try to update the identity information if they have already activated
                // Still need to continue the process if they are in a registered state
                if (identityUser == null)
                {
                    Logger.Error($"Failed to update the E-mail address.  Failed to retrieve Identity User for update.  Username: {wssAccount.PrimaryEmailAddress}");
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { Message = Resources.AccountController_ChangeEmail_Failure_WssAccountNotFound }, JsonRequestBehavior.AllowGet);
                }

                identityUser.UserName = emailAddress;
                identityUser.Email = emailAddress;
                var identityResult = IdentityUserManager.Update(identityUser);

                if (!identityResult.Succeeded)
                {
                    Logger.Error(
                        $"Failed to update the E-mail address.  Could not save the e-mail address to the authentication system.  Errors: {identityResult.Errors}");
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { Message = Resources.AccountController_ChangeEmail_Failure_IdentityUpdateFailed },
                    JsonRequestBehavior.AllowGet);
                }
            }

            // Update the WSS Account
            var oldEmailAddress = wssAccount.PrimaryEmailAddress;
            wssAccount.PrimaryEmailAddress = emailAddress;
            var oldValue = oldEmailAddress;
            var newValue = emailAddress;
            var userName = User?.Identity.Name;
            var fieldName = "email";
            _wssUnitOfWork.Save(User?.Identity.Name ?? "");

            // Save audit record
            var description = string.Format(EventDescriptionHelper.CsrChangeEmailDescrption, oldValue,
                newValue);
            _auditTransaction.AddAuditRecord(fieldName,
                AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.CHNGUID,
                AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                oldValue, newValue, wssAccount.WSSAccountId, userName, description);
            _wssUnitOfWork.Save(User?.Identity.Name ?? "");

            // Send notification e-mail
            try
            {
                EmailService.AddSingleEmail("WSS.ChangeEmail", $"TO!{oldEmailAddress},<OldEmailAddress>!{oldEmailAddress},<NewEmailAddress>!{newValue}");
                EmailService.AddSingleEmail("WSS.ChangeEmail", $"TO!{wssAccount.PrimaryEmailAddress},<OldEmailAddress>!{oldEmailAddress},<NewEmailAddress>!{newValue}");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    $"Exception ocurred sending the notification e-mail.  Old Email Address:{oldEmailAddress},  New Email Address: {wssAccount.PrimaryEmailAddress}", ex);
                AddMessage(UserMessageType.Warning, Resources.AccountController_ChangeEmail_Failure_SendUpdateEmailFailed);
                return Json("", JsonRequestBehavior.AllowGet);
            }

            AddMessage(UserMessageType.Success, $"The action was completed successfully. A notification e-mail will be sent to {oldEmailAddress} and {wssAccount.PrimaryEmailAddress} shortly");
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePassword(int wssAccountId = 0)
        {
            string message;

            try
            {
                var wssAccount = _wssUnitOfWork.WssAccountRepository.Get(wssAccountId);
                var emailAddress = wssAccount?.PrimaryEmailAddress;
                var changePasswordLink = _actionLinkManager.GenerateResetPasswordLink(wssAccountId);
                var keyValuePair = $"TO!{emailAddress},changePasswordLink!{changePasswordLink},<resetPasswordLink>!{changePasswordLink}";

                _sendEmail.AddSingleEmail("WSS.ChangePassword", keyValuePair);

                var oldValue = Convert.ToString(wssAccount?.WSSAccountId);
                var newValue = Convert.ToString(wssAccount?.WSSAccountId);
                var userName = User?.Identity.Name;
                var fieldName = EventType.ResetPasswordEmail;
                var description = EventDescriptionHelper.CsrResetPasswordEmail;
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.RSTPWDEMAIL, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssAccountId, userName, description);
                _wssUnitOfWork.Save("0");

                message =
                    $"The action was completed successfully. An e-mail with Change Password link will be sent to {emailAddress} shortly";
            }
            catch (Exception ex)
            {
                Logger.Error($"An exception ocurred when attempting to send Change Password link.", ex);
                message = "";
            }
            return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ResendActivation(int wssAccountId = 0)
        {
            string message;
            try
            {
                var wssAccount = _wssUnitOfWork.WssAccountRepository.Get(wssAccountId);
                var emailAddress = wssAccount?.PrimaryEmailAddress;
                var activationLink = _actionLinkManager.GenerateActivateAccountLink(wssAccountId);
                var keyValuePair =
                    $"TO!{emailAddress},activationLink!{activationLink},<AccountActivationLink>!{activationLink}";
                _sendEmail.AddSingleEmail("WSS.Activation", keyValuePair);

                var oldValue = Convert.ToString(wssAccount?.WSSAccountId);
                var newValue = string.Empty;
                var wssaccountId = wssAccount?.WSSAccountId;
                var userName = User?.Identity.Name;
                var fieldName = EventType.ResendActivation;
                var description = String.Format(EventDescriptionHelper.CsrResendActivation, emailAddress);
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.RSNDACT, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssaccountId, userName, description);
                _wssUnitOfWork.Save("0");
                message =
                    $"The action was completed successfully. An e-mail with Activation link will be sent to {emailAddress} shortly";
            }
            catch (Exception ex)
            {
                Logger.Error($"An exception ocurred when attempting to resend the activation email.", ex);
                message = "";
            }
            return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnlockAccount(int wssAccountId = 0)
        {
            string message;
            try
            {
                var wssAccount = _wssUnitOfWork.WssAccountRepository.Get(wssAccountId);
                var emailAddress = wssAccount?.PrimaryEmailAddress;
                if (wssAccount != null) wssAccount.WssAccountStatusCode = AccountStatus.ACT.ToString();

                var oldValue = Convert.ToString(wssAccount?.WSSAccountId);
                var newValue = string.Empty;
                var wssaccountId = wssAccount?.WSSAccountId;
                var userName = User?.Identity.Name;
                var fieldName = EventType.AccountUnlocked;
                var description = EventDescriptionHelper.AccountUnlocked;
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.ACCTLOCK, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssaccountId, userName, description);

                _wssUnitOfWork.Save("0");
                EmailService.AddSingleEmail("WSS.ChangeEmail", $"TO!{emailAddress}");
                message =
                    $"The action was completed successfully. Account is Succesfully Unlocked for the User {emailAddress}";
            }
            catch (Exception ex)
            {
                Logger.Error($"An exception ocurred when attempting to send notification email.", ex);
                message = "";
            }
            _wssUnitOfWork.Save("0");

            return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
        }

        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Managetility)]
        public ActionResult _manage(int id)
        {
            Session["ManageWSSAccountId"] = id;
            var records = new List<ManageUtilityAccountListViewModel>();
            var linkedaccounts =
                _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(m => m.WssAccountId == id);
            foreach (var item in linkedaccounts)
            {
                var utilityAccount =
                    _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                        .FirstOrDefault(m => m.UtilityAccountId == item.UtilityAccountId);

                if (utilityAccount == null) continue;
                var manageutilityaccount = new ManageUtilityAccountListViewModel()
                {
                    NickName = item.NickName,
                    AccountNumber = utilityAccount.ccb_acct_id,
                    EditNickname = item.NickName,
                    id = item.LinkedUtilityAccountId,
                    DefaultAccount = (linkedaccounts.Count() <= 1) || (item.DefaultAccount ?? false)
                };
                records.Add(manageutilityaccount);
            }
            return PartialView(records);
        }

        /// <summary>
        /// Delete Linked utility account 
        /// </summary>
        /// <param name="accId">Linked utility account to be deleted</param>
        /// <returns></returns>
        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Managetility)]
        [HttpPost]
        public ActionResult DeleteLinkedUtilityAccount(string accId)
        {
            var isDeleted = false;
            var linkedAccountId = Convert.ToInt32(accId);
            var linkedUtilityAccount = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                    .FirstOrDefault(m => m.LinkedUtilityAccountId == linkedAccountId);
            if (linkedUtilityAccount != null && (linkedUtilityAccount.DefaultAccount == true))
            {
                //Need to display message
            }
            if (linkedUtilityAccount != null)
            {
                var ccbAcctId = _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                   .FirstOrDefault(x => x.UtilityAccountId == linkedUtilityAccount.UtilityAccountId.Value)?
                   .ccb_acct_id;
                var wssaccountId = linkedUtilityAccount.WssAccountId;
                linkedUtilityAccount.WssAccount.IsActive = false;
                _wssUnitOfWork.LinkedUtilityAccountsRepository.Delete(linkedUtilityAccount);

                var oldValue = string.Empty;
                var newValue = Convert.ToString(linkedUtilityAccount.LinkedUtilityAccountId);

                var userName = User?.Identity.Name;
                var fieldName = EventType.UnlinkedAccount;
                var description = string.Format(EventDescriptionHelper.CsrUnlinkedAccount, ccbAcctId);
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.ULINKACCT, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssaccountId, userName, description);
                _wssUnitOfWork.Save("0");
                isDeleted = true;
            }
            return Json(new { IsDeleted = isDeleted, Id = linkedUtilityAccount?.UtilityAccountId }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// To update linked utility account details 
        /// </summary>
        /// <param name="accId">Linked Utility Account Id</param>
        /// <param name="nickName">Name to be updated</param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Managetility)]
        [HttpPost]
        public ActionResult UpdateLinkedUtilityAccount(string accId, string nickName, bool isDefault)
        {
            var isUpdated = false;

            var linkedAccountId = Convert.ToInt32(accId);
            var linkedUtilityAccount = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().FirstOrDefault(m => m.LinkedUtilityAccountId == linkedAccountId);
            if (linkedUtilityAccount != null)
            {
                var ccbAcctId =
                    _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                        .FirstOrDefault(x => x.UtilityAccountId == linkedUtilityAccount.UtilityAccountId.Value)?
                        .ccb_acct_id;

                var objlinkedUtilityAccounts = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(x => x.WssAccountId == linkedUtilityAccount.WssAccountId);
                foreach (var item in objlinkedUtilityAccounts)
                    item.DefaultAccount = (isDefault) ? false : item.DefaultAccount;

                var oldValue = string.IsNullOrEmpty(linkedUtilityAccount.NickName) ? "(Blank)" : linkedUtilityAccount.NickName; linkedUtilityAccount.NickName = nickName;
                linkedUtilityAccount.DefaultAccount = isDefault;

                var newValue = string.IsNullOrEmpty(linkedUtilityAccount.NickName) ? "(Blank)" : linkedUtilityAccount.NickName;
                if (String.Compare(oldValue, newValue, true) != 0)
                {
                    var userName = User?.Identity.Name;
                    var fieldName = EventType.NicknameChanged;
                    var wssaccountId = linkedUtilityAccount.WssAccountId;
                    var description = string.Format(EventDescriptionHelper.Nickname, ccbAcctId, oldValue, newValue);
                    _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.CHNGNN, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                        oldValue, newValue, wssaccountId, userName, description);
                }
                _wssUnitOfWork.Save("0");
                isUpdated = true;
            }
            return Json(new { isUpdated = isUpdated }, JsonRequestBehavior.AllowGet);
        }
        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Bills)]
        [HttpGet]
        public ActionResult _bills(int id, string sortOrder = "Date", string sortDir = "", int? page = null, string noRowsPerPage = "")
        {
            //var currentSort = string.IsNullOrEmpty(sortOrder)
            //   ? "AccountNumber" : Convert.ToString(sortOrder);
            var currentSort = string.IsNullOrEmpty(sortOrder)
                ? (Convert.ToString((Session["BillsSortorder"] ?? "Date")))
                : Convert.ToString(sortOrder);

            ViewBag.Query = id;

            if (Session["BillsSortDirection"] == null)
                Session["BillsSortDirection"] = "";

            if (sortDir.Equals("Change"))
            {
                Session["BillsSortDirection"] = (Session["BillsSortDirection"].ToString().Equals("Desc")) ? "" : "Desc";
            }

            var currentDirection = Session["BillsSortDirection"].ToString();

            var pageSize = string.IsNullOrEmpty(noRowsPerPage)
                ? (Convert.ToInt32((Session["BillPageSize"] ?? 10)))
                : Convert.ToInt32(noRowsPerPage);

            var pageNumber = !page.HasValue
            ? (Convert.ToInt32((Session["BillsListPageNumber"] ?? 1)))
            : Convert.ToInt32(page.Value);


            Session["BillPageSize"] = pageSize;

            ViewBag.RowsPerPageSelectList = Helper.ViewDisplayHelper.GetRowCountSelectListItems(pageSize);

            ViewBag.Date = "fa fa-sort";

            switch (currentSort)
            {
                case "Date":
                    ViewBag.Date = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;

                // Explicitly explaining default case 
                // ReSharper disable once RedundantCaseLabel
                case "DateDesc":
                default:
                    ViewBag.Date = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;
            }

            var query =
               _utilityUnitOfWork.DocumentHeaderRepository.FindAll()
                                 .Where(x => x.UtilityAccountId == id)
                                 .ToList();

            // Test and handle no bills returned properly
            List<DocumentListViewModel> list;
            if (query.Any())
            {
                list = _mapper.Map<List<DocumentHeader>, List<DocumentListViewModel>>(query);
                list = GetDocumenStatusValues(list);
            }
            else
            {
                list = new List<DocumentListViewModel>();
            }

            if ((pageNumber - 1) * pageSize >= list.Count)
            {
                pageNumber = 1;

            }

            Session["BillsListPageNumber"] = pageNumber;

            var sort = currentSort + currentDirection;
            switch (sort)
            {
                case "Date":
                    list = list.OrderBy(m => m.DocumentDate).ToList();
                    break;

                case "DateDesc":
                    list = list.OrderByDescending(m => m.DocumentDate).ToList();
                    break;

                default:
                    list = list.OrderBy(m => m.DocumentDate).ToList();
                    break;
            }
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = noRowsPerPage;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentDir = sortDir;

            ViewBag.BillsQuery = id;
            var finalList = new StaticPagedList<DocumentListViewModel>(list.ToPagedList(pageNumber, pageSize).ToList(), pageNumber, pageSize, list.Count);
            return PartialView(finalList);
        }
        public ActionResult GetDocumentDetailFile(int id)
        {
            var documentDetail =
                _utilityUnitOfWork.DocumentDetailRepository.FindAll().FirstOrDefault(dd => dd.DocumentHeaderId == id);

            if (documentDetail?.DocumentPdf != null && documentDetail.DocumentPdf.Length > 0)
            {
                var memStream = new MemoryStream(documentDetail.DocumentPdf);
                return new FileStreamResult(memStream, "application/pdf");
            }
            else
            {
                AddMessage(UserMessageType.Error,
                    "We're sorry, but the document you selected could not be found on the server.");
                return RedirectToAction("Index", "Search");
            }
        }

        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Audit)]
        public PartialViewResult _audit(int id, int? page, string rowsPerPage, string sortOrder = "", string sortDir = "")
        {
            // Get the PageSize if set, if not default
            var pageSize = string.IsNullOrEmpty(rowsPerPage)
                ? Convert.ToInt32(Session["AuditRecordsPerPage"] ?? 10)
                : Convert.ToInt32(rowsPerPage);

            // Store page size in session
            Session["AuditRecordsPerPage"] = pageSize;

            var currentSort = string.IsNullOrEmpty(sortOrder)
            ? (Convert.ToString((Session["AuditRecordsSortOrder"] ?? "DateTime")))
            : Convert.ToString(sortOrder);

            if (string.Compare(currentSort, (string)Session["AuditRecordsSortOrder"], true, CultureInfo.InvariantCulture) != 0)
            {
                // VB in case that is a new sort order set the default direction
                sortDir = "";
            }
            Session["AuditRecordsSortOrder"] = currentSort;

            if (Session["AuditRecordsSortDirection"] == null)
            { Session["AuditRecordsSortDirection"] = ""; }

            if (sortDir.Equals("Change") && Session["AuditRecordsSortDirection"].ToString().Equals("Desc"))
            {
                Session["AuditRecordsSortDirection"] = "";
            }
            else if (sortDir.Equals("Change") && Session["AuditRecordsSortDirection"].ToString().Equals(""))
            {
                Session["AuditRecordsSortDirection"] = "Desc";
            }
            var currentDirection = Session["AuditRecordsSortDirection"].ToString();

            // Clear the sort icons on screen back to unsorted (it is re-set below)
            ViewBag.EventType = "fa fa-unsorted";
            ViewBag.DateTime = "fa fa-unsorted";
            ViewBag.UserId = "fa fa-unsorted";

            switch (currentSort)
            {
                case "EventType":
                    ViewBag.EventType = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;

                case "DateTime":
                    ViewBag.DateTime = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;

                case "UserId":
                    ViewBag.UserId = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;

                default:
                    ViewBag.DateTime = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;
            }

            var pageNumber = 1;
            if (page.HasValue)
            {
                pageNumber = Convert.ToInt32(page.Value);
            }
            else if (Session["AuditRecordsPageNumber"] != null)
            {
                pageNumber = Convert.ToInt32(Session["AuditRecordsPageNumber"]);
            }

            Session["AuditRecordsPageNumber"] = pageNumber;
            var auditRecords = GetAuditRecords(id, pageNumber, pageSize.ToString(), sortOrder, currentDirection);
            if ((pageNumber - 1) * pageSize >= auditRecords.Count)
            {
                pageNumber = 1;
            }

            ViewBag.RowsPerPageSelectList = ViewDisplayHelper.GetRowCountSelectListItems(pageSize);
            ViewBag.PageNumber = page;
            ViewBag.PageSize = rowsPerPage;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentDir = sortDir;
            return PartialView("_audit", auditRecords);
        }

        private IPagedList<AuditRecordsViewModel> GetAuditRecords(int? wssAccountId, int? page,
            string rowsPerPage, string currentSort = "", string sortDir = "")
        {
            var records = new List<AuditRecordsViewModel>();

            var lookupAuditEventTypeValues = _wssUnitOfWork.AuditEventWssAccountRepository.FindAll()
                .ToDictionary(x => x.AuditEventWssAccountCode, x => x.AuditEventWssAccountDesc);

            if (wssAccountId != null)
            {
                var auditRecords = _auditTransaction.GetAuditRecords(wssAccountId.Value);

                //if (records != null && records.Count > 0)
                foreach (var item in auditRecords)
                {
                    var auditrecord = new AuditRecordsViewModel
                    {
                        id = item.AuditRecordId,
                        DateTime = item.date.ToString("d MMM yyyy H:mm"),
                        DateTimeAsDate = item.date,
                        UserId = item.PerformedBy,
                        Description = item.Description,
                        WssAccountId = wssAccountId.Value,
                        EventType = lookupAuditEventTypeValues.ContainsKey(item.AuditEventWssAccountCode) ? lookupAuditEventTypeValues[item.AuditEventWssAccountCode] ?? string.Empty : string.Empty
                    };
                    records.Add(auditrecord);
                }
            }
            var orderedrecords = records.OrderBy(x => x.id);
            var sort = currentSort + sortDir; // currentDirection;
            switch (sort)
            {
                case "EventType":
                    orderedrecords = orderedrecords.OrderBy(x => x.EventType);
                    break;

                case "EventTypeDesc":
                    orderedrecords = orderedrecords.OrderByDescending(x => x.EventType);
                    break;

                case "UserId":
                    orderedrecords = orderedrecords.OrderBy(x => x.UserId);
                    break;

                case "UserIdDesc":
                    orderedrecords = orderedrecords.OrderByDescending(x => x.UserId);
                    break;

                case "DateTime":
                    // VB: please, leave this case and the next one the way they are
                    orderedrecords = orderedrecords.OrderByDescending(x => x.DateTimeAsDate);
                    break;

                // Explicitly stating default case
                // ReSharper disable once RedundantCaseLabel
                case "DateTimeDesc":
                default:
                    orderedrecords = orderedrecords.OrderBy(x => x.DateTimeAsDate);
                    break;
            }
            var expectedNofRecords = Convert.ToInt32(rowsPerPage) * (page - 1);
            if (expectedNofRecords >= orderedrecords.Count())
            {
                page = 1;
            }
            Session["AuditRecordsPageNumber"] = page;
            //DF - pass page 1 by default, unless otherwise specified
            return new PagedList<AuditRecordsViewModel>(orderedrecords, page ?? 1, Convert.ToInt32(rowsPerPage));
        }

        /// <summary>
        /// Based on the status of the account this method sets the visibility of the various tabs
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public AccountViewModel SetProfileTabVisibility(WssAccount entity, AccountViewModel model)
        {
            // Set Defaults
            model.ShowRegisterAccount = false;
            model.ShowChangeEmail = false;
            model.ShowResendActivation = false;
            model.ShowResetPassword = false;
            model.ShowUnlockAccount = false;
            model.ShowRestoreAccount = false;
            model.ShowUnSubscribe = false;
            model.ShowLockAccount = false;
            model.ShowAdditionalEmail = false;

            switch (entity.WssAccountStatusCode ?? "")
            {
                case "UNSUB": //Not Registered
                default:
                    model.ShowRegisterAccount = true;
                    break;

                case "REG": // Registered
                    model.ShowResendActivation = true;
                    //model.ShowUnSubscribe = true;
                    model.ShowChangeEmail = true;
                    break;

                case "ACT": //Active
                    model.ShowChangeEmail = true;
                    model.ShowResetPassword = true;
                    model.ShowUnSubscribe = true;
                    model.ShowAdditionalEmail = true;
                    break;

                case "LCKD": //Locked
                    model.ShowResetPassword = true;
                    model.ShowResendActivation = true;
                    break;
            }
            return model;
        }

        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Managetility)]
        [HttpPost]
        public JsonResult Unsubscribe(int? utilityAccountId, string reason, string othersText,
         string unsubscribeComments)
        {
            if
                (!utilityAccountId.HasValue)
            {
                return Json(new { isDeleted = false }, JsonRequestBehavior.AllowGet);
            }

            var objWssAccount =
                _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                    .FirstOrDefault(x => x.UtilityAccountId == utilityAccountId);
            var selectedWssAccount = objWssAccount?.WssAccount;

            if (selectedWssAccount == null)
            {
                return Json(new { isDeleted = false }, JsonRequestBehavior.AllowGet);
            }

            // Delete the login account
            var identityUser = IdentityUserManager.FindByEmail(selectedWssAccount.PrimaryEmailAddress);
            if (identityUser == null)
            {
                Logger.Error($"Could not find Identity User to delete during Unsubscription process.  Username: {selectedWssAccount.PrimaryEmailAddress}");
                return Json(new { isDeleted = false }, JsonRequestBehavior.AllowGet);
            }
            var identityResult = IdentityUserManager.Delete(identityUser);

            if (!identityResult.Succeeded)
            {
                Logger.Error($"Could not delete Identity User during Unsubscription process.  Username: {selectedWssAccount.PrimaryEmailAddress}, Identity User ID: {identityUser.Id}");
                return Json(new { isDeleted = false }, JsonRequestBehavior.AllowGet);
            }

            // Delete/unsubscribe the linked utility accounts
            var objlinkedUtilityAccounts =
                _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                    .Where(x => x.WssAccountId == objWssAccount.WssAccountId);

            foreach (var item in objlinkedUtilityAccounts)
            {
                _wssUnitOfWork.LinkedUtilityAccountsRepository.Delete(item);
            }

            // Soft delete/unsubscribe the WSS Account
            selectedWssAccount.IsActive = false;

            // Update SubscriptionTransaction Table
            var objsubscriptionTransaction = new SubscriptionTransaction()
            {
                SubscriptionTransactionDate = DateTime.Now,

                ccb_acct_id =
                    _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                        .FirstOrDefault(x => x.UtilityAccountId == utilityAccountId.Value)?
                        .ccb_acct_id,
                //DF - Temporarily hardcoding these until the full refactor to use the DomainLookup table

                //TODO Update these to pull from DomainLookup
                SubscriptionTransactionTypeCode = "UNSUB",
                SubscriptionTransactionStatusId = 1
            };

            _wssUnitOfWork.SubscriptionTransactionRepository.Insert(objsubscriptionTransaction);
            _wssUnitOfWork.Save(User.Identity.Name);
            EmailService.AddSingleEmail("WSS.Unsubscribe", $"TO!{selectedWssAccount.PrimaryEmailAddress}");

            // Create and save audit record
            var oldValue = Convert.ToString(selectedWssAccount.WSSAccountId);
            var newValue = string.Empty;
            var userName = User?.Identity.Name;
            var fieldName = EventType.Unsubscribe;
            var wssaccountId = selectedWssAccount.WSSAccountId;
            var description = string.Format(EventDescriptionHelper.Unsubscribe, reason);
            _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.UNSUB, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                oldValue, newValue, wssaccountId, userName, description);
            _wssUnitOfWork.Save(User.Identity.Name);

            return Json(new { isDeleted = true }, JsonRequestBehavior.AllowGet);
        }

        [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerDetails_Managetility)]
        [HttpPost]
        public ActionResult AdditionalEmail(string emailAddress, int wssAccountId = 0)
        {
            var message = string.Empty;
            var additionalemailAddress = emailAddress;
            var wssAccount =
                _wssUnitOfWork.WssAccountRepository.FindAll()
                    .FirstOrDefault(x => x.WSSAccountId.Equals(wssAccountId));

            var count = _wssUnitOfWork.WssAccountRepository.FindAll().Count(x => x.PrimaryEmailAddress == emailAddress && x.WSSAccountId == wssAccountId);
            var userAdditionalEmails = _wssUnitOfWork.AdditionalEmailAddressRepository.FindAll().Where(m => m.WssAccountId == wssAccountId);
            var additionalEmail = userAdditionalEmails.FirstOrDefault(m => m.EmailAddress == additionalemailAddress && m.WssAccountId == wssAccountId);
            if (count > 0 || additionalEmail != null)
            {
                message = Resources.AccountController_ChangeEmail_Failure_AddressAlreadyExists;
            }
            else if (userAdditionalEmails.Count() >= 5)
            {
                message = "Email count exceeded";
            }
            else
            {
                additionalEmail = new AdditionalEmailAddress()
                {
                    EmailAddress = emailAddress,
                    WssAccount = wssAccount
                };

                if (wssAccount != null)
                {
                    _wssUnitOfWork.AdditionalEmailAddressRepository.Insert(additionalEmail);

                    var oldEmailAddress = wssAccount.PrimaryEmailAddress;
                    var oldValue = oldEmailAddress;
                    var newValue = emailAddress;
                    var userName = User?.Identity.Name;
                    var fieldName = "AdditionalEmail";

                    var description = string.Format(EventDescriptionHelper.AddedSecondaryEmail, newValue);
                    _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.ADDEMAIL, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT, oldValue, newValue, wssAccount.WSSAccountId, userName, description);
                    _wssUnitOfWork.Save("0");
                    var additionalEmailAddressData = GetAdditionalEmailAddresses(wssAccountId, 1, "10");

                    var linkedUtilityAccountId = Convert.ToInt32(Session["LinkedUtilityAccountId"].ToString());
                    var linkedUtilityAccount = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().FirstOrDefault(m => m.UtilityAccountId == linkedUtilityAccountId);
                    var nickname = linkedUtilityAccount?.NickName;
                    var ccbAcctId = _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                        .FirstOrDefault(x => x.UtilityAccountId == linkedUtilityAccountId)?
                        .ccb_acct_id;

                    var reg = new Regex(@"[0-9]");
                    var ccbAccount = reg.Replace(ccbAcctId ?? string.Empty, "X", 4, 3);
                    ccbAccount = ccbAccount + 'X';

                    var sb = new StringBuilder();
                    _sendEmail.AddSingleEmail("WSS.AdditionalEmailAddedPrimaryAccountHolder", $"TO!{wssAccount.PrimaryEmailAddress},<AdditionalEmailAdded>!{emailAddress}");

                    try
                    {
                        var secondaryEmailnsubscribeUrl = string.Empty;
                        _sendEmail.GetSecondaryEmailUnsubscribeUrl(emailAddress, out secondaryEmailnsubscribeUrl);
                        _sendEmail.AddSingleEmail("WSS.AdditionalEmailAdded", $"TO!{emailAddress},<CCBAccount>!{ccbAccount},<-NickName>!{nickname},<SecondaryEmailUnsubscribeUrl>!{secondaryEmailnsubscribeUrl}");
                    }
                    catch (Exception)
                    {
                        if (!sb.ToString().Contains("Failed to send message"))
                        {
                            sb.Append("Failed to send message to the following reciepent");
                            sb.AppendLine();
                        }
                        else
                        {
                            sb.Append(emailAddress);
                        }
                    }

                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        message = sb.ToString();
                    }

                    return PartialView("_additionalEmailAddresses", additionalEmailAddressData);
                }
                else
                {
                    message = "wssAccount doesnot exist";
                }
            }
            return Json(new { Message = message, additionalEmail?.AdditionalEmailAddressId, additionalEmail?.WssAccountId, additionalEmail?.EmailAddress }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAdditionalEmailAddressesPartialViewResult(int wssAccountId, string page, string rowsPerPage)
        {
            var pageSize = Convert.ToInt32(rowsPerPage);
            var pageNumber = Convert.ToInt32(page);
            var additionalEmailAddressList = GetAdditionalEmailAddresses(wssAccountId, pageNumber, rowsPerPage);
            if ((pageNumber - 1) * pageSize >= additionalEmailAddressList.Count)
            {
                pageNumber = 1;
            }
            ViewBag.RowsPerPageSelectList =
                ViewDisplayHelper.GetRowCountSelectListItems(Convert.ToInt32(rowsPerPage));
            ViewBag.PageNumber = page;
            ViewBag.PageSize = rowsPerPage;
            return PartialView("_additionalEmailAddresses", additionalEmailAddressList);
        }

        public IPagedList<AdditionalEmailAddressViewModel> GetAdditionalEmailAddresses(int? wssAccountId, int? page,
            string rowsPerPage)
        {
            var lstAdditionalEmailAddress = _wssUnitOfWork.AdditionalEmailAddressRepository.FindAll().Where(x => x.WssAccountId == wssAccountId);
            var records = new List<AdditionalEmailAddressViewModel>();
            foreach (var item in lstAdditionalEmailAddress)
            {
                var addressviewmodel = new AdditionalEmailAddressViewModel()
                {
                    AdditionalEmailAddressId = item.AdditionalEmailAddressId,
                    EmailAddress = item.EmailAddress,
                    WssAccountId = wssAccountId
                };
                records.Add(addressviewmodel);
            }
            return new PagedList<AdditionalEmailAddressViewModel>(records, page ?? 1,
               Convert.ToInt32(rowsPerPage));
        }

        public ActionResult DeleteAdditionalEmailAddresses(int additionalEmailAddressId = 0, int wssAccountId = 0)
        {
            var isDeleted = false;
            var userAdditionalEmails = _wssUnitOfWork.AdditionalEmailAddressRepository.FindAll().Where(m => m.WssAccountId == wssAccountId);
            var additionalemailaddress = _wssUnitOfWork.AdditionalEmailAddressRepository.Get(additionalEmailAddressId);
            var message = string.Empty;
            if (additionalemailaddress != null)
            {
                _wssUnitOfWork.AdditionalEmailAddressRepository.Delete(additionalemailaddress);

                var oldEmailAddress = additionalemailaddress.EmailAddress;
                var oldValue = oldEmailAddress;
                var newValue = additionalemailaddress.EmailAddress;
                var userName = User?.Identity.Name;
                var fieldName = "RemoveAdditionalEmail";

                var description = string.Format(EventDescriptionHelper.RemoveSecondaryEmail, oldValue);
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.REMEMAIL, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT, oldValue, newValue, wssAccountId, userName, description);

                var wssAccount =
             _wssUnitOfWork.WssAccountRepository.FindAll()
                 .FirstOrDefault(x => x.WSSAccountId.Equals(wssAccountId));
                var linkedUtilityAccountId = Convert.ToInt32(Session["LinkedUtilityAccountId"].ToString());
                var linkedUtilityAccount = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().FirstOrDefault(m => m.UtilityAccountId == linkedUtilityAccountId);
                var nickname = linkedUtilityAccount?.NickName;
                var ccbAcctId = _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                    .FirstOrDefault(x => x.UtilityAccountId == linkedUtilityAccountId)?
                    .ccb_acct_id;

                var reg = new Regex(@"[0-9]");
                var ccbAccount = reg.Replace(ccbAcctId ?? string.Empty, "X", 4, 3);
                ccbAccount = ccbAccount + 'X';

                _sendEmail.AddSingleEmail("WSS.AdditionalEmailRemovedPrimaryAccountHolder", $"TO!{wssAccount?.PrimaryEmailAddress},<AdditionalEmailRemoved>!{newValue}");
                var sb = new StringBuilder();

                try
                {
                    _sendEmail.AddSingleEmail("WSS.AdditionalEmailRemoved", $"TO!{oldValue},<CCBAccount>!{ccbAccount},<-NickName>!{nickname}");
                }
                catch (Exception)
                {
                    if (!sb.ToString().Contains("Failed to send message"))
                    {
                        sb.Append("Failed to send mail to the following reciepent");
                        sb.Append(oldValue);
                        sb.AppendLine();
                    }
                    else
                    {
                        sb.Append(oldValue);
                    }
                }

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    message = sb.ToString();
                }
                _wssUnitOfWork.Save("0");
                isDeleted = true;
            }
            return Json(new { IsDeleted = isDeleted, Message = message, additionalEmailAddressID = additionalEmailAddressId }, JsonRequestBehavior.AllowGet);
        }

        private List<DocumentListViewModel> GetDocumenStatusValues(List<DocumentListViewModel> list)
        {
            //document type comes from view
            var lookupValuesForDocumentStatus =
                _utilityUnitOfWork.DocumentStatusRepository.FindAll()
                .ToDictionary(x => x.DocumentStatusCode, x => x.DocumentStatusValue);

            if (lookupValuesForDocumentStatus.Any())
            {
                foreach (var item in list)
                {
                    item.Status = lookupValuesForDocumentStatus[item.DocumnetStatuscode];
                }
            }
            return list;
        }
    }
}