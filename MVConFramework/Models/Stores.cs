using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVConFramework.Models
{
    public class Stores
    {
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Store Name is required")]
        [StringLength(100)]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Owner Name is required")]
        [StringLength(100)]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Store Type is required")]
        public string StoreType { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter valid 10 digit phone number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Opening Date is required")]
        [DataType(DataType.Date)]
        public DateTime OpeningDate { get; set; }

        public bool IsActive { get; set; } = true;

        public List<Orders> Orders { get; set; }

        // Food Delivery & Verification Extensions
        public decimal Rating { get; set; } = 4.2m;
        public int TotalOrdersCompleted { get; set; } = 0;
        public bool IsVerified { get; set; } = true;
        public string VerificationStatus { get; set; } = "Approved"; // Pending, Approved, Rejected
        public string FssaiLicenseNo { get; set; }
        public int HygieneScore { get; set; } = 90;
        public bool WaterQualityCertified { get; set; } = true;
        public bool PestControlCertified { get; set; } = true;
        public DateTime? KitchenAuditDate { get; set; }
        public string Address { get; set; }
        public string Cuisine { get; set; } = "Multi-Cuisine";
        public string ImageUrl { get; set; }
        public string BannerUrl { get; set; }
        public string OpeningTime { get; set; } = "10:00 AM";
        public string ClosingTime { get; set; } = "11:00 PM";
        public string Description { get; set; }
        public int? UserId { get; set; }
        public int MenuItemCount { get; set; }
        public int ReviewCount { get; set; }
    }
}
