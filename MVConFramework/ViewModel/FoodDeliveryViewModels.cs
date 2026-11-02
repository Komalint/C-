using System;
using System.Collections.Generic;
using MVConFramework.Models;

namespace MVConFramework.ViewModels
{
    public class CustomerBrowseViewModel
    {
        public string SelectedState { get; set; }
        public string SelectedCity { get; set; }
        public string SearchTerm { get; set; }
        public string UserState { get; set; }
        public List<string> AvailableStates { get; set; } = new List<string> { "Delhi", "Gujarat", "Maharashtra", "Rajasthan", "Uttar Pradesh" };
        public List<string> AvailableCities { get; set; } = new List<string> { "Delhi", "Ahmedabad", "Surat", "Vadodara", "Rajkot", "Mumbai", "Pune", "Jaipur", "Udaipur", "Jodhpur", "Lucknow", "Noida" };
        public List<Stores> StoresList { get; set; } = new List<Stores>();
    }

    public class RestaurantMenuViewModel
    {
        public Stores Store { get; set; }
        public string CustomerState { get; set; }
        public bool IsInterStateRestricted => !string.IsNullOrEmpty(CustomerState) && Store != null && !string.IsNullOrEmpty(Store.State) && !CustomerState.Equals(Store.State, StringComparison.OrdinalIgnoreCase);
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<string> Categories { get; set; } = new List<string>();
        public RestaurantCertificate Certificate { get; set; }
    }

    public class CheckoutViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreState { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public bool IsInterStateRestricted => !string.IsNullOrEmpty(DeliveryState) && !string.IsNullOrEmpty(StoreState) && !DeliveryState.Equals(StoreState, StringComparison.OrdinalIgnoreCase);
        public string PaymentMethod { get; set; } = "UPI"; // UPI, Card, QR, COD
        public string UpiId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string CardExpiry { get; set; }
        public string CardCvv { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal ItemTotal { get; set; }
        public decimal TaxAmount { get; set; } // 5% GST
        public decimal DeliveryFee { get; set; } // ₹40
        public decimal TotalAmount { get; set; }
        public int TotalItemCount { get; set; }
        public int PreparationTimeMinutes { get; set; }
        public string ItemsSummary { get; set; }
    }

    public class OrderReceiptViewModel
    {
        public int OrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StoreState { get; set; }
        public string FssaiLicenseNo { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PlatformFee { get; set; } // 7% fee
        public string PaymentMethod { get; set; }
        public string OrderStatus { get; set; }
        public string RejectionMessage { get; set; }
        public int PreparationTimeMinutes { get; set; }
        public int EstimatedDeliveryMinutes { get; set; }
        public decimal DistanceKm { get; set; }
        public string AssignedAgentName { get; set; }
        public string AssignedAgentPhone { get; set; }
    }

    public class OrderTrackingViewModel
    {
        public int OrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public string OrderStatus { get; set; }
        public int PreparationTimeMinutes { get; set; }
        public int EstimatedDeliveryMinutes { get; set; }
        public decimal DistanceKm { get; set; }
        public string AssignedAgentName { get; set; }
        public string AssignedAgentPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProductName { get; set; }
        public int ElapsedMinutes { get; set; }
        public int RemainingPrepMinutes { get; set; }
    }

    public class RestaurantDashboardViewModel
    {
        public Stores Store { get; set; }
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int CompletedOrders { get; set; }
        public decimal GrossSales { get; set; }
        public decimal PlatformFeeDeducted { get; set; } // 7% to admin
        public decimal NetPayout { get; set; } // 93% net payout
        public decimal AverageRating { get; set; }
        public List<Orders> LiveOrders { get; set; } = new List<Orders>();
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public RestaurantCertificate Certificate { get; set; }
        public bool IsCertificateEligible => CompletedOrders >= 200 && AverageRating >= 3.5m;
    }

    public class AdminFoodDeliveryViewModel
    {
        public string SelectedState { get; set; }
        public string SelectedCity { get; set; }
        public List<string> AvailableStates { get; set; } = new List<string> { "All States", "Delhi", "Gujarat", "Maharashtra", "Rajasthan", "Uttar Pradesh" };
        public int TotalRestaurants { get; set; }
        public int VerifiedRestaurants { get; set; }
        public int PendingVerificationsCount { get; set; }
        public decimal TotalGrossSales { get; set; }
        public decimal TotalPlatformFeeRevenue { get; set; } // 7% commission
        public int TotalOrdersCount { get; set; }
        public decimal OverallAverageRating { get; set; }
        public int ActiveDeliveryAgentsCount { get; set; }
        public List<CityMetricRow> CityMetrics { get; set; } = new List<CityMetricRow>();
        public List<Stores> PendingKitchens { get; set; } = new List<Stores>();
        public List<Stores> AllRestaurants { get; set; } = new List<Stores>();
        public List<DeliveryAgent> DeliveryAgents { get; set; } = new List<DeliveryAgent>();
        public DatabaseHealthStatus DbHealth { get; set; } = new DatabaseHealthStatus();
    }

    public class CityMetricRow
    {
        public string State { get; set; }
        public string City { get; set; }
        public int RestaurantCount { get; set; }
        public int VerifiedCount { get; set; }
        public int OrdersCount { get; set; }
        public decimal CityGrossSales { get; set; }
        public decimal CityPlatformFee { get; set; } // 7%
        public decimal AvgCityRating { get; set; }
        public int AvgHygieneScore { get; set; }
    }

    public class DeliveryAgentDashboardViewModel
    {
        public DeliveryAgent Agent { get; set; }
        public List<Orders> ActiveOrders { get; set; } = new List<Orders>();
        public List<Orders> CompletedDeliveries { get; set; } = new List<Orders>();
        public decimal TodayEarnings { get; set; }
        public decimal LifetimeEarnings { get; set; }
        public int TotalCompletedDeliveries { get; set; }
    }
}
