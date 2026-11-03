using System.ComponentModel.DataAnnotations;

namespace MVConFramework.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Account role is required")]
        [Display(Name = "Register As")]
        public string Role { get; set; } = "Customer"; // "Customer", "DeliveryAgent", "Restaurant"

        // Common Account Credentials
        [Required(ErrorMessage = "Full Name or Username is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
        [Display(Name = "Full Name / Username")]
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

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State")]
        [StringLength(50)]
        public string State { get; set; }

        // --- Rider / Delivery Agent Specific Fields ---
        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; } = "Motorcycle"; // Motorcycle, Scooter, Electric Vehicle, Bicycle

        [Display(Name = "Vehicle Registration Number")]
        public string VehicleNumber { get; set; }

        [Display(Name = "Aadhaar / National ID")]
        public string AadhaarNumber { get; set; }

        // --- Restaurant Partner Specific Fields ---
        [Display(Name = "Restaurant / Kitchen Name")]
        public string RestaurantName { get; set; }

        [Display(Name = "Kitchen / Store Type")]
        public string StoreType { get; set; } = "Restaurant"; // Restaurant, Cloud Kitchen, Bakery, Cafe, Sweet Shop

        [Display(Name = "Cuisine Category")]
        public string Cuisine { get; set; } = "Multi-Cuisine";

        [Display(Name = "Physical Kitchen Address")]
        public string RestaurantAddress { get; set; }

        [Display(Name = "FSSAI Food Safety License Number (Optional or Pending)")]
        public string FssaiLicenseNo { get; set; }
    }
}
