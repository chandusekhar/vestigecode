using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WSS.CustomerApplication.Properties;

namespace WSS.CustomerApplication.Models
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            EmailAddress = "";
            ReenterEmail = "";
        }
        [DisplayName(@"Account Number:")]
        public string CcbAccountNumber { get; set; }

        [DisplayName(@"E-mail Address : ")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterViewModel_EmailAddress_Please_enter_email_address", AllowEmptyStrings = true)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterViewModel_EmailAddress_Invalid_Email_address")]
        public string EmailAddress { get; set; }

        [DisplayName(@"Customer Name : ")]
        //[Required(ErrorMessage = "Please enter temporary customer name")]
        public string CustomerName { get; set; }

        [DisplayName(@"My Utility Account Number:")]
        public string MyUtilityAccountNumber { get; set; }

        [DisplayName(@"My 1 Meter Number for Account:")]
        public string MeterNumberForAccount { get; set; }

        [DisplayName(@"Re-Enter Email Address: ")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterViewModel_ReenterEmail_Please_enter_email_address", AllowEmptyStrings = true)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterViewModel_ReenterEmail_Invalid_Email_address")]
        [Compare("EmailAddress", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterViewModel_ReenterEmail_Email_addresses_do_not_match")]

        public string ReenterEmail { get; set; }

        public string NewOrExistingCcdAccount { get; set; }

        public bool IsMeter { get; set; }

        public string AboutAccount { get; set; }

    }
}