using System.ComponentModel.DataAnnotations;

namespace FilmLog.Models
{
    // This class represents the data needed for a user to log in to the system.
    public class LoginViewModel
    {
        // The email address the user will use to log in. This field is required and must be a valid email format.
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // The password the user will use to log in. This field is required and should be treated as a password (hidden input).
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // A flag indicating whether the user wants to be remembered (stay logged in) on the site.
        public bool RememberMe { get; set; }
    }
}
