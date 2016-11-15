using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Ninject;
using PagedList;
using UtilityBilling.Data;
using WSS.AuditTransaction.Interfaces;
using WSS.CustomerApplication.CustomAttributes;
using WSS.CustomerApplication.Helper;
using WSS.CustomerApplication.Models;
using WSS.Data;
using WSS.Email.Service;
using WSS.Identity;
using WSS.Logging.Service;
using WSS.Common.Utilities;
using WSS.CustomerApplication.Properties;
using WSS.Data.Migrations;
using static System.Net.AuthenticationManager;
using EventType = WSS.CustomerApplication.Helper.EventType;
using IUnitOfWork = WSS.Data.IUnitOfWork;

namespace WSS.CustomerApplication.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private static readonly ILogger Logger = new Logger(typeof(AccountActivationController));

        private readonly IUnitOfWork _wssUnitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _utilityUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditTransaction _auditTransaction;

        private IWssIdentityUserManager _identityUserManager;
        private IAuthenticationManager _wssAuthenticationManager;


        public IAuthenticationManager WssAuthenticationManager
        {
            get
            {
                return _wssAuthenticationManager ?? HttpContext.GetOwinContext().Authentication;
            }
            private set
            {
                _wssAuthenticationManager = value;
            }
        }

        public IWssIdentityUserManager IdentityUserManager
        {
            get
            {
                return _identityUserManager ?? HttpContext.GetOwinContext().GetUserManager<WssIdentityUserManager>();
            }
            set
            {
                _identityUserManager = value;
            }
        }

        //private IAuthenticationManager WssAuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUnitOfWork wssUnitOfWork, UtilityBilling.Data.IUnitOfWork utilityUnitOfWork,
            IMapper mapper, IAuditTransaction auditTransaction, ISendEmail sendEmail) : base(sendEmail)
        {
            _wssUnitOfWork = wssUnitOfWork;
            _utilityUnitOfWork = utilityUnitOfWork;
            _mapper = mapper;
            _auditTransaction = auditTransaction;
        }

        [NoCache]
        public ActionResult Index()
        {
            // Set the current tab
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
                    ViewBag.ActiveTabId = 2;
                    break;
            }

            ViewBag.BannerFileName = GetBanner();

            var currentIdentityUser = IdentityUserManager.FindById(User.Identity.GetUserId());

            // get current user WebSelfServe account - ALWAYS ONLY ONE per user
            var userWssAccount =
                _wssUnitOfWork.WssAccountRepository.FindAll()
                    .FirstOrDefault(x => x.AuthenticationId == currentIdentityUser.Id);

            Session["UserWSSAccountId"] = userWssAccount?.WSSAccountId;

            // Find all Utility Acoounts linked to the user WSS account - 
            // can be many utility accounts per one WSS account 
            var allLinkedUtilityAccounts = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                .Where(m => m.WssAccountId == userWssAccount.WSSAccountId);

            // check if theer are any utility accounts linked to the wss account
            if (allLinkedUtilityAccounts.Any())
            { }
            else
            {
                // TODO: VB shouldn't we here redirect users to the add utility account page?
                // but before this was here, so a left it
                return View("Error");
            }

            // find a default utility account for the WSS account
            var defaultLinkedUtilityAccount = allLinkedUtilityAccounts.FirstOrDefault(x => x.WssAccountId == userWssAccount.WSSAccountId && x.DefaultAccount.HasValue && x.DefaultAccount.Value);

            if (defaultLinkedUtilityAccount == null && allLinkedUtilityAccounts.Any())
            {
                // if for whatever reason a default account is not set, just grab the first one
                defaultLinkedUtilityAccount = allLinkedUtilityAccounts.FirstOrDefault();
            }

            // Set up the data for the WSS Bills page
            Session["SelectedUtilityAccountId"] = defaultLinkedUtilityAccount.UtilityAccountId;

            // bild a view model for bills
            var model = _mapper.Map<WssAccount, AccountViewModel>(userWssAccount);
            model = SetProfileTabVisibility(userWssAccount, model);

            //Get the status text
            model.DefaultUtilityAccountId = defaultLinkedUtilityAccount.UtilityAccountId.HasValue ? defaultLinkedUtilityAccount.UtilityAccountId.Value : 0;

            // Set up the initial parameters for paging the table
            ViewBag.PageNumber = 1;
            ViewBag.PageSize = 10;
            ViewBag.CurrentSort = "Date";
            ViewBag.CurrentDir = "";
            ViewBag.additionalEmailAddressRecords = GetAdditionalEmailAddresses(defaultLinkedUtilityAccount.WssAccountId, 1, "10");
            ViewBag.RowsPerPageSelectList = ViewDisplayHelper.GetRowCountSelectListItems(10);
            ViewBag.Questions = GetQuestions();
            ViewBag.Query = Session["UserWSSAccountId"];
            ViewBag.EmailErrorMessage = "";

            return View(model);
        }

        private Dictionary<int, string> GetAccountsDropdownData()
        {
            // Find all Utility Acoounts linked to the user WSS account - 
            // can be many utility accounts per one WSS account 
            var wssAccountId = (int)Session["UserWSSAccountId"];
            var allLinkedUtilityAccounts = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                .Where(m => m.WssAccountId == wssAccountId);

            var allLinkedAccountsDictionary = allLinkedUtilityAccounts.ToDictionary(x => x.UtilityAccountId, x => x.NickName);

            var tempUtilityAccountIdsArray = allLinkedUtilityAccounts.Select(m => m.UtilityAccountId).ToArray();
            var allUtilityAccountsLinkedToTheUser =
                _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                .Where(x => tempUtilityAccountIdsArray.Contains(x.UtilityAccountId))
                .ToDictionary(x => x.UtilityAccountId, x => x.ccb_acct_id);

            var allLinkedUtilityAccountsDropdown = new Dictionary<int, string>();

            foreach (KeyValuePair<int, string> pair in allUtilityAccountsLinkedToTheUser)
            {
                var ccbNumber = pair.Value;

                var nickname = string.Empty;

                if (allLinkedAccountsDictionary.TryGetValue(pair.Key, out nickname))
                {
                    if (!string.IsNullOrEmpty(nickname))
                    {
                        ccbNumber += " (" + nickname + ") ";
                    }
                }

                allLinkedUtilityAccountsDropdown.Add(pair.Key, ccbNumber);
            }

            return allLinkedUtilityAccountsDropdown;
        }

        [HttpGet]
        public ActionResult _bills(int id, string sortOrder = "Date", string sortDir = "", int? page = null, string noRowsPerPage = "")
        {
            // Important ! 
            var currentUtilityAccountId = id;

            ViewBag.NotAllowed = string.Empty;
            var allowedAccounts = GetUtilityAccountsLoggedUserIsAllowedToManage();

            if (!allowedAccounts.Any(x => x == id))
            {
                ViewBag.NotAllowed = "Sorry, you can not manage the account";
                var emptyList = new List<DocumentListViewModel>();
                var emptyPagedList = new StaticPagedList<DocumentListViewModel>(emptyList.ToPagedList(1, 10).ToList(), 1, 10, emptyList.Count);
                return PartialView(emptyPagedList);
            }

            var currentSort = string.IsNullOrEmpty(sortOrder)
                ? (Convert.ToString((Session["BillsSortorder"] ?? "Date")))
                : Convert.ToString(sortOrder);

            ViewBag.Query = Session["UserWSSAccountId"];

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
                                 .Where(x => x.UtilityAccountId == currentUtilityAccountId)
                                 .ToList();

            // Test and handle no bills returned properly
            List<DocumentListViewModel> list;
            if (query.Any())
            {
                list = _mapper.Map<List<DocumentHeader>, List<DocumentListViewModel>>(query);
            }
            else
            {
                list = new List<DocumentListViewModel>();
            }

            ViewBag.BillsDropDownList = GetAccountsDropdownData();

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

            if (((pageNumber - 1) * pageSize) >= list.Count)
            {
                pageNumber = 1;
            }

            Session["BillsListPageNumber"] = pageNumber;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = noRowsPerPage;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentDir = sortDir;

            ViewBag.BillsQuery = currentUtilityAccountId;

            var finalList = new StaticPagedList<DocumentListViewModel>(list.ToPagedList(pageNumber, pageSize).ToList(), pageNumber, pageSize, list.Count);
            return PartialView(finalList);
        }


        private List<DocumentListViewModel> GetDocumentTypeValues(List<DocumentListViewModel> list)
        {
            //document type comes from view
            var lookupValuesForDocumentType =
                _utilityUnitOfWork.DocumentTypeRepository.FindAll()
                .ToDictionary(x => x.DocumentTypeCode, x => x.DocumentTypeValue);

            if (lookupValuesForDocumentType.Any())
            {
                foreach (var item in list)
                {
                    item.DocumentType = lookupValuesForDocumentType[item.DocumentType];
                }
            }
            return list;
        }


        [Authorize]
        [NoCache]
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
                    Resources.AccountController_GetDocumentDetailFile_We_re_sorry__but_the_document_you_selected_could_not_be_found_on_the_server_);
                return RedirectToAction("Index", "Account");
            }
        }



        [HttpPost]
        public ActionResult Manage()
        {
            var records = PrepareManageUtilityAccountListViewModels(GetLoggedUserWssAccount());
            return PartialView("_manage", records);
        }

        /// <summary>
        /// Gets list of linked utility account by wss account id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<ManageUtilityAccountListViewModel> PrepareManageUtilityAccountListViewModels(int wssAccountId)
        {
            var records = new List<ManageUtilityAccountListViewModel>();
            var linkedUtilityAccounts =
                _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(m => m.WssAccountId == wssAccountId);
            foreach (var item in linkedUtilityAccounts)
            {
                var linkedUtilityAccount = new ManageUtilityAccountListViewModel()
                {
                    NickName = item.NickName,
                    UtilityAccountNumber =
                        _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                            .FirstOrDefault(m => m.UtilityAccountId == item.UtilityAccountId)?
                            .ccb_acct_id,
                    EditNickname = item.NickName,
                    UtilityAccountId = item.UtilityAccountId.HasValue ? item.UtilityAccountId.Value : 0,
                    DefaultAccount = (linkedUtilityAccounts.Count() <= 1) || (item.DefaultAccount ?? false)
                };
                records.Add(linkedUtilityAccount);
            }
            return records;
        }

        public ActionResult DeleteLinkedUtilityAccount(int utilityAccId)
        {
            var wssaccountId = 0;
            var isDeleted = false;
            var utilityAccountId = Convert.ToInt32(utilityAccId);

            var utilityAccount = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                .FirstOrDefault(m => m.UtilityAccountId == utilityAccountId);
            if (utilityAccount != null && (utilityAccount.DefaultAccount == true))
            {
                //Need to display message
                return Json(new { IsDeleted = false, message = Resources.AccountController_DeleteLinkedUtilityAccount_You_can_not_delete_a_default_account },
                    JsonRequestBehavior.AllowGet);
            }
            else if (utilityAccount != null)
            {
                // get accounts, linked to the logged user
                var loggedUserCanManageAccounts = GetUtilityAccountsLoggedUserIsAllowedToManage();

                if (!loggedUserCanManageAccounts.Any(x => x.Equals(utilityAccount.UtilityAccountId)))
                {
                    return Json(new { IsDeleted = false, message = Resources.AccountController_DeleteLinkedUtilityAccount_You_have_no_rights_to_change_the_account },
                    JsonRequestBehavior.AllowGet);
                }

                // here we got only if the user can manage the account

                var ccbAcctId = _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                    .FirstOrDefault(x => x.UtilityAccountId == utilityAccount.UtilityAccountId.Value)?
                    .ccb_acct_id;
                wssaccountId = utilityAccount.WssAccountId.Value;

                utilityAccount.WssAccount.IsActive = false;
                _wssUnitOfWork.LinkedUtilityAccountsRepository.Delete(utilityAccount);

                var oldValue = string.Empty;
                var newValue = Convert.ToString(utilityAccount.LinkedUtilityAccountId);
                var userName = User?.Identity.Name;
                var fieldName = EventType.UnlinkedAccount;
                var description = string.Format(EventDescriptionHelper.UnlinkedAccount, ccbAcctId);

                _auditTransaction.AddAuditRecord(fieldName,
                    AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.ULINKACCT,
                    AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssaccountId, userName, description);

                var reg = new Regex(@"[0-9]");
                var ccbAccount = reg.Replace(ccbAcctId ?? string.Empty, "X", 4, 3);
                ccbAccount = ccbAccount + 'X';
                sendEmail.AddSingleEmail("WSS.LinkedUtilityAccountRemoved",
                    $"TO!{userName},<CCBAccount>!{ccbAccount},<-NickName>!{utilityAccount.NickName}");

                _wssUnitOfWork.Save(User.Identity.Name);
                // Send notification e-mail
                isDeleted = true;
            }
            var records = PrepareManageUtilityAccountListViewModels(wssaccountId);
            return PartialView("_manage", records);
        }


        private int[] GetUtilityAccountsLoggedUserIsAllowedToManage()
        {
            var currentIdentityUser = IdentityUserManager.FindById(User.Identity.GetUserId());

            var selectedWssAccount =
                _wssUnitOfWork.WssAccountRepository.FindAll()
                    .FirstOrDefault(x => x.AuthenticationId == currentIdentityUser.Id);

            var loggedUserCanManageAccounts = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(x => x.WssAccountId == selectedWssAccount.WSSAccountId);
            return loggedUserCanManageAccounts.Select(x => x.UtilityAccountId.Value).ToArray();
        }

        private int GetLoggedUserWssAccount()
        {
            var currentIdentityUser = IdentityUserManager.FindById(User.Identity.GetUserId());
            var temp = _wssUnitOfWork.WssAccountRepository.FindAll()
                    .FirstOrDefault(x => x.AuthenticationId == currentIdentityUser.Id);
            return temp == null ? 0 : temp.WSSAccountId;
        }



        /// <summary>
        /// <param name="accId">Linked Utility Account Id</param>
        /// <param name="nickName">Name to be updated</param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        /// </summary>
        [HttpPost]
        public ActionResult UpdateLinkedUtilityAccount(string accId, string nickName, bool isDefault)
        {
            var utilityAccountId = Convert.ToInt32(accId);

            var utilityAccount = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().FirstOrDefault(m => m.UtilityAccountId == utilityAccountId);
            if (utilityAccount == null)
            { return Json(new { isUpdated = false, result = string.Empty }, JsonRequestBehavior.AllowGet); }

            // get accounts, linked to the logged user
            var loggedUserCanManageAccounts = GetUtilityAccountsLoggedUserIsAllowedToManage();

            var objLinkedUtilityAccounts = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(x => x.WssAccountId == utilityAccount.WssAccountId);

            // check if the default was changed for the account
            if (utilityAccount.DefaultAccount != null && utilityAccount.DefaultAccount.Value != isDefault)
            {
                foreach (var item in objLinkedUtilityAccounts)
                {
                    // clear default setting from all linked accounts
                    if (loggedUserCanManageAccounts.Any(x => x.Equals(item.UtilityAccountId)))
                    { item.DefaultAccount = isDefault ? false : item.DefaultAccount; }
                }

                utilityAccount.DefaultAccount = isDefault;
            }

            var oldValue = string.IsNullOrEmpty(utilityAccount.NickName) ? "(Blank)" : utilityAccount.NickName;
            utilityAccount.NickName = nickName.Trim();

            var newValue = string.IsNullOrEmpty(utilityAccount.NickName) ? "(Blank)" : utilityAccount.NickName;

            if (String.Compare(oldValue, newValue, true) != 0)
            {

                var ccbAcctId = _utilityUnitOfWork.UtilityAccountRepository.FindAll().FirstOrDefault(x => x.UtilityAccountId == utilityAccount.UtilityAccountId.Value)?.ccb_acct_id;

                var userName = User?.Identity.Name;
                var fieldName = EventType.NicknameChanged;
                var wssaccountId = utilityAccount.WssAccountId;
                var description = string.Format(EventDescriptionHelper.Nickname, ccbAcctId, oldValue, newValue);
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.CHNGNN, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssaccountId, userName, description);
            }

            _wssUnitOfWork.Save("0");
            var records = PrepareManageUtilityAccountListViewModels(utilityAccount.WssAccountId.Value);
            return PartialView("_manage", records);

        }

        [HttpPost]
        public JsonResult ChangeEmail(string changeEmail1, int wssAccountId)
        {
            var message = string.Empty;

            if (wssAccountId != GetLoggedUserWssAccount())
            {
                return Json(new { Message = "Sorry, you can not manage the account" }, JsonRequestBehavior.AllowGet);
            }

            if (!ValidateEmail(changeEmail1))
            {
                return Json(new { Message = Resources.AccountController_AdditionalEmail_Sorry__the_email_address_is_not_valid }, JsonRequestBehavior.AllowGet);
            }
            var count = _wssUnitOfWork.WssAccountRepository.FindAll().Count(x => x.PrimaryEmailAddress == changeEmail1);
            if (count > 0)
            {
                return Json(new { Message = Resources.AccountController_ChangeEmail_Email_Already_Exists }, JsonRequestBehavior.AllowGet);
            }


                try
                {
                    var wssAccount =
                        _wssUnitOfWork.WssAccountRepository.FindAll()
                            .FirstOrDefault(x => x.WSSAccountId.Equals(wssAccountId));

                    if (wssAccount == null)
                    {
                        Logger.Error(
                            $"Failed to retrieve WSS Account when changing e-mail address.  Account Number: {wssAccountId}");
                        message = Resources.AccountController_ChangeEmail_Failed_to_retrieve_WSS_Account;
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
                    }

                    var oldEmailAddress = wssAccount.PrimaryEmailAddress;
                    wssAccount.PrimaryEmailAddress = changeEmail1;

                    // Get the Identity user
                    var user = IdentityUserManager.FindByEmail(oldEmailAddress);
                    if (null == user)
                    {
                        message =
                            $"Failed to retrieve authentication information for user: {oldEmailAddress}";
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
                    }

                    // Update the Identity user
                    user.Email = changeEmail1;
                    user.UserName = changeEmail1;

                    var result = IdentityUserManager.Update(user);
                    if (!result.Succeeded)
                    {
                        message = result.Errors.Aggregate(message, (current, error) => current + (error + " "));
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
                    }

                    // Update the WssAccount
                    var oldValue = oldEmailAddress;
                    var newValue = changeEmail1;
                    var userName = User?.Identity.Name;
                    const string fieldName = "Email changed";

                    var description = string.Format(EventDescriptionHelper.ChangeEmailDescrption, oldValue, newValue);
                    _auditTransaction.AddAuditRecord(fieldName,
                        AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.CHNGUID,
                        AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                        oldValue, newValue, wssAccount.WSSAccountId, userName, description);

                    _wssUnitOfWork.Save("0");

                    // Send notification e-mail
                    try
                    {
                        var secondaryEmailunsubscribeUrl = string.Empty;
                        sendEmail.GetSecondaryEmailUnsubscribeUrl(wssAccount?.PrimaryEmailAddress, out secondaryEmailunsubscribeUrl);
                       sendEmail.AddSingleEmail("WSS.ChangeEmail",
                            $"TO!{oldEmailAddress},,<OldEmailAddress>!{oldEmailAddress},<NewEmailAddress>!{newValue},SecondaryEmailUnsubscribeUrl!{secondaryEmailunsubscribeUrl}");
                        sendEmail.AddSingleEmail("WSS.ChangeEmail", $"TO!{wssAccount.PrimaryEmailAddress},<OldEmailAddress>!{oldEmailAddress},<NewEmailAddress>!{newValue},SecondaryEmailUnsubscribeUrl!{secondaryEmailunsubscribeUrl}");
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn($"An exception ocurred when attempting to send Change Email notification.  Original e-mail address: {oldEmailAddress}, New e-mail address: {wssAccount.PrimaryEmailAddress}", ex);
                    }

                // user will be redirected and the message will be shown on Login page 
                TempData["SuccessfulEmailChange"] = String.Format(Resources.AccountController_ChangeEmail_The_action_was_completed_successfully__A_notification_e_mail_will_be_sent_to__0__and__1__shortly, oldEmailAddress, wssAccount.PrimaryEmailAddress);

                    WssAuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                }
                catch (Exception ex)
                {
                    message =
                        Resources
                            .AccountController_ChangeEmail_An_unexpected_error_ocurred_when_changing_the_email_address;
                    Logger.Error($"An unhandled exception occurred when changing the e-mail address.  Account: {wssAccountId},  New e-mail address: {changeEmail1}", ex);
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
                }
            return Json(new { Message = "", ReturnUrl = ConfigurationManager.AppSettings["BaseUrl"] }, JsonRequestBehavior.AllowGet);
            }



        [HttpPost]
        public JsonResult ChangeSecuritySettings(string securityQuestion, string securityQuestionAnswer, int wssAccountId)
        {
            if (wssAccountId != GetLoggedUserWssAccount())
            {
                return Json(new { message = "Sorry, you can not manage the account" }, JsonRequestBehavior.AllowGet);
            }

            var message = string.Empty;

            var wssAccouts = _wssUnitOfWork.WssAccountRepository.FindAll().Where(x => x.WSSAccountId == wssAccountId);
            if (!wssAccouts.Any())
            {
                return Json(new { message = Resources.AccountController_ChangeSecuritySettings_Account_with_this_Email_Address_is_not_found }, JsonRequestBehavior.AllowGet);
            }

            if (!(ValidateTextInput(securityQuestion, 255, @"^[\w\s\'@$()_{},.?:;-]+$") &&
                ValidateTextInput(securityQuestionAnswer, 255, @"^[\w\s\'@$()_{},.?:;-]+$")))
            {
                return Json(new { message = Resources.AccountController_ChangeSecurityQuestion_Text }, JsonRequestBehavior.AllowGet);
            }

                try
                {
                    var wssAccount = wssAccouts.FirstOrDefault();

                    if (wssAccount != null)
                    {
                        wssAccount.SecurityQuestion = securityQuestion;
                        wssAccount.SecurityQuestionAnswer = securityQuestionAnswer;
                        wssAccount.IsActive = true;

                        var description = string.Format(EventDescriptionHelper.ChangedSecurityQuestion);

                        _auditTransaction.AddAuditRecord("ChangedSecurityQuestion", AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.CHSECQ, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                              null, null, wssAccount.WSSAccountId, User?.Identity.Name, description);
                        _wssUnitOfWork.Save("0");
                    }
                    else
                    {
                        message = Resources.AccountController_ChangeSecuritySettings_Failed_to_Change_Security_Question;
                    }
                }
                catch (Exception)
                {
                    message = Resources.AccountController_ChangeSecuritySettings_Failed_to_Change_Security_Question;
                }

            return Json(new { message }, JsonRequestBehavior.AllowGet);
            }

        private Dictionary<string, string> GetQuestions()
        {
            var list = new Dictionary<string, string>();

            //     TODO: for now hardcoded until a decizion on where to store: database, file, enumerable - is made

            list.Add("What was the name of your favorite childhood friend?", "What was the name of your favorite childhood friend?");
            list.Add("What was the name of your elementary school?", "What was the name of your elementary school?");
            list.Add("Who is your favourite fictional character?", "Who is your favourite fictional character?");
            list.Add("What is your mother's maiden name?", "What is your mother's maiden name?");
            list.Add("What is the name of your first pet?", "What is the name of your first pet?");
            list.Add("Who is your favourite actor or actress?", "Who is your favourite actor or actress?");
            list.Add("What is your father's middle name?", "What is your father's middle name?");
            list.Add("What is your favourite flavor of ice cream?", "What is your favourite flavor of ice cream?");
            return list;
        }

        [HttpPost]
        public JsonResult ChangePassword(int wssAccountId, string currentPassword, string newPassword1,
            string newPassword2)
        {

            if (wssAccountId != GetLoggedUserWssAccount())
            {
                return Json(new { Message = "Sorry, you can not manage the account" }, JsonRequestBehavior.AllowGet);
            }

            if (!(ValidateTextInput(currentPassword, 0, @"(?=.*\d)(?=.*[a-zA-Z]).{7,15}")
                  && ValidateTextInput(newPassword1, 0, @"(?=.*\d)(?=.*[a-zA-Z]).{7,15}")
                  && ValidateTextInput(newPassword2, 0, @"(?=.*\d)(?=.*[a-zA-Z]).{7,15}"))
                )
            {
                return Json(new { Message = Resources.AccountViewModel_newPassword1_Invalid_Password }, JsonRequestBehavior.AllowGet);
            }

            var message = string.Empty;
            try
            {
                var wssAccount =
                  _wssUnitOfWork.WssAccountRepository.FindAll()
                      .FirstOrDefault(x => x.WSSAccountId.Equals(wssAccountId));

                if (wssAccount != null)
                {
                    var emailAddress = wssAccount.PrimaryEmailAddress;
                    var identityUser = IdentityUserManager.FindByEmail(emailAddress);
                    var result = IdentityUserManager.ChangePassword(identityUser.Id, currentPassword, newPassword2);

                    if (!result.Succeeded)
                    {
                        message = result.Errors.Aggregate(message, (current, error) => current + (error + " "));
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
                    }

                    var oldValue = Convert.ToString(wssAccount.WSSAccountId);
                    var newValue = Convert.ToString(wssAccount.WSSAccountId);
                    var userName = User?.Identity.Name;
                    var fieldName = EventType.ResetPassword;
                    var description = EventDescriptionHelper.ResetPassword;
                    _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.RSTPWD, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                        oldValue, newValue, wssAccountId, userName, description);

                    sendEmail.AddSingleEmail("WSS.ChangePasswordNotification", $"TO!{emailAddress}");
                }
                message =
                    $"A notification e-mail will be sent to {wssAccount?.PrimaryEmailAddress} shortly";
            }

            catch (Exception ex)
            {
                Logger.Error($"Exception occurred when attempting to change password.  Account ID {wssAccountId}", ex);

                message = Resources.AccountController_ChangePassword_Failed_to_Change_Password;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
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
            model.ShowSecuritySettings = false;

            //var WssAccountStatusId = entity.WssAccountStatusCode < 0 ? 0 : entity.WssAccountStatusCode;
            //switch (WssAccountStatusCode)
            switch (entity.WssAccountStatusCode ?? "")
            {
                // ReSharper disable once RedundantCaseLabel
                case "NTREG": //Not Registered
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
                    model.ShowSecuritySettings = true;
                    break;

                case "INACT": //Inactive
                    model.ShowChangeEmail = true;
                    model.ShowResendActivation = true;
                    model.ShowRestoreAccount = true;
                    model.ShowLockAccount = true;
                    model.ShowSecuritySettings = true;
                    break;

                case "LCKD": //Locked
                    model.ShowResetPassword = true;
                    model.ShowResendActivation = true;
                    break;
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


        [HttpPost]
        public JsonResult Unsubscribe(int wssAccountId, string reason, string othersText, string unsubscribeComments)
        {
            if (!ValidateTextInput(unsubscribeComments, 500, @"^[\w\s\'@$()_{},.?:;-]*$"))
            {
                return Json(new { isDeleted = false, errorMessage = Resources.AccountViewModel_UnsubscribeComments_Invalid_input_into_comment_field__Use_alphanumeric_input_only }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(reason) && !ValidateTextInput(othersText, 100, @"^[\w\s\'@$()_{},.?:;-]+$"))
            {
                return Json(new { isDeleted = false, errorMessage = Resources.AccountViewModel_UnsubscribeReasonOtherText_Invalid_input_into_reason_field__Use_alphanumeric_input_only }, JsonRequestBehavior.AllowGet);
            }

            if (wssAccountId != GetLoggedUserWssAccount())
            {
                return Json(new { isDeleted = false, errorMessage = "Sorry, you can not manage the account" }, JsonRequestBehavior.AllowGet);
            }

            // make sure an account with the supplied Id exists
            var objWssAccount = _wssUnitOfWork.WssAccountRepository.FindAll()
                    .FirstOrDefault(x => x.WSSAccountId == wssAccountId);
            var selectedWssAccount = objWssAccount?.WSSAccountId;

            if (selectedWssAccount == null)
            {
                return Json(new { isDeleted = false, errorMessage = Resources.AccountController_Unsubscribe_There_is_an_error_occured_while_unsubscribing },
                    JsonRequestBehavior.AllowGet);
            }
            else if (string.Compare(objWssAccount.AuthenticationId, User.Identity.GetUserId()) != 0)
            {
                WssAuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                return Json(new { isDeleted = false, errorMessage = Resources.AccountController_Unsubscribe_You_are_not_autenticated_to_unsubscribe_the_account },
                JsonRequestBehavior.AllowGet);
            }

            var message = ValidateFormInput(wssAccountId, reason, othersText, unsubscribeComments);

            if (!string.IsNullOrEmpty(message))
            {
                return Json(new { isDeleted = false, errorMessage = message }, JsonRequestBehavior.AllowGet);
            }

            // add reason or comment if needed
            if (string.Compare(reason, "Other") == 0)
            {
                reason += " - " + othersText.Trim();
            }

            if (!string.IsNullOrEmpty(unsubscribeComments))
            {
                reason += "<br/><stromg>Comment</strong>" + unsubscribeComments.Trim();
            }

            // Delete the login account
            var identityUser = IdentityUserManager.FindByEmail(objWssAccount.PrimaryEmailAddress);
            if (identityUser == null)
            {
                Logger.Error($"Could not find Identity User to delete during Unsubscription process.  Username: {objWssAccount.PrimaryEmailAddress}");
                return Json(new { isDeleted = false, errorMessage = Resources.AccountController_Unsubscribe_There_is_an_error_occured_while_unsubscribing }, JsonRequestBehavior.AllowGet);
            }

            var identityResult = IdentityUserManager.Delete(identityUser);
            if (!identityResult.Succeeded)
            {
                Logger.Error($"Could not delete Identity User during Unsubscription process.  Username: {objWssAccount.PrimaryEmailAddress}, Identity User ID: {identityUser.Id}");
                return Json(new { isDeleted = false, errorMessage = Resources.AccountController_Unsubscribe_There_is_an_error_occured_while_unsubscribing }, JsonRequestBehavior.AllowGet);
            }

            var objLinkedUtilityAccounts =
                _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll()
                    .Where(x => x.WssAccountId == objWssAccount.WSSAccountId);

            var utilityAccountIdsToBeUnlinked = objLinkedUtilityAccounts.Select(x => x.UtilityAccountId).ToArray();

            // Unlink the linked utility accounts
            foreach (var item in objLinkedUtilityAccounts)
            {
                _wssUnitOfWork.LinkedUtilityAccountsRepository.Delete(item);
            }

            // Soft delete/unsubscribe the WSS Account
            objWssAccount.IsActive = false;

            // Add SubscriptionTransaction table records for the unlinked accounts
            foreach (var currentUtilityAccountId in utilityAccountIdsToBeUnlinked)
            {
                var objsubscriptionTransaction = new SubscriptionTransaction()
                {
                    SubscriptionTransactionDate = DateTime.Now,

                    ccb_acct_id =
                        _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                            .FirstOrDefault(x => x.UtilityAccountId == currentUtilityAccountId)?
                            .ccb_acct_id,
                    //DF - Temporarily hardcoding these until the full refactor to use the DomainLookup table
                    //TODO Update these to pull from DomainLookup
                    SubscriptionTransactionTypeCode = "UNSUB",
                    SubscriptionTransactionStatusId = 1
                };
                _wssUnitOfWork.SubscriptionTransactionRepository.Insert(objsubscriptionTransaction);
            }
            _wssUnitOfWork.Save(User.Identity.Name);

            var secondaryEmailunsubscribeUrl = string.Empty;
            sendEmail.GetSecondaryEmailUnsubscribeUrl(objWssAccount.PrimaryEmailAddress, out secondaryEmailunsubscribeUrl);
            sendEmail.AddSingleEmail("WSS.Unsubscribe", $"TO!{objWssAccount.PrimaryEmailAddress},SecondaryEmailUnsubscribeUrl!{secondaryEmailunsubscribeUrl}");

            // Create and save audit record
            var oldValue = Convert.ToString(objWssAccount.WSSAccountId);
            var newValue = string.Empty;
            var userName = User?.Identity.Name;
            var fieldName = EventType.Unsubscribe;
            var wssaccountId = objWssAccount.WSSAccountId;
            var description = string.Format(EventDescriptionHelper.Unsubscribe, reason);
            _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.UNSUB, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                oldValue, newValue, wssaccountId, userName, description);
            _wssUnitOfWork.Save(User.Identity.Name);

            WssAuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            return Json(new { isDeleted = true }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ViewResult Thankyou()
        {
            return View("Thankyou");
        }




        [HttpPost]
        public ActionResult AdditionalEmail(string additionalemailAddress, int wssAccountId = 0)
        {
            var message = string.Empty;
            if (!ValidateEmail(additionalemailAddress))
            {
                return Json(new { message = "Sorry the email address is not valid" }, JsonRequestBehavior.AllowGet);
            }

            if (wssAccountId != GetLoggedUserWssAccount())
            {
                return Json(new { message = "Sorry, you can not manage the account" }, JsonRequestBehavior.AllowGet);
            }

            if (wssAccountId == 0)
            {
                return Json(new { message = Resources.AccountController_DeleteAdditionalEmailAddresses_The_account_does_not_exist }, JsonRequestBehavior.AllowGet);
            }

            var wssAccount =
                _wssUnitOfWork.WssAccountRepository.FindAll()
                    .FirstOrDefault(x => x.WSSAccountId.Equals(wssAccountId));

            var count = _wssUnitOfWork.WssAccountRepository.FindAll().Count(x => x.PrimaryEmailAddress == additionalemailAddress && x.WSSAccountId == wssAccountId);
            var userAdditionalEmails = _wssUnitOfWork.AdditionalEmailAddressRepository.FindAll().Where(m => m.WssAccountId == wssAccountId);
            var additionalEmail = userAdditionalEmails.FirstOrDefault(m => m.EmailAddress == additionalemailAddress && m.WssAccountId == wssAccountId);

            if (count > 0 || additionalEmail != null)
            {
                return Json(new { message = Resources.AccountController_ChangeEmail_Email_Already_Exists }, JsonRequestBehavior.AllowGet);
            }
            else if (userAdditionalEmails.Count() >= 5)
            {
                return Json(new { message = Resources.AccountController_AdditionalEmail_Email_count_exceed }, JsonRequestBehavior.AllowGet);

            }

            // all is fine since we have got here
            additionalEmail = new AdditionalEmailAddress()
            {
                EmailAddress = additionalemailAddress,
                WssAccount = wssAccount
            };

            if (wssAccount != null)
            {
                _wssUnitOfWork.AdditionalEmailAddressRepository.Insert(additionalEmail);

                //Audit 
                var oldEmailAddress = wssAccount.PrimaryEmailAddress;
                var oldValue = oldEmailAddress;
                var newValue = additionalemailAddress;
                var userName = User?.Identity.Name;
                var fieldName = "AdditionalEmail";
                var description = string.Format(EventDescriptionHelper.AddedSecondaryEmail, newValue);
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.ADDEMAIL, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT, oldValue, newValue, wssAccount.WSSAccountId, userName, description);

                _wssUnitOfWork.Save("0");

                //Send Email notification                    
                var utilityAccountId = Convert.ToInt32(Session["SelectedUtilityAccountId"].ToString());
                var utilityAccount = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().FirstOrDefault(m => m.UtilityAccountId.Value == utilityAccountId);
                var nickname = utilityAccount?.NickName;
                var ccbAcctId = _utilityUnitOfWork.UtilityAccountRepository.FindAll()
                        .FirstOrDefault(x => x.UtilityAccountId == utilityAccount.UtilityAccountId.Value)?
               .ccb_acct_id;

                var reg = new Regex(@"[0-9]");
                var ccbAccount = reg.Replace(ccbAcctId ?? string.Empty, "X", 4, 3);
                ccbAccount = ccbAccount + 'X';

                StringBuilder sb = new StringBuilder();
                try
                {
                    sendEmail.AddSingleEmail("WSS.AdditionalEmailAddedPrimaryAccountHolder",
                        $"TO!{wssAccount.PrimaryEmailAddress},<AdditionalEmailAdded>!{additionalemailAddress}");
                }
                catch (Exception)
                {
                    sb.Append("Failed to send message to: ");
                    sb.Append(wssAccount.PrimaryEmailAddress);
                }


                try
                {
                    var secondaryEmailunsubscribeUrl = string.Empty;
                    sendEmail.GetSecondaryEmailUnsubscribeUrl(additionalemailAddress, out secondaryEmailunsubscribeUrl);
                    sendEmail.AddSingleEmail("WSS.AdditionalEmailAdded", $"TO!{additionalemailAddress},<CCBAccount>!{ccbAccount},<-NickName>!{nickname},SecondaryEmailUnsubscribeUrl!{secondaryEmailunsubscribeUrl}");
                }
                catch (Exception)
                {
                    if (!sb.ToString().Contains("Failed to send message"))
                    {
                        sb.Append("Failed to send message to: ");
                    }
                    sb.Append(", " + additionalemailAddress);
                }

                ViewBag.EmailErrorMessage = "";
                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    ViewBag.EmailErrorMessage = sb.ToString();
                }

                var additionalEmailAddressData = GetAdditionalEmailAddresses(wssAccountId, 1, "10");
                return PartialView("_additionalEmailAddresses", additionalEmailAddressData);

            }
            else
            {
                return Json(new { Message = message, additionalEmail?.AdditionalEmailAddressId, additionalEmail?.WssAccountId, additionalEmail?.EmailAddress }, JsonRequestBehavior.AllowGet);
            }
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

        public PagedList.IPagedList<AdditionalEmailAddressViewModel> GetAdditionalEmailAddresses(int? wssAccountId, int? page,
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
            return new PagedList.PagedList<AdditionalEmailAddressViewModel>(records, page ?? 1,
               Convert.ToInt32(rowsPerPage));
        }


        public ActionResult DeleteAdditionalEmailAddresses(int additionalEmailAddressId = 0, int wssAccountId = 0)
        {
            if (wssAccountId != GetLoggedUserWssAccount())
            {
                return Json(new { message = "Sorry, you can not manage the account" }, JsonRequestBehavior.AllowGet);
            }

            if (wssAccountId == 0)
            {
                return Json(new { message = "The account does not exist" }, JsonRequestBehavior.AllowGet);
            }

            var additionalemailaddress = _wssUnitOfWork.AdditionalEmailAddressRepository.Get(additionalEmailAddressId);

            if (additionalemailaddress != null)
            {
                _wssUnitOfWork.AdditionalEmailAddressRepository.Delete(additionalemailaddress);
                //Audit
                var oldEmailAddress = additionalemailaddress.EmailAddress;
                var oldValue = oldEmailAddress;
                var newValue = additionalemailaddress.EmailAddress;
                var userName = User?.Identity.Name;
                var fieldName = "RemoveAdditionalEmail";

                var description = string.Format(EventDescriptionHelper.RemoveSecondaryEmail, oldValue);
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.REMEMAIL, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT, oldValue, newValue, wssAccountId, userName, description);
                //Send Email Notification
                var wssAccount =
           _wssUnitOfWork.WssAccountRepository.FindAll()
               .FirstOrDefault(x => x.WSSAccountId.Equals(wssAccountId));

                ViewBag.EmailErrorMessage = "";

                try
                {
                    var secondaryEmailunsubscribeUrl = string.Empty;
                    sendEmail.GetSecondaryEmailUnsubscribeUrl(wssAccount?.PrimaryEmailAddress, out secondaryEmailunsubscribeUrl);
                    sendEmail.AddSingleEmail("WSS.AdditionalEmailRemovedPrimaryAccountHolder",
                        $"TO!{wssAccount?.PrimaryEmailAddress},<AdditionalEmailRemoved>!{oldValue}, SecondaryEmailUnsubscribeUrl!{secondaryEmailunsubscribeUrl}");

                }
                catch (Exception)
                {
                    ViewBag.EmailErrorMessage = "Failed to send mail to " + wssAccount?.PrimaryEmailAddress;
                }
                
                _wssUnitOfWork.Save("0");
            }

            var additionalEmailAddressData = GetAdditionalEmailAddresses(wssAccountId, 1, "10");
            return PartialView("_additionalEmailAddresses", additionalEmailAddressData);
        }

        public string ValidateFormInput(int wssAccountId, string reason, string othersText,
            string unsubscribeComments)
        {
            if (wssAccountId == 0)
            {
                return "No account selected";
            }

            if (string.IsNullOrEmpty(reason))
            {
                return "Specify unsubscribe reason";
            }

            if (string.Compare(reason, "Other") == 0)
            {
                if (string.IsNullOrEmpty(othersText))
                {
                    return "Specify other unsubscribe reason";
                }
                else if (!ValidateTextInput(othersText, 100))
                {
                    return "Invalid input into reason field. Use alphanumeric input only. 100 characters or less";
                }
            }

            if (!ValidateTextInput(unsubscribeComments, 250))
            {
                return "Invalid input into comment field. Use alphanumeric input only. 250 characters or less";
            }
            return string.Empty;
        }

        private bool ValidateTextInput(string inputText, int maxLength = 0, string regexPattern = @"^[\w\s\'@$()_{},.?:;-]*$")
        {
            // var regexPattern = @"^[\w\s\'@$()_{},.?:;-]*$";  // Optional, allows empty string
            // var regexPattern = @"^[\w\s\'@$()_{},.?:;-]+$";  // at least one character
            if (maxLength == 0)
            {
                return Regex.IsMatch(inputText, regexPattern);
            }
            else
        {
            var matches = Regex.Match(inputText, regexPattern);
            if (inputText.Length <= maxLength)
            { return matches.Success; }
            else
            {
                return false;
            }
        }
        }

        private bool ValidateEmail(string emailAddress)
        {
            string regexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Match matches = Regex.Match(emailAddress, regexPattern);
            if (emailAddress.Length <= 254)
            { return matches.Success; }
            else
            {
                return false;
            }
        }


    }
}