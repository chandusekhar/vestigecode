using WWDCommon.Data;

namespace WSS.Email.Service
{
    /// <summary>
    ///
    /// </summary>
    public interface ISendEmail
    {
        /// <summary>
        /// Generates an e-mail message and places it in a queue in the database.  The e-mail message is generated based on the seleted template, and the template values to be populated from the string of key/value pairs.
        /// </summary>
        /// <param name="templateName">The name of the template to populate with the values from the key/value pair string.</param>
        /// <param name="keyValuePair">String containing the key/value pair(s) to be used to produce the message.  Each key and value are separated by the '=' character and multiple key/value pairs are separated by the ',' character.</param>
        void AddSingleEmail(string templateName, string keyValuePair);

        /// <summary>
        /// Adds an e-mail template to the repository, to be populated when an e-mail is sent
        /// </summary>
        /// <param name="emailTemplate">The e-mail template to add</param>
        void LoadEmailTemplate(EmailTemplate emailTemplate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        bool GetSecondaryEmailUnsubscribeUrl(string emailId,out string secondayEmailUnsubscribeUrl);
    }
}