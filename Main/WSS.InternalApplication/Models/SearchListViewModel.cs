using System.ComponentModel;

namespace WSS.InternalApplication.Models
{
    public class SearchListViewModel
    {
        public int id { get; set; }

        [DisplayName("Utility Account Number")]
        public string ccbAccountNumber { get; set; }

        [DisplayName("Main Customer Name")]
        public string MainCustomerName { get; set; }

        [DisplayName("Account Status")]
        public string Status { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
    }
}