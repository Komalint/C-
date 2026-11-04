using System;
using System.ComponentModel.DataAnnotations;

namespace MVConFramework.Models
{
    public class Orders
    {
        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Store Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Store Id must be greater than 0")]
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Store Name is required")]
        [StringLength(100, ErrorMessage = "Store Name cannot exceed 100 characters")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(100, ErrorMessage = "Customer Name cannot exceed 100 characters")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, ErrorMessage = "Product Name cannot exceed 100 characters")]
        public string ProductName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        [Range(1, 1000000, ErrorMessage = "Total Amount must be greater than 0")]
        public int TotalAmount { get; set; }

        [Required(ErrorMessage = "Payment Method is required")]
        [StringLength(50, ErrorMessage = "Payment Method cannot exceed 50 characters")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Delivery City is required")]
        [StringLength(100, ErrorMessage = "Delivery City cannot exceed 100 characters")]
        public string DeliveryCity { get; set; }

        // Food Delivery Extensions
        public string OrderStatus { get; set; } = "Delivered"; // Placed, Preparing, ReadyForPickup, OutForDelivery, Delivered, Cancelled
        public int PreparationTimeMinutes { get; set; } = 15;
        public int? AssignedAgentId { get; set; }
        public string AssignedAgentName { get; set; }
        public string AssignedAgentPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public int EstimatedDeliveryMinutes { get; set; } = 45;
        public decimal DistanceKm { get; set; } = 3.5m;
        public string InvoiceNumber { get; set; }
        public decimal PlatformFee { get; set; } = 0.00m;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; }
        public decimal DeliveryEarnings { get; set; } = 60.00m;

        // Delivery Verification OTP System
        public string DeliveryOtp { get; set; }
        public bool IsOtpVerified { get; set; }
        public DateTime? OtpGeneratedAt { get; set; }
        public DateTime? OtpVerifiedAt { get; set; }

        // Contact, Items Details & Cancellation
        public string CustomerPhone { get; set; }
        public string ItemsSummary { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string CancellationReason { get; set; }
        public int CancellationGraceSecondsRemaining { get; set; }
    }
}
