using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WSS.CustomerApplication.Properties;

namespace WSS.CustomerApplication.Models
{
    public class ForgotPassowrdViewModel
    {

        [DisplayName("Your new Password")]    
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ForgotPassowrdViewModel_Password_Password_is_required")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-zA-Z]).{7,15}", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ForgotPassowrdViewModel_Password_Password_has_to_be_7_to_15_characters_long_and_contain_at_least_one_letter_and_one_digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Re-enter your new Password")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ForgotPassowrdViewModel_ConfirmPassword_Confirm_password_is_required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ForgotPassowrdViewModel_ConfirmPassword_Confirm_Password_does_not_match__Type_again_")]        
        public string ConfirmPassword { get; set; }

        [DisplayName("Enter the Email you used to register your Self-Service Account")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ForgotPassowrdViewModel_EmailAddress_E_mail_address_is_required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ForgotPassowrdViewModel_EmailAddress_Email_address_format_is_not_valid")]
        public string EmailAddress { get; set; }

        [DisplayName("you have {0} attempts left")]
        public int AttemptsLeft { get; set; }

        public int CurrentAttempt { get; set; }

        public int WssAccountId { get; set; }

        public string Actiontoken { get; set; }
        public string SecurityQuestion { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ForgotPassowrdViewModel_SecurityAnswer_Answer_to_security_question_is_required")]
        [RegularExpression(@"[\w*\s*'*@*-*,*\.*]{1,250}", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ForgotPassowrdViewModel_SecurityAnswer_Answer_is_incorrect")]
        public string SecurityAnswer { get; set; }
        public bool IsForgotPasswordMailSent { get; set; }

    }
}