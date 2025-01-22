using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApi.Core.Data
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Field {0} required")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field {0} required")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        public UserLogin(string email, string password)
        {
            Email = email;
            Password = password;
        }

    }
}
