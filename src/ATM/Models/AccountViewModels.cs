using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "PIN")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(4, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "PIN")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm pin")]
        [Compare("Password", ErrorMessage = "The pin and confirmation pin do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(4, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "PIN")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm pin")]
        [Compare("Password", ErrorMessage = "The pin and confirmation pin do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}