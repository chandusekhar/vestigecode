using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WSS.CustomerApplication.Properties;

namespace WSS.CustomerApplication.Models
{
    public class ResendActivationEmailViewModel
    {
        [DisplayName("Enter the Email you used to register your Self-Service Account")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ResendActivationEmailViewModel_EmailAddress_E_mail_address_is_required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ResendActivationEmailViewModel_EmailAddress_Email_address_format_is_not_valid")]
        public string EmailAddress { get; set; }

        [DisplayName("you have {0} attempts left")]
        public int AttemptsLeft { get; set; }

        public int CurrentAttempt { get; set; }

        public int WssAccountId { get; set; }

        public string Actiontoken { get; set; }     
        public bool IsResendAvtivationMailSent { get; set; }
    }
}