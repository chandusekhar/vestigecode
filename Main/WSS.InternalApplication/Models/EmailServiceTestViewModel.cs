using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSS.InternalApplication.Models
{
    public class EmailServiceTestViewModel
    {
        [DisplayName("Email Address : ")]
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email address")]
        public string EmailAddress { get; set; }
    }
}