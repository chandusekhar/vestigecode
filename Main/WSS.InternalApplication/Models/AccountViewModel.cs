using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSS.InternalApplication.Models
{
    /// <summary>
    ///     Used to display the WSS Account (Customer Account)
    /// </summary>
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
        public int UtilityAccountId { get; set; }

        [DisplayName("Primary Email *")]
        [Required(ErrorMessage = "A Primary Email Address is Required.")]
        [StringLength(100)]
        public string PrimaryEmailAddress { get; set; }

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

        [DisplayName("Why are you closing your account?")]
        public UnsubscribeReason UnsubscribeReasonSelected { get; set; }

        public string UnsubscribeReasonOtherText { get; set; }

        [DisplayName("Leave a comment (optional):")]
        public string UnsubscribeComments { get; set; }

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
    }
}