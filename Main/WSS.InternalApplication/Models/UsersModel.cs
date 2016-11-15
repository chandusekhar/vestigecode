using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSS.InternalApplication.Models
{
    public class UsersModel
    {
        [DisplayName("Username*")]
        [Required(ErrorMessage = "User ID cannot be empty")]
        public string Username { get; set; }

        [DisplayName("Password*")]
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}