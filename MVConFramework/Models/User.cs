using System;
using System.ComponentModel.DataAnnotations;

namespace MVConFramework.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
