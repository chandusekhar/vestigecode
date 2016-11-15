using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities;
using WSS.Common.Utilities.ActionLink;
using WSS.Common.Utilities.Transactions;
using WSS.Email.Service;
using WSS.InternalApplication.Authorization;
using WSS.InternalApplication.CustomAttributes;
using WSS.InternalApplication.Helper;
using WSS.InternalApplication.Infrastructure;
using WSS.InternalApplication.Models;
using WSS.InternalApplication.Properties;
using WSS.Logging.Service;
using EventType = WSS.InternalApplication.Helper.EventType;
using IUnitOfWork = UtilityBilling.Data.IUnitOfWork;

namespace WSS.InternalApplication.Controllers
{
    [Authorize]
    [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.CustomerRegistrations)]
    public class RegisterController : BaseController
    {
        //private readonly ISendEmail _emailService;
        private static readonly ILogger Logger = new Logger(typeof(RegisterController));

        private readonly IUnitOfWork _ubUnitofWork;
        private readonly Data.IUnitOfWork _unitOfWork;
        private readonly IAuditTransaction _auditTransaction;
        private readonly IActionLinkManager _actionLinkManager;

        public RegisterController(Data.IUnitOfWork unitOfWork, IUnitOfWork ubUnitofWork, SendEmail emailService, IAuditTransaction auditTransaction, IActionLinkManager actionLinkManager) : base(emailService)
        {
            _unitOfWork = unitOfWork;
            _ubUnitofWork = ubUnitofWork;
            _auditTransaction = auditTransaction;
            _actionLinkManager = actionLinkManager;
        }

        [CanExecuteFunction(FunctionCode = Functions.CustomerRegistrations, Roles = Roles.CSR)]
        // GET: Register
        [HttpGet]
        public ActionResult Index(string ccbAccountNumber, string customerName)
        {
            var model = new RegisterViewModel();
            if (!string.IsNullOrEmpty(ccbAccountNumber))
            { model.CcbAccountNumber = ccbAccountNumber; }

            if (!string.IsNullOrEmpty(customerName))
            {
                model.CustomerName = customerName;
                model.NewOrExistingCcdAccount = "existing";
            }
            else
            {
                model.CustomerName = string.Empty;
                model.NewOrExistingCcdAccount = "new";
            }

            ViewBag.IsMenuHidden = "hidden";
            ViewBag.WizardId = "First";
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            ViewBag.WizardId = "First";

            var records =
                 _unitOfWork.WssAccountRepository.FindAll().Where(x => x.PrimaryEmailAddress == model.EmailAddress && x.IsActive == true);

            if (records.Any())
            {
                ModelState.AddModelError("EmailAddress", Resources.RegisterController_Register_Email_address_already_exists);
            }
            if (ModelState.IsValid)
            {
                if (model.NewOrExistingCcdAccount == "new")
                {
                    model.CustomerName = "**" + model.CustomerName;
                    ViewBag.DoubleStarMessage = "Names starting with ** are temprory names for the new CCB Customers ";
                }
                else
                {
                    ViewBag.DoubleStarMessage = string.Empty;
                }

                ViewBag.WizardId = "Second";
            }
            return View("Index", model);
        }

        public ActionResult Confirm(RegisterViewModel model)
        {
            var EmailAddressAvailable = _unitOfWork.WssAccountRepository.FindAll().Where(x => x.PrimaryEmailAddress.ToLower().Equals(model.EmailAddress.ToLower()) && x.IsActive == true);
            if (EmailAddressAvailable.Any())
            {
                AddMessage(UserMessageType.Error, "Email address" + model.CcbAccountNumber + " already registered", "Registration");
                ViewBag.WizardId = "First";
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
                if (utilityAccount != null)
                {
                    var account = utilityAccount;
                    var linkedUtilityAccounts = _unitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(m => m.UtilityAccountId == account.UtilityAccountId);
                    if (linkedUtilityAccounts != null && linkedUtilityAccounts.Any())
                    {
                        Logger.Error(
                    "Utility Account {0} already exist in Linked Account {1}",
                    model.CcbAccountNumber, linkedUtilityAccounts.FirstOrDefault()?.LinkedUtilityAccountId);
                        AddMessage(UserMessageType.Error,
                            "Failed to register the Web Self-Service profile for Account Number: " + model.CcbAccountNumber +
                            ", Customer Name: " + model.CustomerName +
                            ", E-mail address: " + model.EmailAddress);
                        ViewBag.WizardId = "First";
                        return View("Index", model);
                    }
                }

                var registrationSuccess = utilityAccount == null
                    ? registrationTransaction.RegisterNewUtilityAccount()
                    : registrationTransaction.RegisterExistingUtilityAccount();
                Logger.Debug(
                    $"Registration result for Utility Account Number: {model.CcbAccountNumber}, Customer Name: {model.CustomerName}, E -mail address: {model.EmailAddress} was {registrationSuccess}");
                if (utilityAccount == null)
                    utilityAccount =
                        _ubUnitofWork.UtilityAccountRepository.FindAll()
                            .FirstOrDefault(x => x.ccb_acct_id.Equals(model.CcbAccountNumber));
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
                            $"Registration of the Web Self-Service profile failed for Account Number: {model.CcbAccountNumber}, Customer Name: {model.CustomerName}, E-mail address: {model.EmailAddress}.  Please verify the information that was entered and try again.");
                        ViewBag.WizardId = "First";
                        return View("Index", model);
                    }
                    var wssaccountId = wssAccount?.WSSAccountId;
                    var fieldName = EventType.Register;
                    var description = string.Format(EventDescriptionHelper.CsrRegister, model.EmailAddress);
                    _auditTransaction.AddAuditRecord(fieldName, AuditTransaction.Implementation.AuditTransaction.AuditTransactionEventType.REG, AuditTransaction.Implementation.AuditTransaction.AuditTransactionSubject.WSS_ACCT,
                        String.Empty, Convert.ToString(wssaccountId), wssaccountId, User?.Identity.Name, description);
                    _unitOfWork.Save("0");

                    var sendEmailSuccess = SendActivationEmail(wssAccount.WSSAccountId, wssAccount.PrimaryEmailAddress,
                        string.Empty);
                    if (sendEmailSuccess)
                    {
                        AddMessage(UserMessageType.Success,
                            $"Successfully registered the Web Self-Service profile for Account Number: {model.CcbAccountNumber}, Customer Name: {model.CustomerName}, E-mail address: {model.EmailAddress}");
                        return RedirectToAction("Index", "Account", new { id = utilityAccount.UtilityAccountId });
                    }
                    AddMessage(UserMessageType.Warning,
                        $"Successfully registered the Web Self-Service profile, but the Activation e-mail message failed to send.  Account Number: {model.CcbAccountNumber}, Customer Name: {model.CustomerName}, E-mail address: {model.EmailAddress}.  Please attempt to resend the activation e-mail from the Web Self-Service Profile page.");
                    return RedirectToAction("Index", "Account", new { id = utilityAccount.UtilityAccountId });
                }

                Logger.Error(
                    "Failed to register the Web Self-Service profile for Account Number: {0}, Customer Name: {1}, E-mail address: {2}",
                    model.CcbAccountNumber, model.CustomerName, model.EmailAddress);
                AddMessage(UserMessageType.Error,
                    "Failed to register the Web Self-Service profile for Account Number: " + model.CcbAccountNumber +
                    ", Customer Name: " + model.CustomerName +
                    ", E-mail address: " + model.EmailAddress);
                ViewBag.WizardId = "First";
                return View("Index", model);
            }
            catch (Exception ex)
            {
                Logger.Error(
                    "Exception occurred while registering the Web Self-Service profile for Account Number: {0}, Customer Name: {1}, E-mail address: {2}.  Exception: {3}",
                    model.CcbAccountNumber, model.CustomerName, model.EmailAddress, ex.StackTrace);
                AddMessage(UserMessageType.Error,
                    "Failed to register the Web Self-Service profile for Account Number: " + model.CcbAccountNumber +
                    ", Customer Name: " + model.CustomerName +
                    ", E-mail address: " + model.EmailAddress);
                ViewBag.WizardId = "First";
                return View("Index", model);
            }
        }

        public bool SendActivationEmail(int wssAccountId, string emailAddress, string activationToken)
        {
            try
            {
                var activationLink = _actionLinkManager.GenerateActivateAccountLink(wssAccountId);
                var keyValuePair = $"TO!{emailAddress},activationLink!{activationLink},<AccountActivationLink>!{activationLink}";
                EmailService.AddSingleEmail("WSS.Activation", keyValuePair);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}