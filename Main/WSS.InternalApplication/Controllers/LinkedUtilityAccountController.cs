using WSS.Common.Utilities;
using WSS.InternalApplication.Authorization;
using WSS.InternalApplication.Properties;

namespace WSS.InternalApplication.Controllers
{
    using AuditTransaction.Interfaces;
    using CustomAttributes;
    using Data;
    using Email.Service;
    using Helper;
    using Infrastructure;
    using Logging.Service;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerRegistrations)]
    public class LinkedUtilityAccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _ubUnitofWork;
        private static readonly ILogger Logger = new Logger(typeof(LinkedUtilityAccountController));
        private readonly IAuditTransaction _auditTransaction;

        public LinkedUtilityAccountController(IUnitOfWork unitOfWork, UtilityBilling.Data.IUnitOfWork ubUnitofWork, SendEmail emailService, IAuditTransaction auditTransaction) : base(emailService)
        {
            _unitOfWork = unitOfWork;
            _ubUnitofWork = ubUnitofWork;
            _auditTransaction = auditTransaction;
        }

        [CanExecuteFunction(FunctionCode = Functions.CustomerRegistrations, Roles = Roles.CSR)]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.WizardId = "First";
            var model = new Models.LinkedUtilityAccountViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Next(Models.LinkedUtilityAccountViewModel model)
        {
            ViewBag.WizardId = "Second";
            var utilityaccount = _ubUnitofWork.UtilityAccountRepository.FindAll().FirstOrDefault(m => m.ccb_acct_id == model.SearchUtilityAccountNumber);
            if (utilityaccount == null)
            {
                ModelState.AddModelError("SearchUtilityAccountNumber", Resources.LinkedUtilityAccountController_Next_The_specified_utility_account_number_could_not_be_found);
                ViewBag.WizardId = "First";
                return View("Index", model);
            }

            var linkedUtilityAccounts = _unitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(m => m.UtilityAccountId == utilityaccount.UtilityAccountId);
            if (linkedUtilityAccounts != null && linkedUtilityAccounts.Count() > 0)
            {
                ModelState.AddModelError("SearchUtilityAccountNumber", Resources.LinkedUtilityAccountController_Next_This_utility_account_number_is_not_available);
                ViewBag.WizardId = "First";
            }
            else
            {
                var linkedaccount = _unitOfWork.LinkedUtilityAccountsRepository.FindAll().FirstOrDefault(m => m.UtilityAccountId == utilityaccount.UtilityAccountId);
                model.CcbAccountNumber = utilityaccount.ccb_acct_id;
                model.CustomerName = utilityaccount.PrimaryAccountHolderName; //linkedaccount.NickName;
                model.EmailAddress = string.Empty;
                model.UtilityAccountNumber = utilityaccount.UtilityAccountId;
            }
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Confirm(Models.LinkedUtilityAccountViewModel model)
        {
            var utilityAccount = _ubUnitofWork.UtilityAccountRepository.FindAll().FirstOrDefault(m => m.UtilityAccountId == model.UtilityAccountNumber);
            if (utilityAccount != null)
            {
                var wssAccountid = Convert.ToInt32(Session["ManageWSSAccountId"]);
                var oldlinkedutilityaccounts = _unitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(m => m.WssAccountId == wssAccountid && m.DefaultAccount == true);
                var linkedUtilityAccount = new WSS.Data.LinkedUtilityAccount()
                {
                    NickName = string.Empty,
                    WssAccountId = wssAccountid,
                    UtilityAccountId = utilityAccount.UtilityAccountId,
                    DefaultAccount = (oldlinkedutilityaccounts != null && oldlinkedutilityaccounts.Count() > 0) ? false : true
                };
                _unitOfWork.LinkedUtilityAccountsRepository.Insert(linkedUtilityAccount);

                var ccbAcctId =
                _ubUnitofWork.UtilityAccountRepository.FindAll()
                    .FirstOrDefault(x => x.UtilityAccountId == linkedUtilityAccount.UtilityAccountId.Value)?
                    .ccb_acct_id;

                var oldValue = string.Empty;
                var newValue = model.CcbAccountNumber;               
                var userName = User?.Identity.Name;
                var wssaccountId = linkedUtilityAccount.WssAccountId;
                var fieldName = Helper.EventType.LinkedAccount;
                var description = string.Format(EventDescriptionHelper.CsrLinkedAccount, ccbAcctId);
                _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.LNKACCT, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                    oldValue, newValue, wssaccountId, userName, description);

                _unitOfWork.Save("0");
            }
            return RedirectToAction("Index", "Account", new { id = model.UtilityAccountNumber, from = Constants.AddLinkedUtilityAccount });
        }
    }
}