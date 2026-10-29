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
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Enter valid 10 digit phone number")]
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

        public bool IsActive { get; set; }


        public List<Orders> Orders { get; set; }
    }
}