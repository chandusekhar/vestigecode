using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSS.InternalApplication.Models
{
    public class ChangeEmailAddressViewModel
    {
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Change Email Address")]
        public string ChangeEmailAddress1 { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [Compare("EmailAddress", ErrorMessage = "Confirm Email Address does not match, Type again !")]
        [DisplayName("Re-Enter Email Address")]
        public string ChangeEmailAddress2 { get; set; }

        public int id { get; set; }
    }
}