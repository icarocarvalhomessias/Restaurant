using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Restaurant.WebApi.Core.Data
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        public TypeUser TypeUser { get; set; }
    }

    public enum TypeUser
    {
        Admin = 1,
        DeliveryMan
    }
}
