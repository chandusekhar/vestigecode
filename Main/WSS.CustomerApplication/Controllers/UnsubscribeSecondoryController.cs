using AutoMapper;
using System;
using System.Linq;
using System.Web.Mvc;
using WSS.AuditTransaction.Interfaces;
using WSS.Common.Utilities;
using WSS.CustomerApplication.Models;
using WSS.Data;
using WSS.Email.Service;

namespace WSS.CustomerApplication.Controllers
{
    public class UnsubscribeSecondaryController : BaseController
    {
        private readonly IUnitOfWork _wssUnitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _utilityUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditTransaction _auditTransaction;

        public UnsubscribeSecondaryController(IUnitOfWork wssUnitOfWork, UtilityBilling.Data.IUnitOfWork utilityUnitOfWork,
            IMapper mapper, IAuditTransaction auditTransaction, ISendEmail sendEmail) : base(sendEmail)
        {
            _wssUnitOfWork = wssUnitOfWork;
            _utilityUnitOfWork = utilityUnitOfWork;
            _mapper = mapper;
            _auditTransaction = auditTransaction;
        }

        // GET: Unsubscribe
        /// <summary>
        /// Unsubscribe Primary Email address get action
        /// </summary>
        /// <param name="actionToken"></param>
        /// <param name="wssAccountId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(Guid actionToken, int wssAccountId)
        {
            return View();
        }


        /// <summary>
        /// Unsubscribe primary email address post action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(UnsubscribeViewModel model)
        {
            var objWssccount = _wssUnitOfWork.WssAccountRepository.FindAll().FirstOrDefault(x => x.WSSAccountId == model.WssAccountId && x.PrimaryEmailAddress.ToLower().Equals(model.EmailAddress.ToLower()) && x.IsActive);
            if (objWssccount == null)
            {
                AddMessage(UserMessageType.Error, "Account has not beed unsubscribed successfully", "Unsubscribe");
                return View(model);
            }

             var objLinkedUtilityAccount = _wssUnitOfWork.LinkedUtilityAccountsRepository.FindAll().FirstOrDefault(x => x.WssAccountId == model.WssAccountId);
             objWssccount.IsActive = false;
             _wssUnitOfWork.LinkedUtilityAccountsRepository.Delete(objLinkedUtilityAccount);
             _wssUnitOfWork.Save("0");
             AddMessage(UserMessageType.Success, "Account has beed unsubscribed successfully", "Unsubscribe");
             return View(model);
        }

        /// <summary>
        /// Unsubscribe secondary email address get action
        /// </summary>
        /// <param name="wssAdditionalEmailId">Secondary email address id</param>
        /// <param name="secondaryEmail">secondary email</param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult UnsubscribeSecondaryEmail(int wssAdditionalEmailId, string secondaryEmail)
        {
            var unsubscribeViewModel = new UnsubscribeViewModel
            {
                EmailAddress = secondaryEmail,
                WssAdditionalEmailId = wssAdditionalEmailId
            };
            return View(unsubscribeViewModel);
        }

        /// <summary>
        /// Unsubscribe secondary email address post action
        /// </summary>
        /// <param name="model">Object having details of secondary email for unsubscription</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UnsubscribeSecondaryEmail(UnsubscribeViewModel model, string submit)
        {
            if (submit == "YES")
            {
                var wssAdditionalEmailData = _wssUnitOfWork.AdditionalEmailAddressRepository.FindAll().FirstOrDefault(x => x.AdditionalEmailAddressId == model.WssAdditionalEmailId);
                if (wssAdditionalEmailData != null)
                {
                    _wssUnitOfWork.AdditionalEmailAddressRepository.Delete(wssAdditionalEmailData);
                    _wssUnitOfWork.Save(wssAdditionalEmailData.EmailAddress);
                    ViewBag.UnsubscriptionSecondaryEmailMessage = "Specified email has been unsubscribed successfully";
                }
                else
                {
                    ViewBag.UnsubscriptionSecondaryEmailMessage = "Invalid unsubscription request";
                }
            }
            else
            {
                ViewBag.UnsubscriptionSecondaryEmailMessage = "Your request to unsubscribe has been cancelled";
            }
            return View();
        }
    }
}