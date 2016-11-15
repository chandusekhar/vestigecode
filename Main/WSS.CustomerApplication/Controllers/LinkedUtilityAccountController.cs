using System.Collections.Generic;
using System.Text.RegularExpressions;
using WSS.CustomerApplication.Infrastructure;
using WSS.CustomerApplication.Properties;

namespace WSS.CustomerApplication.Controllers
{
    using AuditTransaction.Interfaces;
    using Data;
    using Email.Service;
    using Helper;
    using Logging.Service;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common.Utilities;
    using ExtetionClasses;

    [Authorize]
    public class LinkedUtilityAccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _ubUnitofWork;
        private static readonly ILogger Logger = new Logger(typeof(LinkedUtilityAccountController));
        private readonly IAuditTransaction _auditTransaction;
        private readonly ISendEmail _emailService;

        public LinkedUtilityAccountController(IUnitOfWork unitOfWork, UtilityBilling.Data.IUnitOfWork ubUnitofWork,
            SendEmail emailService, IAuditTransaction auditTransaction) : base(emailService)
        {
            _unitOfWork = unitOfWork;
            _ubUnitofWork = ubUnitofWork;
            _auditTransaction = auditTransaction;
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new Models.LinkedUtilityAccountViewModel();
            ViewBag.AccountHolderRelationshipQuestions = ViewDisplayHelper.GetAccountHolderRelationshipSelectListItems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Next(Models.LinkedUtilityAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.AccountHolderRelationshipQuestions =
                    ViewDisplayHelper.GetAccountHolderRelationshipSelectListItems();
                model.CcbAccountNumber = model.CcbAccountNumber.Left(10);
                var existingUtilityAccount =
                    _ubUnitofWork.UtilityAccountRepository.FindAll()
                        .FirstOrDefault(x => x.ccb_acct_id == model.CcbAccountNumber);

                if (existingUtilityAccount == null)
                {
                    AddMessage(UserMessageType.Error,
                        Resources.LinkedUtilityAccountController_Next_Utility_account_ + model.CcbAccountNumber +
                        Resources.LinkedUtilityAccountController_Next__does_not_exists_,
                        Resources.LinkedUtilityAccountController_Next_Registration);
                    return View("Index", model);
                }

                var isAccountRegistered = _unitOfWork.LinkedUtilityAccountsRepository.FindAll()
                    .Any(x => x.UtilityAccountId == existingUtilityAccount.UtilityAccountId);

                if (isAccountRegistered)
                {
                    AddMessage(UserMessageType.Error,
                        Resources.LinkedUtilityAccountController_Next_Utility_account_ + model.CcbAccountNumber +
                        Resources.LinkedUtilityAccountController_Next__already_registered_,
                        Resources.LinkedUtilityAccountController_Next_Registration);
                }
                else if (CommonMethods.ValidateUtilityAccountNumberAndMeterNumber(_ubUnitofWork, model.CcbAccountNumber,
                    model.MeterNumberForAccount, model.IsUnmetered))
                {
                    model.CustomerName = existingUtilityAccount.PrimaryAccountHolderName;
                    model.EmailAddress = string.Empty;
                    model.UtilityAccountNumber = existingUtilityAccount.UtilityAccountId;

                    var wssAccountid = Convert.ToInt32(Session["UserWSSAccountId"]);
                    var oldlinkedutilityaccounts =
                        _unitOfWork.LinkedUtilityAccountsRepository.FindAll()
                            .Where(m => m.WssAccountId == wssAccountid && m.DefaultAccount == true);
                    var linkedUtilityAccount = new LinkedUtilityAccount()
                    {
                        NickName = string.Empty,
                        WssAccountId = wssAccountid,
                        UtilityAccountId = existingUtilityAccount.UtilityAccountId,
                        DefaultAccount = (!oldlinkedutilityaccounts.Any())
                    };
                    _unitOfWork.LinkedUtilityAccountsRepository.Insert(linkedUtilityAccount);

                    var ccbAcctId = _ubUnitofWork.UtilityAccountRepository.FindAll()
                        .FirstOrDefault(x => x.UtilityAccountId == linkedUtilityAccount.UtilityAccountId.Value)?
                        .ccb_acct_id;

                    var oldValue = string.Empty;
                    var newValue = model.CcbAccountNumber;
                    var userName = User?.Identity.Name;
                    var wssaccountId = linkedUtilityAccount.WssAccountId;
                    var fieldName = EventType.LinkedAccount;
                    var description = string.Format(EventDescriptionHelper.LinkedAccount, ccbAcctId,
                        model.AccountHolderRelationship);
                    _auditTransaction.AddAuditRecord(fieldName,
                        AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.LNKACCT,
                        AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                        oldValue, newValue, wssaccountId, userName, description);

                    _unitOfWork.Save(User.Identity.Name);
                    var wssAccount = _unitOfWork.WssAccountRepository.Get(linkedUtilityAccount.WssAccountId);
                    var emailAddress = wssAccount?.PrimaryEmailAddress;
                    Regex reg = new Regex(@"[0-9]");
                    string ccbAccount = reg.Replace(ccbAcctId ?? string.Empty, "X", 4, 3);
                    ccbAccount = ccbAccount + 'X';
                    _emailService.AddSingleEmail("WSS.LinkedUtilityAccountAdded",
                        $"TO!{emailAddress},<CCBAccount>!{ccbAccount},<-NickName>!{linkedUtilityAccount.NickName}");
                    AddMessage(UserMessageType.Success,
                        Resources.LinkedUtilityAccountController_Next_Utility_account_has_been_linked_successfully,
                        Resources.LinkedUtilityAccountController_Next_Linked_Account);
                    return RedirectToAction("Index", "Account",
                        new {id = model.UtilityAccountNumber, TabId = "Manage Utility Accounts"});
                }
                else
                {
                    if (model.IsUnmetered)
                        AddMessage(UserMessageType.Error,
                            Resources.LinkedUtilityAccountController_Next_Invalid_Utility_account_ +
                            model.CcbAccountNumber + Resources.LinkedUtilityAccountController_Next__or_Meter_Number +
                            model.MeterNumberForAccount, Resources.LinkedUtilityAccountController_Next_Registration);
                    else
                        AddMessage(UserMessageType.Error,
                            Resources.LinkedUtilityAccountController_Next_Invalid_Utility_account_ +
                            model.CcbAccountNumber,
                            Resources.LinkedUtilityAccountController_Next_Registration);
                }
            }
            else
            {
                AddMessage(UserMessageType.Error,
                        Resources.LinkedUtilityAccountController_Invalid_fild_input,
                        Resources.LinkedUtilityAccountController_Next_Registration);
            }
            return View("Index", model);
        }
    }
}