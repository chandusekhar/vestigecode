using System.Collections.Generic;
using System.Web.Mvc;
using WSS.Common.Utilities;
using WSS.CustomerApplication.Infrastructure;
using WSS.CustomerApplication.Models;
using WSS.Email.Service;

namespace WSS.CustomerApplication.Controllers
{
    public class BaseController : Controller
    {
        public readonly ISendEmail sendEmail;
        //protected readonly IAuditTransaction _auditTransaction;

        public BaseController(ISendEmail sendEmail/*, IAuditTransaction _auditTransaction*/)
        {
            this.sendEmail = sendEmail;
            //this._auditTransaction = _auditTransaction;
        }

        /// <summary>
        /// Add a message to be displayed on the next page view.
        /// </summary>
        /// <param name="type">Type of the message, success, error etc.</param>
        /// <param name="message">Text of the message to show</param>
        /// <param name="title">Title to show</param>
        public void AddMessage(UserMessageType type, string message, string title)
        {
            var messages = new List<UserMessageModel>
                    {
                        new UserMessageModel()
                            {
                                Message = message,
                                Title = title,
                                Type = type
                            }
                    };
            TempData["Notifications"] = messages;
        }

        /// <summary>
        /// Add a message to be displayed on the next page view, no title shown.
        /// </summary>
        /// <param name="type">Type of the message, success, error etc.</param>
        /// <param name="message">Text of the message to show</param>
        public void AddMessage(UserMessageType type, string message)
        {
            AddMessage(type, message, string.Empty);
        }

        /// <summary>
        /// Clear any pending user messages.
        /// </summary>
        public void ClearMessages()
        {
            TempData["Notifications"] = new List<UserMessageModel>();
        }
    }
}