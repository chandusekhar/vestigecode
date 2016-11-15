using System.ComponentModel;

namespace WSS.InternalApplication.Models
{
    public class ManageUtilityAccountListViewModel
    {
        public int id { get; set; }

        [DisplayName("NickName")]
        public string NickName { get; set; }

        [DisplayName("Is my name on the UtilityAccount?")]
        public string NameOnAccount { get; set; }

        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }

        [DisplayName(" is my name on the utility Bill account?")]
        public string nameOnUtilityAccount { get; set; }

        [DisplayName(" Edit Nickname for Account:")]
        public string EditNickname { get; set; }

        [DisplayName(" Edit ")]
        public string edit { get; set; }

        [DisplayName("Default")]
        public bool DefaultAccount { get; set; }
        public bool IsActive { get; set; }
    }
}