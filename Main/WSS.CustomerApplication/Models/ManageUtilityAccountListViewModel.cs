using System.ComponentModel;

namespace WSS.CustomerApplication.Models
{
    public class ManageUtilityAccountListViewModel
    {
       // public int LinkedUtilityAccountId { get; set; }
        public int UtilityAccountId { get; set; }

        [DisplayName("Nickname")]
        public string NickName { get; set; }

        [DisplayName("Is my name on the UtilityAccount?")]
        public string NameOnAccount { get; set; }

        [DisplayName("Account Number")]
        public string UtilityAccountNumber { get; set; }

        [DisplayName(" is my name on the utility Bill account?")]
        public string nameOnUtilityAccount { get; set; }

        [DisplayName(" Edit Nickname for Account:")]
        public string EditNickname { get; set; }

        public bool DefaultAccount { get; set; }
        public bool IsActive { get; set; }
    }
}