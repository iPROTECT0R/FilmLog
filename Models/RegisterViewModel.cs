using System.ComponentModel.DataAnnotations;

namespace FilmLog.Models
{
    // This class represents the data needed for a user to register a new account.
    public class RegisterViewModel
    {
        // The email address the user will use to register. This field is required and must be a valid email format.
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // The password the user will use to create their account. It must be at least 6 characters long, and the field is required.
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // The confirmation password must match the password entered above. It's required and must be a valid password format.
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
