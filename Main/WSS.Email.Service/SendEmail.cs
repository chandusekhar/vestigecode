using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using WSS.Logging.Service;
using WWDCommon.Data;

namespace WSS.Email.Service
{
    /// <summary>
    ///
    /// </summary>
    public class SendEmail : ISendEmail
    {
        private static readonly ILogger Logger = new Logger(typeof(SendEmail));

        private readonly IUnitOfWork _commonUnitOfWork;
        private readonly WSS.Data.IUnitOfWork _wssUnitofWork;

        /// <summary>
        /// constructor
        /// </summary>
        public SendEmail(IUnitOfWork commonUnitOfWork, WSS.Data.IUnitOfWork wssUnitofWork)
        {
            _commonUnitOfWork = commonUnitOfWork;
            _wssUnitofWork = wssUnitofWork;
        }

        /// <summary>
        /// Add single email
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="keyValuePair"></param>
        /// <returns></returns>
        public void AddSingleEmail(string templateName, string keyValuePair)
        {
            var emailQueue = new EmailQueue
            {
                TemplateName = templateName,
                Parameters = keyValuePair,
                EtlBatchNumber = string.Empty
            };

            var repository = _commonUnitOfWork.EmailQueueRepository;
            repository.Insert(emailQueue);
            _commonUnitOfWork.Save("0");

            var bypassQueue = ConfigurationManager.AppSettings["WSS.Email.Service.BypassEmailQueue"].Equals("True", StringComparison.InvariantCultureIgnoreCase);
            if (bypassQueue)
            {
                var firstOrDefault = _commonUnitOfWork.EmailQueueRepository.FindAll()
                    .FirstOrDefault(x => x.Parameters.Equals(keyValuePair) && x.TemplateName.Equals(templateName));

                int emailId;

                if (firstOrDefault != null)
                {
                    emailId = firstOrDefault.Emaild;
                }
                else
                {
                    emailId = -1;
                }

                ProcessEmailSingle(emailId);
            }
        }

        /// <summary>
        /// Load predefined email templates
        /// </summary>
        public void LoadEmailTemplate(EmailTemplate emailTemplate)
        {
            if (emailTemplate != null)
            {
                _commonUnitOfWork.EmailTemplateRepository.Insert(emailTemplate);
                _commonUnitOfWork.Save("0");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="wssAccountId"></param>
        /// <returns></returns>
        public bool GetSecondaryEmailUnsubscribeUrl(string emailId,out string secondayEmailUnsubscribeUrl)
        {
            try
            {
                secondayEmailUnsubscribeUrl = ConfigurationManager.AppSettings["BaseUrl"];
                var additionalEmailData=_wssUnitofWork.AdditionalEmailAddressRepository.FindAll().FirstOrDefault(x => x.EmailAddress == emailId);
                if (additionalEmailData != null)
                {
                    var actionToken = Guid.NewGuid().ToString();
                    var secondaryEmailUnsubscribeAction = new WSS.Data.Action
                    {
                        ActionName = "Wss.UnsubscribeSecondaryEmail",
                        ActionToken = actionToken,
                        ExpiryDateTime = DateTime.Now.AddDays(2),
                        WssAccountId = additionalEmailData.WssAccountId.Value
                    };
                    _wssUnitofWork.ActionDataRepository.Insert(secondaryEmailUnsubscribeAction);
                    _wssUnitofWork.Save(emailId);
                    secondayEmailUnsubscribeUrl = ConfigurationManager.AppSettings["BaseUrl"];
                    secondayEmailUnsubscribeUrl = secondayEmailUnsubscribeUrl + "action/" + actionToken + "/?action=US&email=" + emailId;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Assembles and sends a single e-mail message from the queue
        /// </summary>
        /// <param name="emailId">The E-mail ID for the message to prepare and send.</param>
        /// <returns></returns>
        private void ProcessEmailSingle(int emailId)
        {
            var singleEmail = _commonUnitOfWork.EmailQueueRepository.FindAll().SingleOrDefault(m => m.Emaild == emailId);            
            ProcesSendEmail(singleEmail);
        }

        /// <summary>
        /// Assembles and sends multiple e-mail messages from the queue
        /// </summary>
        /// <param name="etlBatchNumber">The ETL batch number for the messages to prepare and send.</param>
        private void ProcessEmailBatch(string etlBatchNumber)
        {
            var batchEmails = _commonUnitOfWork.EmailQueueRepository.FindAll().Where(x => x.EtlBatchNumber.Equals(etlBatchNumber));
            foreach (var email in batchEmails)
            {
                IDictionary<string, string> args = email.Parameters.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split('!')).ToDictionary(k => k[0].ToString(), k => k[1].ToString());
                var secondaryEmailnsubscribeUrl = string.Empty;
                if(GetSecondaryEmailUnsubscribeUrl(args["TO"],out secondaryEmailnsubscribeUrl))
                {
                    email.Parameters += ",<secondaryEmailnsubscribeUrl>!" + secondaryEmailnsubscribeUrl;
                }
                ProcesSendEmail(email);
            }
        }

        /// <summary>
        /// Retrieves the template and parameters for the e-mail message to be sent
        /// Updates the EmailTransaction table with a record of the sent message
        /// Removes the message from the e-mail queue
        /// </summary>
        /// <param name="singleEmail"></param>
        private void ProcesSendEmail(EmailQueue singleEmail)
        {
            var emailTemplate = _commonUnitOfWork.EmailTemplateRepository.FindAll().FirstOrDefault(x => x.TemplateName == singleEmail.TemplateName);
            IDictionary<string, string> args = singleEmail.Parameters.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split('!')).ToDictionary(k => k[0].ToString(), k => k[1].ToString());
            var to = args["TO"];
            if (emailTemplate != null)
            {
                var cc = emailTemplate.Defaultcc;
                var bcc = emailTemplate.Defaultbcc;

                if (args.Keys.Contains("CC") && !string.IsNullOrEmpty(args["CC"]))
                {
                    cc += "," + args["CC"];
                    args.Remove("CC");
                }
                if (args.Keys.Contains("BCC") && !string.IsNullOrEmpty(args["BCC"]))
                {
                    bcc += "," + args["BCC"];
                    args.Remove("BCC");
                }
                args.Remove("TO");
                var emailBody = CreateEmailBody(emailTemplate.MessageBody, args);
                DoSendEmail(emailTemplate.DefaultFrom, to, emailTemplate.Subject, emailBody, cc, bcc);

                // Delete the processed e-mail from the queue and add a record to the Transactions log
                var emailTransaction = new EmailTransaction()
                {
                    EmailTo = to,
                    IsProcessed = true,
                    TemplateId = _commonUnitOfWork.EmailTemplateRepository.FindAll().FirstOrDefault(x => x.TemplateName.Equals(singleEmail.TemplateName))?.TemplateId,
                    Parameters = singleEmail.Parameters
                };
                _commonUnitOfWork.EmailTransaction.Insert(emailTransaction);
                _commonUnitOfWork.EmailQueueRepository.Delete(singleEmail);
            }
            else
            {
                Logger.Error("Template for e-mail message for template name: {0} could not be found.  E-mail message was not sent.", singleEmail.TemplateName);
            }
        }

        /// <summary>
        /// Parses the template and parameters into a message in string format 
        /// If there are no parameters, it simply returns the template body unmodified.
        /// </summary>
        /// <param name="templateBody"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static string CreateEmailBody(string templateBody, IDictionary<string, string> arguments)
        {
            if (arguments != null && arguments.Count > 0)
            {
                foreach (var key in arguments.Keys)
                {
                    templateBody = templateBody.Replace($"<{key.Trim()}>", arguments[key].ToString().Trim());
                }
            }
            foreach (var key in arguments.Keys)
            {
                templateBody = templateBody.Replace($"{key.Trim() }", arguments[key].ToString().Trim());
            }
            return templateBody;
        }

        /// <summary>
        /// Composes a MailMessage object from the input parameters
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="emailBody"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <returns></returns>
        private static void DoSendEmail(string from, string to, string subject, string emailBody, string cc, string bcc)
        {
            var message = new MailMessage(from, to)
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = emailBody
            };

            if (string.IsNullOrEmpty(cc) == false)
            {
                var ccSplit = cc.Split(',', ';');
                foreach (var x in ccSplit)
                {
                    message.CC.Add(x);
                }
            }

            if (string.IsNullOrEmpty(bcc) == false)
            {
                var bccSplit = bcc.Split(',', ';');
                foreach (var x in bccSplit)
                {
                    message.Bcc.Add(x);
                }
            }

            CreateSmtp(message);
        }

        /// <summary>
        ///  Performs the actual sending of the message
        /// </summary>
        /// <param name="message"></param>
        private static void CreateSmtp(MailMessage message)
        {
            try
            {
                var smtpHostname = ConfigurationManager.AppSettings["SMTPHostName"];
                var smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPHostPort"]);
                var networkCredientials = CredentialCache.DefaultNetworkCredentials;

                var client = new SmtpClient(smtpHostname, smtpPort)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    DeliveryFormat = SmtpDeliveryFormat.International,
                    EnableSsl = false,
                    Credentials = networkCredientials,
                    UseDefaultCredentials = false
                };
                client.Send(message);
            }
            catch (Exception ex)
            {
                Logger.Error("An Error occurred when attempting to send the message.", ex);
                throw ex;
            }
        }
    }
}