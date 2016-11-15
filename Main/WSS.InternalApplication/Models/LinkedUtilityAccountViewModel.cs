using System.ComponentModel;

namespace WSS.InternalApplication.Models
{
    public class LinkedUtilityAccountViewModel
    {
        [DisplayName("Search for 10 digits UtilityAccount Number")]
        public string SearchUtilityAccountNumber { get; set; }

        [DisplayName("Utility Account")]
        public string CcbAccountNumber { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        public int UtilityAccountNumber { get; set; }
    }
}