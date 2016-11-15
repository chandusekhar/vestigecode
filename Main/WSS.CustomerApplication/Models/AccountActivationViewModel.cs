using System.ComponentModel.DataAnnotations;
using WSS.CustomerApplication.Properties;


namespace WSS.CustomerApplication.Models
{
    public class AccountActivationViewModel
    {
        public int WssAccountId { get; set; }

        public string AdditionalEmailAddressId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountActivationViewModel_Password1_A_Password_is_required")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-zA-Z]).{7,15}", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountActivationViewModel_Password1_Password_Has_to_be_7_to_15_characters_long_and_contain_at_least_one_letter_and_one_digit")]
        [DataType(DataType.Password)]
        public string Password1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountActivationViewModel_Password2_Confirm_Password_is_required")]
        [DataType(DataType.Password)]
        [Compare("Password1", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountActivationViewModel_Password2_Confirm_Password_does_not_match__Type_again_")]
        public string Password2 { get; set; }

        public string Token { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountActivationViewModel_SecurityQuestion_Security_question_is_required")]
        [RegularExpression(@"[\w*\s*'*@*-*\?*!*,*\.*]{1,250}", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountActivationViewModel_SecurityQuestion_Question_is_incorrect")]
        public string SecurityQuestion { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountActivationViewModel_SecurityQuestionAnswer_Answer_to_security_question_is_required")]
        [RegularExpression(@"[\w*\s*'*@*-*,*\.*]{1,250}", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountActivationViewModel_SecurityQuestionAnswer_Answer_is_incorrect")]
        public string SecurityQuestionAnswer { get; set; }

        public bool AgreeToTermsAndConditions { get; set; }

 }

}