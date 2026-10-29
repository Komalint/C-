using System.ComponentModel.DataAnnotations;

namespace MVConFramework.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string State { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; } = "User";
    }
}
