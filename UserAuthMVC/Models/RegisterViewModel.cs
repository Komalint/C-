using System.ComponentModel.DataAnnotations;

namespace UserAuthMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full Name / Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [Display(Name = "Full Name / Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        [Display(Name = "State")]
        public string State { get; set; }
    }
}
