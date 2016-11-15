using System;
using System.Configuration;
using System.Web.Mvc;
using WSS.Common.Utilities;
using WSS.Email.Service;
using WSS.InternalApplication.Infrastructure;
using WSS.InternalApplication.Models;
using WSS.Logging.Service;

namespace WSS.InternalApplication.Controllers
{
    public class EmailServiceTestController : BaseController
    {
        private readonly ILogger _logger = new Logger(typeof(EmailServiceTestController));

        public EmailServiceTestController(ISendEmail emailService) : base(emailService)
        {
        }

        // GET: EmailServiceTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmail(EmailServiceTestViewModel model)
        {
            try
            {
                var bypassQueue = ConfigurationManager.AppSettings["WSS.Email.Service.BypassEmailQueue"].Equals("True",
                    StringComparison.InvariantCultureIgnoreCase);

                EmailService.AddSingleEmail("testMessage", $"TO={model.EmailAddress}");

                if (bypassQueue)
                {
                    AddMessage(UserMessageType.Success, "Test message was sent successfully");
                }
                else
                {
                    _logger.Warn(
                        "Attempted to test sending Email with WSS.Email.Service.BypassEmailQueue not set or set to false.  Email messages will be queued, but not sent directly.");
                    AddMessage(UserMessageType.Warning,
                        "Direct Email sending is currently disabled.  Email messages will be queued, but not sent directly.  To enable direct email sending, set WSS.Email.Service.BypassEmailQueue to \"true\" in web.config and try again.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Test message failed to be sent", ex);
                AddMessage(UserMessageType.Error, "Test message failed to be sent");
            }

            return View("Index");
        }
    }
}