using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSS.InternalApplication.Models
{
    public class RegisterViewModel
    {
        [DisplayName("Account Number:")]
        public string CcbAccountNumber { get; set; }

        [DisplayName("E-mail Address : ")]
        [Required(ErrorMessage = "Please enter e-mail address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email address")]
        public string EmailAddress { get; set; }

        [DisplayName("Customer Name : ")]
        [Required(ErrorMessage = "Please enter temporary customer name")]
        public string CustomerName { get; set; }

        public string NewOrExistingCcdAccount { get; set; }

    }
}