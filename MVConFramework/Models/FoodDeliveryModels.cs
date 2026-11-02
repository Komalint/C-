using System;
using System.Collections.Generic;

namespace MVConFramework.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public int StoreId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } = "Main Course";
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsVeg { get; set; } = true;
        public bool IsAvailable { get; set; } = true;
        public int PrepTimeMinutes { get; set; } = 15;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class Review
    {
        public int ReviewId { get; set; }
        public int StoreId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? OrderId { get; set; }
        public int Rating { get; set; } = 5;
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public string StoreResponse { get; set; }
        public DateTime? ResponseDate { get; set; }
    }

    public class DeliveryAgent
    {
        public int AgentId { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AadhaarNumber { get; set; }
        public bool IsAadhaarVerified { get; set; } = true;
        public string VehicleType { get; set; } = "Motorcycle";
        public string VehicleNumber { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; } = true;
        public int TotalDeliveries { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal Rating { get; set; } = 4.8m;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class FoodSafetyVerification
    {
        public int VerificationId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string FssaiLicense { get; set; }
        public int HygieneScore { get; set; } = 90;
        public string PestControlCertNo { get; set; }
        public string WaterSafetyCertNo { get; set; }
        public string FireSafetyNoc { get; set; }
        public string AuditRemarks { get; set; }
        public int? VerifiedByAdminId { get; set; }
        public DateTime VerificationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Approved";
    }

    public class RestaurantCertificate
    {
        public int CertificateId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string OwnerName { get; set; }
        public string City { get; set; }
        public string FssaiLicenseNo { get; set; }
        public int HygieneScore { get; set; }
        public string CertificateNumber { get; set; }
        public DateTime IssuedDate { get; set; } = DateTime.Now;
        public int TotalOrdersAtIssue { get; set; }
        public decimal AverageRatingAtIssue { get; set; }
        public string CertificateTitle { get; set; } = "Certificate of Food Quality, Safety & Customer Trust";
        public bool IsValid { get; set; } = true;
        public int CurrentOrdersCompleted { get; set; }
        public decimal CurrentRating { get; set; }
    }

    public class AppNotification
    {
        public int NotificationId { get; set; }
        public string RecipientRole { get; set; }
        public int? RecipientUserId { get; set; }
        public int? StoreId { get; set; }
        public int? AgentId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotificationType { get; set; } = "OrderUpdate";
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class CartItem
    {
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsVeg { get; set; }
        public decimal Subtotal => Price * Quantity;
    }

    public class DatabaseHealthStatus
    {
        public string AssignmentDbStatus { get; set; }
        public int AssignmentDbStoresCount { get; set; }
        public int AssignmentDbOrdersCount { get; set; }
        public int AssignmentDbAgentsCount { get; set; }
        public string PracticeDbStatus { get; set; }
        public int PracticeDbUsersCount { get; set; }
        public int PracticeDbCoursesCount { get; set; }
        public string Message { get; set; }
    }
}
