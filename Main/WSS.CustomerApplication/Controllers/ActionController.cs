using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WSS.Common.Utilities.ActionLink;
using WSS.Data;
using System.Collections.Generic;
using Action = WSS.Data.Action;

namespace WSS.CustomerApplication.Controllers
{
    [RoutePrefix("")]
    public class ActionController : ApiController
    {
        private readonly IUnitOfWork _wssUnitofWork;
        public ActionController(IUnitOfWork wssUnitofWork)
        {
            _wssUnitofWork = wssUnitofWork;
        }

        [Route("action/{actionToken:length(36):regex(^([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$)}")]
        public IHttpActionResult Get(string actionToken)
        {
            Guid validActionToken = Guid.Empty;
            if (string.IsNullOrEmpty(actionToken) && !Guid.TryParse(actionToken, out validActionToken))
                return BadRequest("Invalid Action Token");

            var parameterList = Request.GetQueryNameValuePairs();


            var actionUrl = PerformLandbackAction(actionToken, parameterList);
            Uri loc = new Uri(actionUrl, UriKind.Relative);
            return Redirect(loc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public string PerformLandbackAction(string key, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var actionUrl = string.Empty;// System.Web.Configuration.WebConfigurationManager.AppSettings["applicationUrl"];
            var actionkey = parameters.FirstOrDefault(m => m.Key.ToLowerInvariant().Equals("action")).Value;
            var actionId = (ActionType)Enum.Parse(typeof(ActionType), actionkey, true);
            WssAccount objWssAccount;
            Action objActionData;
            switch (actionId)
            {
                case ActionType.UP:
                    objActionData = _wssUnitofWork.ActionDataRepository.FindAll().FirstOrDefault(x => x.ActionToken == key);
                    if (objActionData != null)
                    {
                        objWssAccount = _wssUnitofWork.WssAccountRepository.FindAll().FirstOrDefault(x => x.WSSAccountId == objActionData.WssAccountId && x.IsActive);
                        if (objWssAccount != null)
                        {
                            actionUrl = "/UnsubscribeSecondary/index?actionToken=" + objWssAccount.AuthenticationId.Replace("-", string.Empty) + "&wssAccountId=" + objWssAccount.WSSAccountId;
                        }
                    }
                    break;

                case ActionType.US:
                    objActionData = _wssUnitofWork.ActionDataRepository.FindAll().FirstOrDefault(x => x.ActionToken == key);
                    if (objActionData != null)
                    {
                        var secondaryEmail = parameters.FirstOrDefault(m => m.Key.ToLowerInvariant().Equals("email")).Value;
                        var objWssAdditionalEmailData = _wssUnitofWork.AdditionalEmailAddressRepository.FindAll().FirstOrDefault(a => a.WssAccountId == objActionData.WssAccountId && a.EmailAddress == secondaryEmail);
                        if (objWssAdditionalEmailData != null)
                        {
                            actionUrl = "/UnsubscribeSecondary/UnsubscribeSecondaryEmail?wssAdditionalEmailId=" + objWssAdditionalEmailData.AdditionalEmailAddressId + "&secondaryEmail=" + objWssAdditionalEmailData.EmailAddress;
                        }
                        else
                            actionUrl = "/Login/Index";
                    }
                    break;

                case ActionType.AA:
                    actionUrl = "AccountActivation";
                    break;

                case ActionType.RP:
                    objActionData = _wssUnitofWork.ActionDataRepository.FindAll().FirstOrDefault(x => x.ActionToken == key);
                    if (objActionData != null)
                    {
                        ;
                    }
                    actionUrl = "Password/ForgotPassword";
                    break;

                default:
                    actionUrl = actionUrl + "registration/index";   //for example only
                    break;
            }
            return actionUrl;   //for example only
        }
    }
}
