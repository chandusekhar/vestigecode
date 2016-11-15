using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WSS.CustomerApplication.Properties;

namespace WSS.CustomerApplication.Models
{
    public class AccountViewModel
    {
        public enum UnsubscribeReason
        {
            NoLongerResponsible,
            UnhappyWithService,
            PreferNotToSay,
            Other
        }

        [ScaffoldColumn(false)]
        public int WssAccountId { get; set; }

        [ScaffoldColumn(false)]
        public int DefaultUtilityAccountId { get; set; }

        [DisplayName("Primary Email *")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_PrimaryEmailAddress_A_Primary_Email_Address_is_Required_")]
        [StringLength(100)]
        public string PrimaryEmailAddress { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_ChangeEmailAddress1_Email_Address_is_required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_ChangeEmailAddress1_Invalid_Email_Address")]
        public string ChangeEmailAddress1 { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_ChangeEmailAddress1_Email_Address_is_required")]
        [Compare("EmailAddress", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_ChangeEmailAddress2_Confirm_Email_Address_does_not_match__Type_again__")]
        public string ChangeEmailAddress2 { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_currentPassword_Current_Password_is_required")]
        private string currentPassword { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_newPassword1_New_Password_is_required")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-zA-Z]).{7,15}",ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_newPassword1_Invalid_Password")]
        [DataType(DataType.Password)]
        public string newPassword1 { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_newPassword2_Confirm_Password_is_required")]
        [Compare("newPassword1", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_newPassword2_Confirm_Password_does_not_match__Type_again__")]
        [DataType(DataType.Password)]
        public string newPassword2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_SecurityQuestion_Security_question_is_required")]
        [RegularExpression(@"[\w*\s*'*@*-*\?*!*,*\.*]{1,250}", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_SecurityQuestion_Question_is_incorrect")]
        public string SecurityQuestion { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_SecurityQuestionAnswer_Answer_to_security_question_is_required")]
        [RegularExpression(@"[\w*\s*'*@*-*\?*!*,*\.*]{1,250}", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_SecurityQuestionAnswer_Letters_and_numbers_only__less_then_250_characters")]
        public string SecurityQuestionAnswer { get; set; }

        [ScaffoldColumn(false)]
        public int StatusId { get; set; }

        [DisplayName("Account Status")]
        public string StatusDescription { get; set; }

        [DisplayName("Customer")]
        public string PrimaryAccountHolderName { get; set; }

        [DisplayName("Resend Attempts")]
        public int ResendAttempts { get; set; }

        [DisplayName("Terms and Condistions Acceptance Date")]
        public string TermsDate { get; set; }

        [DisplayName("Account Balance")]
        public decimal AccountBalance { get; set; }

        public string CcbAccountNumber { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowRegisterAccount { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowChangeEmail { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowResendActivation { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowResetPassword { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowUnlockAccount { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowRestoreAccount { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowUnSubscribe { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowLockAccount { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowAdditionalEmail { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowSecuritySettings { get; set; }

        [DisplayName("Why are you closing your account?")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_UnsubscribeReasonSelected_A_reason_is_required")]
        public UnsubscribeReason UnsubscribeReasonSelected { get; set; }

        [RegularExpression(@"^[\w\s\'@$()_{},.?:;-]*$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_UnsubscribeReasonOtherText_Invalid_input_into_reason_field__Use_alphanumeric_input_only")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_UnsubscribeReasonOtherText_Specify_reason_length_is_up_to_100_characters")]
        public string UnsubscribeReasonOtherText { get; set; }

        [RegularExpression(@"^[\w\s\'@$()_{},.?:;-]*$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_UnsubscribeComments_Invalid_input_into_comment_field__Use_alphanumeric_input_only")]
        [DisplayName("Leave a comment (optional):")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AccountViewModel_UnsubscribeComments_Comment_length_should_be_less_than_250_characters")]
        public string UnsubscribeComments { get; set; }

    }

}