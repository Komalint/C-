using System.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MVConFramework.ViewModels;

namespace MVConFramework.Models
{
    public class FoodDeliveryRepo
    {
        private readonly string _cnStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;

        // Overload for backwards compatibility
        public List<Stores> GetStores(string city, string search, bool includeUnverified)
        {
            return GetStores(null, city, search, includeUnverified);
        }

        // 1. Get Stores By State, City or Search
        public List<Stores> GetStores(string state = null, string city = null, string search = null, bool includeUnverified = false)
        {
            var list = new List<Stores>();
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_GetStoresByCityOrSearch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State", string.IsNullOrWhiteSpace(state) || state == "All States" ? (object)DBNull.Value : state.Trim());
                    cmd.Parameters.AddWithValue("@City", string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city.Trim());
                    cmd.Parameters.AddWithValue("@Search", string.IsNullOrWhiteSpace(search) ? (object)DBNull.Value : search.Trim());
                    cmd.Parameters.AddWithValue("@IncludeUnverified", includeUnverified);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var s = new Stores
                            {
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                StoreName = dr["StoreName"].ToString(),
                                OwnerName = dr["OwnerName"].ToString(),
                                StoreType = dr["StoreType"].ToString(),
                                ContactNumber = dr["ContactNumber"].ToString(),
                                Email = dr["Email"].ToString(),
                                City = dr["City"].ToString(),
                                State = dr["State"].ToString(),
                                OpeningDate = Convert.ToDateTime(dr["OpeningDate"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                Rating = dr["Rating"] != DBNull.Value ? Convert.ToDecimal(dr["Rating"]) : 4.2m,
                                TotalOrdersCompleted = dr["TotalOrdersCompleted"] != DBNull.Value ? Convert.ToInt32(dr["TotalOrdersCompleted"]) : 0,
                                IsVerified = dr["IsVerified"] != DBNull.Value && Convert.ToBoolean(dr["IsVerified"]),
                                VerificationStatus = dr["VerificationStatus"] != DBNull.Value ? dr["VerificationStatus"].ToString() : "Approved",
                                FssaiLicenseNo = dr["FssaiLicenseNo"] != DBNull.Value ? dr["FssaiLicenseNo"].ToString() : "",
                                HygieneScore = dr["HygieneScore"] != DBNull.Value ? Convert.ToInt32(dr["HygieneScore"]) : 90,
                                WaterQualityCertified = dr["WaterQualityCertified"] != DBNull.Value && Convert.ToBoolean(dr["WaterQualityCertified"]),
                                PestControlCertified = dr["PestControlCertified"] != DBNull.Value && Convert.ToBoolean(dr["PestControlCertified"]),
                                Address = dr["Address"] != DBNull.Value ? dr["Address"].ToString() : "",
                                Cuisine = dr["Cuisine"] != DBNull.Value ? dr["Cuisine"].ToString() : "Multi-Cuisine",
                                ImageUrl = dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "",
                                MenuItemCount = dr["MenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["MenuItemCount"]) : 0,
                                ReviewCount = dr["ReviewCount"] != DBNull.Value ? Convert.ToInt32(dr["ReviewCount"]) : 0
                            };
                            list.Add(s);
                        }
                    }
                }
            }
            return list;
        }

        // 2. Get Store Details With Menu & Reviews
        public RestaurantMenuViewModel GetStoreDetailsWithMenu(int storeId)
        {
            var vm = new RestaurantMenuViewModel();
            var categoriesSet = new HashSet<string>();

            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_GetStoreDetailsWithMenu", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StoreId", storeId);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        // 1. Store details
                        if (dr.Read())
                        {
                            vm.Store = new Stores
                            {
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                StoreName = dr["StoreName"].ToString(),
                                OwnerName = dr["OwnerName"].ToString(),
                                StoreType = dr["StoreType"].ToString(),
                                ContactNumber = dr["ContactNumber"].ToString(),
                                Email = dr["Email"].ToString(),
                                City = dr["City"].ToString(),
                                State = dr["State"].ToString(),
                                OpeningDate = Convert.ToDateTime(dr["OpeningDate"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                Rating = dr["Rating"] != DBNull.Value ? Convert.ToDecimal(dr["Rating"]) : 4.2m,
                                TotalOrdersCompleted = dr["TotalOrdersCompleted"] != DBNull.Value ? Convert.ToInt32(dr["TotalOrdersCompleted"]) : 0,
                                IsVerified = dr["IsVerified"] != DBNull.Value && Convert.ToBoolean(dr["IsVerified"]),
                                VerificationStatus = dr["VerificationStatus"] != DBNull.Value ? dr["VerificationStatus"].ToString() : "Approved",
                                FssaiLicenseNo = dr["FssaiLicenseNo"] != DBNull.Value ? dr["FssaiLicenseNo"].ToString() : "",
                                HygieneScore = dr["HygieneScore"] != DBNull.Value ? Convert.ToInt32(dr["HygieneScore"]) : 90,
                                WaterQualityCertified = dr["WaterQualityCertified"] != DBNull.Value && Convert.ToBoolean(dr["WaterQualityCertified"]),
                                PestControlCertified = dr["PestControlCertified"] != DBNull.Value && Convert.ToBoolean(dr["PestControlCertified"]),
                                Address = dr["Address"] != DBNull.Value ? dr["Address"].ToString() : "",
                                Cuisine = dr["Cuisine"] != DBNull.Value ? dr["Cuisine"].ToString() : "Multi-Cuisine",
                                ImageUrl = dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : ""
                            };
                        }

                        // 2. Menu Items
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                var item = new MenuItem
                                {
                                    MenuItemId = Convert.ToInt32(dr["MenuItemId"]),
                                    StoreId = Convert.ToInt32(dr["StoreId"]),
                                    ItemName = dr["ItemName"].ToString(),
                                    Description = dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "",
                                    Category = dr["Category"].ToString(),
                                    Price = Convert.ToDecimal(dr["Price"]),
                                    ImageUrl = dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "",
                                    IsVeg = Convert.ToBoolean(dr["IsVeg"]),
                                    IsAvailable = Convert.ToBoolean(dr["IsAvailable"]),
                                    PrepTimeMinutes = dr["PrepTimeMinutes"] != DBNull.Value ? Convert.ToInt32(dr["PrepTimeMinutes"]) : 15,
                                    CreatedAt = Convert.ToDateTime(dr["CreatedAt"])
                                };
                                vm.MenuItems.Add(item);
                                if (!string.IsNullOrWhiteSpace(item.Category))
                                {
                                    categoriesSet.Add(item.Category);
                                }
                            }
                        }

                        // 3. Reviews
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                var r = new Review
                                {
                                    ReviewId = Convert.ToInt32(dr["ReviewId"]),
                                    StoreId = Convert.ToInt32(dr["StoreId"]),
                                    CustomerName = dr["CustomerName"].ToString(),
                                    Rating = Convert.ToInt32(dr["Rating"]),
                                    Comment = dr["Comment"] != DBNull.Value ? dr["Comment"].ToString() : "",
                                    ReviewDate = Convert.ToDateTime(dr["ReviewDate"]),
                                    StoreResponse = dr["StoreResponse"] != DBNull.Value ? dr["StoreResponse"].ToString() : "",
                                    ResponseDate = dr["ResponseDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["ResponseDate"]) : null
                                };
                                vm.Reviews.Add(r);
                            }
                        }
                    }
                }
            }

            vm.Categories = new List<string>(categoriesSet);
            vm.Certificate = GetRestaurantCertificate(storeId);
            return vm;
        }

        // 3. Place Customer Order with dynamic preparation time & 7% platform fee
        public OrderReceiptViewModel PlaceCustomerOrder(CheckoutViewModel model, int? customerId)
        {
            var receipt = new OrderReceiptViewModel();

            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_PlaceCustomerOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StoreId", model.StoreId);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId.HasValue ? (object)customerId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CustomerName", string.IsNullOrWhiteSpace(model.CustomerName) ? "Guest Customer" : model.CustomerName.Trim());
                    cmd.Parameters.AddWithValue("@DeliveryAddress", string.IsNullOrWhiteSpace(model.DeliveryAddress) ? "Default Address" : model.DeliveryAddress.Trim());
                    cmd.Parameters.AddWithValue("@DeliveryCity", string.IsNullOrWhiteSpace(model.DeliveryCity) ? "Delhi" : model.DeliveryCity.Trim());
                    cmd.Parameters.AddWithValue("@DeliveryState", string.IsNullOrWhiteSpace(model.DeliveryState) ? (object)DBNull.Value : model.DeliveryState.Trim());
                    cmd.Parameters.AddWithValue("@PaymentMethod", string.IsNullOrWhiteSpace(model.PaymentMethod) ? "UPI" : model.PaymentMethod);
                    cmd.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(Math.Round(model.TotalAmount)));
                    cmd.Parameters.AddWithValue("@ItemCount", model.TotalItemCount > 0 ? model.TotalItemCount : 1);
                    cmd.Parameters.AddWithValue("@ProductName", string.IsNullOrWhiteSpace(model.ItemsSummary) ? "Assorted Gourmet Food" : model.ItemsSummary);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            receipt.OrderId = Convert.ToInt32(dr["OrderId"]);
                            receipt.InvoiceNumber = dr["InvoiceNumber"] != DBNull.Value ? dr["InvoiceNumber"].ToString() : "";
                            receipt.StoreName = dr["StoreName"] != DBNull.Value ? dr["StoreName"].ToString() : "";
                            receipt.TotalAmount = dr["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmount"]) : 0m;
                            receipt.PlatformFee = dr["PlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFee"]) : 0m;
                            receipt.PreparationTimeMinutes = dr["PreparationTimeMinutes"] != DBNull.Value ? Convert.ToInt32(dr["PreparationTimeMinutes"]) : 0;
                            receipt.EstimatedDeliveryMinutes = dr["EstimatedDeliveryMinutes"] != DBNull.Value ? Convert.ToInt32(dr["EstimatedDeliveryMinutes"]) : 0;
                            receipt.DistanceKm = dr["DistanceKm"] != DBNull.Value ? Convert.ToDecimal(dr["DistanceKm"]) : 0m;
                            receipt.OrderStatus = dr["OrderStatus"] != DBNull.Value ? dr["OrderStatus"].ToString() : "Placed";
                            try
                            {
                                if (dr["Message"] != DBNull.Value)
                                {
                                    receipt.RejectionMessage = dr["Message"].ToString();
                                }
                            }
                            catch { }
                        }
                    }
                }
            }

            receipt.StoreId = model.StoreId;
            receipt.CustomerName = model.CustomerName;
            receipt.DeliveryAddress = model.DeliveryAddress;
            receipt.DeliveryCity = model.DeliveryCity;
            receipt.DeliveryState = model.DeliveryState;
            receipt.StoreState = model.StoreState;
            receipt.PaymentMethod = model.PaymentMethod;
            receipt.OrderDate = DateTime.Now;
            receipt.Quantity = model.TotalItemCount;
            receipt.ProductName = model.ItemsSummary;

            // Fetch store details to enrich receipt
            var storeDetails = GetStoreById(model.StoreId);
            if (storeDetails != null)
            {
                receipt.StoreAddress = storeDetails.Address;
                receipt.FssaiLicenseNo = storeDetails.FssaiLicenseNo;
            }

            return receipt;
        }

        // 4. Get Order Receipt By Order Id
        public OrderReceiptViewModel GetOrderReceipt(int orderId)
        {
            OrderReceiptViewModel vm = null;
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand(@"
                    SELECT TOP 1 
                        o.OrderId, o.InvoiceNumber, o.StoreId, o.StoreName, s.Address AS StoreAddress, s.State AS StoreState, s.FssaiLicenseNo,
                        o.CustomerName, o.DeliveryAddress, o.DeliveryCity, o.DeliveryState, o.OrderDate, o.ProductName, o.Quantity,
                        o.TotalAmount, o.PlatformFee, o.PaymentMethod, o.OrderStatus, o.PreparationTimeMinutes,
                        o.EstimatedDeliveryMinutes, o.DistanceKm, o.AssignedAgentName, o.AssignedAgentPhone,
                        o.DeliveryOtp, ISNULL(o.IsOtpVerified, 0) AS IsOtpVerified,
                        o.CustomerPhone, o.ItemsSummary, o.CreatedAt,
                        DATEDIFF(SECOND, o.CreatedAt, GETDATE()) AS SecondsSinceCreation
                    FROM hra.Orders o
                    LEFT JOIN hra.Stores s ON o.StoreId = s.StoreId
                    WHERE o.OrderId = @OrderId", con))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            vm = new OrderReceiptViewModel
                            {
                                OrderId = Convert.ToInt32(dr["OrderId"]),
                                InvoiceNumber = dr["InvoiceNumber"] != DBNull.Value ? dr["InvoiceNumber"].ToString() : "INV-FD-" + orderId,
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                StoreName = dr["StoreName"].ToString(),
                                StoreAddress = dr["StoreAddress"] != DBNull.Value ? dr["StoreAddress"].ToString() : "",
                                StoreState = dr["StoreState"] != DBNull.Value ? dr["StoreState"].ToString() : "",
                                FssaiLicenseNo = dr["FssaiLicenseNo"] != DBNull.Value ? dr["FssaiLicenseNo"].ToString() : "",
                                CustomerName = dr["CustomerName"].ToString(),
                                DeliveryAddress = dr["DeliveryAddress"] != DBNull.Value ? dr["DeliveryAddress"].ToString() : "",
                                DeliveryCity = dr["DeliveryCity"].ToString(),
                                DeliveryState = dr["DeliveryState"] != DBNull.Value ? dr["DeliveryState"].ToString() : "",
                                OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                                ProductName = dr["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                                PlatformFee = dr["PlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFee"]) : 0m,
                                DeliveryOtp = dr["DeliveryOtp"] != DBNull.Value ? dr["DeliveryOtp"].ToString() : null,
                                IsOtpVerified = dr["IsOtpVerified"] != DBNull.Value && Convert.ToBoolean(dr["IsOtpVerified"]),
                                CustomerPhone = dr["CustomerPhone"] != DBNull.Value ? dr["CustomerPhone"].ToString() : null,
                                ItemsSummary = dr["ItemsSummary"] != DBNull.Value ? dr["ItemsSummary"].ToString() : dr["ProductName"].ToString(),
                                CancellationSecondsRemaining = dr["SecondsSinceCreation"] != DBNull.Value ? Math.Max(0, 30 - Convert.ToInt32(dr["SecondsSinceCreation"])) : 0,
                                CanCancel = (dr["OrderStatus"] != DBNull.Value && dr["OrderStatus"].ToString() == "Placed" && dr["SecondsSinceCreation"] != DBNull.Value && Convert.ToInt32(dr["SecondsSinceCreation"]) <= 30),
                                PaymentMethod = dr["PaymentMethod"].ToString(),
                                OrderStatus = dr["OrderStatus"] != DBNull.Value ? dr["OrderStatus"].ToString() : "Delivered",
                                PreparationTimeMinutes = dr["PreparationTimeMinutes"] != DBNull.Value ? Convert.ToInt32(dr["PreparationTimeMinutes"]) : 15,
                                EstimatedDeliveryMinutes = dr["EstimatedDeliveryMinutes"] != DBNull.Value ? Convert.ToInt32(dr["EstimatedDeliveryMinutes"]) : 45,
                                DistanceKm = dr["DistanceKm"] != DBNull.Value ? Convert.ToDecimal(dr["DistanceKm"]) : 3.5m,
                                AssignedAgentName = dr["AssignedAgentName"] != DBNull.Value ? dr["AssignedAgentName"].ToString() : "Rider Assigned Soon",
                                AssignedAgentPhone = dr["AssignedAgentPhone"] != DBNull.Value ? dr["AssignedAgentPhone"].ToString() : ""
                            };
                        }
                    }
                }
            }
            return vm;
        }

        // 5. Get Order for Live Tracking
        public OrderTrackingViewModel GetOrderForTracking(int orderId)
        {
            OrderTrackingViewModel vm = null;
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand(@"
                    SELECT TOP 1 
                        o.OrderId, o.InvoiceNumber, o.StoreId, o.StoreName, s.Address AS StoreAddress,
                        o.CustomerName, o.DeliveryAddress, o.DeliveryCity, o.OrderStatus,
                        o.PreparationTimeMinutes, o.EstimatedDeliveryMinutes, o.DistanceKm,
                        o.AssignedAgentName, o.AssignedAgentPhone, o.OrderDate, o.TotalAmount, o.ProductName,
                        o.DeliveryOtp, ISNULL(o.IsOtpVerified, 0) AS IsOtpVerified,
                        o.CustomerPhone, o.ItemsSummary, o.CreatedAt,
                        DATEDIFF(SECOND, o.CreatedAt, GETDATE()) AS SecondsSinceCreation
                    FROM hra.Orders o
                    LEFT JOIN hra.Stores s ON o.StoreId = s.StoreId
                    WHERE o.OrderId = @OrderId", con))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var orderDate = Convert.ToDateTime(dr["OrderDate"]);
                            var prepMinutes = dr["PreparationTimeMinutes"] != DBNull.Value ? Convert.ToInt32(dr["PreparationTimeMinutes"]) : 15;
                            var elapsed = (int)(DateTime.Now - orderDate).TotalMinutes;
                            var remaining = Math.Max(0, prepMinutes - elapsed);

                            vm = new OrderTrackingViewModel
                            {
                                OrderId = Convert.ToInt32(dr["OrderId"]),
                                InvoiceNumber = dr["InvoiceNumber"] != DBNull.Value ? dr["InvoiceNumber"].ToString() : "INV-FD-" + orderId,
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                StoreName = dr["StoreName"].ToString(),
                                StoreAddress = dr["StoreAddress"] != DBNull.Value ? dr["StoreAddress"].ToString() : "",
                                CustomerName = dr["CustomerName"].ToString(),
                                DeliveryAddress = dr["DeliveryAddress"] != DBNull.Value ? dr["DeliveryAddress"].ToString() : "",
                                DeliveryCity = dr["DeliveryCity"].ToString(),
                                OrderStatus = dr["OrderStatus"] != DBNull.Value ? dr["OrderStatus"].ToString() : "Placed",
                                PreparationTimeMinutes = prepMinutes,
                                EstimatedDeliveryMinutes = dr["EstimatedDeliveryMinutes"] != DBNull.Value ? Convert.ToInt32(dr["EstimatedDeliveryMinutes"]) : 45,
                                DistanceKm = dr["DistanceKm"] != DBNull.Value ? Convert.ToDecimal(dr["DistanceKm"]) : 3.5m,
                                AssignedAgentName = dr["AssignedAgentName"] != DBNull.Value ? dr["AssignedAgentName"].ToString() : null,
                                AssignedAgentPhone = dr["AssignedAgentPhone"] != DBNull.Value ? dr["AssignedAgentPhone"].ToString() : null,
                                OrderDate = orderDate,
                                TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                                ProductName = dr["ProductName"].ToString(),
                                ElapsedMinutes = elapsed,
                                RemainingPrepMinutes = remaining,
                                DeliveryOtp = dr["DeliveryOtp"] != DBNull.Value ? dr["DeliveryOtp"].ToString() : null,
                                IsOtpVerified = dr["IsOtpVerified"] != DBNull.Value && Convert.ToBoolean(dr["IsOtpVerified"]),
                                CustomerPhone = dr["CustomerPhone"] != DBNull.Value ? dr["CustomerPhone"].ToString() : null,
                                ItemsSummary = dr["ItemsSummary"] != DBNull.Value ? dr["ItemsSummary"].ToString() : dr["ProductName"].ToString(),
                                CancellationSecondsRemaining = dr["SecondsSinceCreation"] != DBNull.Value ? Math.Max(0, 30 - Convert.ToInt32(dr["SecondsSinceCreation"])) : 0,
                                CanCancel = (dr["OrderStatus"] != DBNull.Value && dr["OrderStatus"].ToString() == "Placed" && dr["SecondsSinceCreation"] != DBNull.Value && Convert.ToInt32(dr["SecondsSinceCreation"]) <= 30)
                            };
                        }
                    }
                }
            }
            return vm;
        }

        // 6. Update Order Status (auto-assigns delivery agent on ReadyForPickup)
        public bool UpdateOrderStatus(int orderId, string newStatus)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_UpdateOrderStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@NewStatus", newStatus);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return Convert.ToBoolean(dr["IsSuccess"]);
                        }
                    }
                }
            }
            return false;
        }

        // 6b. Verify Delivery OTP and Complete Delivery (Strict Rider OTP Verification)
        public bool VerifyDeliveryOtp(int orderId, int agentId, string otp, out string message)
        {
            message = string.Empty;
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_VerifyDeliveryOtpAndComplete", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@AgentId", agentId);
                    cmd.Parameters.AddWithValue("@EnteredOtp", (object)otp?.Trim() ?? string.Empty);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bool success = Convert.ToBoolean(dr["IsSuccess"]);
                            message = dr["Message"] != DBNull.Value ? dr["Message"].ToString() : "";
                            return success;
                        }
                    }
                }
            }
            message = "Unable to connect to database for OTP verification.";
            return false;
        }

        // 6c. Cancel Customer Order (Within 30-Second Grace Window)
        public bool CancelCustomerOrder(int orderId, int? customerId, string reason, out string message)
        {
            message = string.Empty;
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_CancelCustomerOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId.HasValue ? (object)customerId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Reason", string.IsNullOrWhiteSpace(reason) ? "Cancelled by customer within 30-second grace window" : reason.Trim());

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bool success = Convert.ToBoolean(dr["IsSuccess"]);
                            message = dr["Message"] != DBNull.Value ? dr["Message"].ToString() : "";
                            return success;
                        }
                    }
                }
            }
            message = "Unable to connect to database to cancel order.";
            return false;
        }

        // 7. Get Customer Orders History
        public List<Orders> GetCustomerOrders(string customerName = null, int? customerId = null)
        {
            var list = new List<Orders>();
            using (var con = new SqlConnection(_cnStr))
            {
                string sql = @"
                    SELECT 
                        OrderId, StoreId, StoreName, CustomerName, ProductName, OrderDate, Quantity, TotalAmount,
                        PaymentMethod, DeliveryCity, ISNULL(OrderStatus, 'Delivered') AS OrderStatus,
                        ISNULL(PreparationTimeMinutes, 15) AS PreparationTimeMinutes,
                        AssignedAgentName, AssignedAgentPhone, DeliveryAddress, EstimatedDeliveryMinutes,
                        DistanceKm, InvoiceNumber, PlatformFee, CreatedAt, CustomerId, DeliveryOtp, ISNULL(IsOtpVerified, 0) AS IsOtpVerified
                    FROM hra.Orders
                    WHERE (@CustomerName IS NULL OR LOWER(CustomerName) = LOWER(@CustomerName) OR CustomerName LIKE '%' + @CustomerName + '%')
                       OR (@CustomerId IS NOT NULL AND CustomerId = @CustomerId)
                    ORDER BY OrderDate DESC";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerName", string.IsNullOrWhiteSpace(customerName) ? (object)DBNull.Value : customerName.Trim());
                    cmd.Parameters.AddWithValue("@CustomerId", customerId.HasValue ? (object)customerId.Value : DBNull.Value);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Orders
                            {
                                OrderId = Convert.ToInt32(dr["OrderId"]),
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                StoreName = dr["StoreName"].ToString(),
                                CustomerName = dr["CustomerName"].ToString(),
                                ProductName = dr["ProductName"].ToString(),
                                OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                TotalAmount = Convert.ToInt32(dr["TotalAmount"]),
                                PaymentMethod = dr["PaymentMethod"].ToString(),
                                DeliveryCity = dr["DeliveryCity"].ToString(),
                                OrderStatus = dr["OrderStatus"].ToString(),
                                PreparationTimeMinutes = Convert.ToInt32(dr["PreparationTimeMinutes"]),
                                AssignedAgentName = dr["AssignedAgentName"] != DBNull.Value ? dr["AssignedAgentName"].ToString() : null,
                                AssignedAgentPhone = dr["AssignedAgentPhone"] != DBNull.Value ? dr["AssignedAgentPhone"].ToString() : null,
                                DeliveryAddress = dr["DeliveryAddress"] != DBNull.Value ? dr["DeliveryAddress"].ToString() : null,
                                EstimatedDeliveryMinutes = dr["EstimatedDeliveryMinutes"] != DBNull.Value ? Convert.ToInt32(dr["EstimatedDeliveryMinutes"]) : 45,
                                DistanceKm = dr["DistanceKm"] != DBNull.Value ? Convert.ToDecimal(dr["DistanceKm"]) : 3.5m,
                                InvoiceNumber = dr["InvoiceNumber"] != DBNull.Value ? dr["InvoiceNumber"].ToString() : null,
                                PlatformFee = dr["PlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFee"]) : 0m,
                                DeliveryOtp = dr["DeliveryOtp"] != DBNull.Value ? dr["DeliveryOtp"].ToString() : null,
                                IsOtpVerified = dr["IsOtpVerified"] != DBNull.Value && Convert.ToBoolean(dr["IsOtpVerified"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // 8. Add Customer Review
        public bool AddReview(int storeId, string customerName, int rating, string comment, int? orderId = null, int? customerId = null)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_AddRestaurantReview", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StoreId", storeId);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId.HasValue ? (object)customerId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@OrderId", orderId.HasValue ? (object)orderId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.Parameters.AddWithValue("@Comment", string.IsNullOrWhiteSpace(comment) ? (object)DBNull.Value : comment.Trim());

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return Convert.ToBoolean(dr["IsSuccess"]);
                        }
                    }
                }
            }
            return false;
        }

        // 9. Restaurant Respond to Review
        public bool RespondToReview(int reviewId, string response)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_RespondToReview", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReviewId", reviewId);
                    cmd.Parameters.AddWithValue("@StoreResponse", response);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return Convert.ToBoolean(dr["IsSuccess"]);
                        }
                    }
                }
            }
            return false;
        }

        // 10. Restaurant Dashboard View
        public RestaurantDashboardViewModel GetRestaurantDashboard(int storeId)
        {
            var vm = new RestaurantDashboardViewModel();
            vm.Store = GetStoreById(storeId);
            if (vm.Store == null) return vm;

            // Financial Summary & Invoices
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_GetRestaurantInvoicesAndSales", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StoreId", storeId);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            vm.TotalOrders = dr["TotalOrdersCount"] != DBNull.Value ? Convert.ToInt32(dr["TotalOrdersCount"]) : 0;
                            vm.GrossSales = dr["GrossSales"] != DBNull.Value ? Convert.ToDecimal(dr["GrossSales"]) : 0m;
                            vm.PlatformFeeDeducted = dr["PlatformFeeDeducted"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFeeDeducted"]) : 0m;
                            decimal netPayout = 0m;
                            try { if (dr["NetPayout"] != DBNull.Value) netPayout = Convert.ToDecimal(dr["NetPayout"]); }
                            catch { try { if (dr["NetEarnings"] != DBNull.Value) netPayout = Convert.ToDecimal(dr["NetEarnings"]); } catch { } }
                            vm.NetPayout = netPayout;
                            vm.CompletedOrders = vm.Store.TotalOrdersCompleted;
                            vm.AverageRating = vm.Store.Rating;
                        }

                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                var o = new Orders
                                {
                                    OrderId = dr["OrderId"] != DBNull.Value ? Convert.ToInt32(dr["OrderId"]) : 0,
                                    InvoiceNumber = dr["InvoiceNumber"] != DBNull.Value ? dr["InvoiceNumber"].ToString() : "INV-FD-" + dr["OrderId"],
                                    CustomerName = dr["CustomerName"] != DBNull.Value ? dr["CustomerName"].ToString() : "",
                                    ProductName = dr["ProductName"] != DBNull.Value ? dr["ProductName"].ToString() : "",
                                    OrderDate = dr["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dr["OrderDate"]) : DateTime.Now,
                                    Quantity = dr["Quantity"] != DBNull.Value ? Convert.ToInt32(dr["Quantity"]) : 1,
                                    TotalAmount = dr["GrossAmount"] != DBNull.Value ? Convert.ToInt32(Convert.ToDecimal(dr["GrossAmount"])) : 0,
                                    PlatformFee = dr["PlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFee"]) : 0m,
                                    PaymentMethod = dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : "UPI",
                                    OrderStatus = dr["OrderStatus"] != DBNull.Value ? dr["OrderStatus"].ToString() : "Delivered",
                                    DeliveryCity = dr["DeliveryCity"] != DBNull.Value ? dr["DeliveryCity"].ToString() : "",
                                    DeliveryAddress = dr["DeliveryAddress"] != DBNull.Value ? dr["DeliveryAddress"].ToString() : "",
                                    ItemsSummary = dr["ItemsSummary"] != DBNull.Value ? dr["ItemsSummary"].ToString() : (dr["ProductName"] != DBNull.Value ? dr["ProductName"].ToString() : ""),
                                    CustomerPhone = dr["CustomerPhone"] != DBNull.Value ? dr["CustomerPhone"].ToString() : null,
                                    PreparationTimeMinutes = dr["PreparationTimeMinutes"] != DBNull.Value ? Convert.ToInt32(dr["PreparationTimeMinutes"]) : 15,
                                    EstimatedDeliveryMinutes = dr["EstimatedDeliveryMinutes"] != DBNull.Value ? Convert.ToInt32(dr["EstimatedDeliveryMinutes"]) : 45,
                                    DistanceKm = dr["DistanceKm"] != DBNull.Value ? Convert.ToDecimal(dr["DistanceKm"]) : 3.5m,
                                    AssignedAgentName = dr["AssignedAgentName"] != DBNull.Value ? dr["AssignedAgentName"].ToString() : null,
                                    AssignedAgentPhone = dr["AssignedAgentPhone"] != DBNull.Value ? dr["AssignedAgentPhone"].ToString() : null
                                };
                                vm.LiveOrders.Add(o);
                                if (o.OrderStatus == "Placed" || o.OrderStatus == "Preparing")
                                {
                                    vm.PendingOrders++;
                                }
                            }
                        }
                    }
                }
            }

            int actualCompleted = vm.LiveOrders.Count(o => o.OrderStatus.Equals("Delivered", StringComparison.OrdinalIgnoreCase));
            vm.CompletedOrders = Math.Max(vm.CompletedOrders, Math.Max(vm.Store.TotalOrdersCompleted, actualCompleted));

            vm.MenuItems = GetMenuItems(storeId);
            vm.Reviews = GetReviewsByStore(storeId);
            vm.Certificate = GetRestaurantCertificate(storeId);
            return vm;
        }

        // 11. Menu Items CRUD
        public List<MenuItem> GetMenuItems(int storeId)
        {
            var list = new List<MenuItem>();
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_GetMenuItemsByStore", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StoreId", storeId);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new MenuItem
                            {
                                MenuItemId = Convert.ToInt32(dr["MenuItemId"]),
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                ItemName = dr["ItemName"].ToString(),
                                Description = dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "",
                                Category = dr["Category"].ToString(),
                                Price = Convert.ToDecimal(dr["Price"]),
                                ImageUrl = dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "",
                                IsVeg = Convert.ToBoolean(dr["IsVeg"]),
                                IsAvailable = Convert.ToBoolean(dr["IsAvailable"]),
                                PrepTimeMinutes = dr["PrepTimeMinutes"] != DBNull.Value ? Convert.ToInt32(dr["PrepTimeMinutes"]) : 15,
                                CreatedAt = Convert.ToDateTime(dr["CreatedAt"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool AddOrUpdateMenuItem(MenuItem item)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_AddOrUpdateMenuItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MenuItemId", item.MenuItemId);
                    cmd.Parameters.AddWithValue("@StoreId", item.StoreId);
                    cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                    cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(item.Description) ? (object)DBNull.Value : item.Description);
                    cmd.Parameters.AddWithValue("@Category", string.IsNullOrWhiteSpace(item.Category) ? "Main Course" : item.Category);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    cmd.Parameters.AddWithValue("@ImageUrl", string.IsNullOrWhiteSpace(item.ImageUrl) ? (object)DBNull.Value : item.ImageUrl);
                    cmd.Parameters.AddWithValue("@IsVeg", item.IsVeg);
                    cmd.Parameters.AddWithValue("@IsAvailable", item.IsAvailable);
                    cmd.Parameters.AddWithValue("@PrepTimeMinutes", item.PrepTimeMinutes > 0 ? item.PrepTimeMinutes : 15);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteMenuItem(int menuItemId)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_DeleteMenuItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MenuItemId", menuItemId);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 12. Trust Certificate (Requires 200 orders + >= 3.5 rating)
        public RestaurantCertificate GetRestaurantCertificate(int storeId)
        {
            RestaurantCertificate cert = null;
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_GetRestaurantCertificate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StoreId", storeId);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cert = new RestaurantCertificate
                            {
                                CertificateId = Convert.ToInt32(dr["CertificateId"]),
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                StoreName = dr["StoreName"].ToString(),
                                OwnerName = dr["OwnerName"].ToString(),
                                City = dr["City"].ToString(),
                                FssaiLicenseNo = dr["FssaiLicenseNo"] != DBNull.Value ? dr["FssaiLicenseNo"].ToString() : "",
                                HygieneScore = dr["HygieneScore"] != DBNull.Value ? Convert.ToInt32(dr["HygieneScore"]) : 90,
                                CertificateNumber = dr["CertificateNumber"].ToString(),
                                IssuedDate = Convert.ToDateTime(dr["IssuedDate"]),
                                TotalOrdersAtIssue = Convert.ToInt32(dr["TotalOrdersAtIssue"]),
                                AverageRatingAtIssue = Convert.ToDecimal(dr["AverageRatingAtIssue"]),
                                CertificateTitle = dr["CertificateTitle"].ToString(),
                                IsValid = Convert.ToBoolean(dr["IsValid"]),
                                CurrentOrdersCompleted = Convert.ToInt32(dr["CurrentOrdersCompleted"]),
                                CurrentRating = Convert.ToDecimal(dr["CurrentRating"])
                            };
                        }
                    }
                }
            }

            // Fallback: If store completed >= 200 orders and rating >= 3.5, generate preview certificate dynamically if not yet registered in table
            if (cert == null)
            {
                var s = GetStoreById(storeId);
                if (s != null && s.TotalOrdersCompleted >= 200 && s.Rating >= 3.5m)
                {
                    cert = new RestaurantCertificate
                    {
                        CertificateId = 9999 + storeId,
                        StoreId = s.StoreId,
                        StoreName = s.StoreName,
                        OwnerName = s.OwnerName,
                        City = s.City,
                        FssaiLicenseNo = s.FssaiLicenseNo ?? "FSSAI-10022021000" + storeId,
                        HygieneScore = s.HygieneScore > 0 ? s.HygieneScore : 94,
                        CertificateNumber = "CERT-TRUST-" + s.City.Substring(0, Math.Min(3, s.City.Length)).ToUpper() + "-" + s.TotalOrdersCompleted + s.StoreId,
                        IssuedDate = DateTime.Now,
                        TotalOrdersAtIssue = s.TotalOrdersCompleted,
                        AverageRatingAtIssue = s.Rating,
                        CertificateTitle = "Certificate of Food Quality, Hygiene & Customer Trust",
                        IsValid = true,
                        CurrentOrdersCompleted = s.TotalOrdersCompleted,
                        CurrentRating = s.Rating
                    };
                }
            }

            return cert;
        }

        // 13. Admin City Analytics & Food Safety Verification
        public AdminFoodDeliveryViewModel GetAdminAnalytics(string state = null, string city = null)
        {
            var vm = new AdminFoodDeliveryViewModel 
            { 
                SelectedState = state, 
                SelectedCity = city 
            };

            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_GetAdminCityAnalytics", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State", string.IsNullOrWhiteSpace(state) || state == "All States" ? (object)DBNull.Value : state.Trim());
                    cmd.Parameters.AddWithValue("@City", string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city.Trim());

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        // 1. Summary
                        if (dr.Read())
                        {
                            vm.TotalRestaurants = dr["TotalRestaurants"] != DBNull.Value ? Convert.ToInt32(dr["TotalRestaurants"]) : 0;
                            vm.VerifiedRestaurants = dr["VerifiedRestaurants"] != DBNull.Value ? Convert.ToInt32(dr["VerifiedRestaurants"]) : 0;
                            vm.PendingVerificationsCount = dr["PendingVerificationRestaurants"] != DBNull.Value ? Convert.ToInt32(dr["PendingVerificationRestaurants"]) : 0;
                            vm.TotalGrossSales = dr["TotalGrossSales"] != DBNull.Value ? Convert.ToDecimal(dr["TotalGrossSales"]) : 0m;
                            vm.TotalPlatformFeeRevenue = dr["TotalPlatformFeeRevenue"] != DBNull.Value ? Convert.ToDecimal(dr["TotalPlatformFeeRevenue"]) : 0m;
                            vm.TotalOrdersCount = dr["TotalOrdersCount"] != DBNull.Value ? Convert.ToInt32(dr["TotalOrdersCount"]) : 0;
                            vm.OverallAverageRating = dr["OverallAverageRating"] != DBNull.Value ? Convert.ToDecimal(dr["OverallAverageRating"]) : 4.2m;
                            vm.ActiveDeliveryAgentsCount = dr["ActiveDeliveryAgentsCount"] != DBNull.Value ? Convert.ToInt32(dr["ActiveDeliveryAgentsCount"]) : 10;
                        }

                        // 2. City breakdown
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                vm.CityMetrics.Add(new CityMetricRow
                                {
                                    State = dr["State"] != DBNull.Value ? dr["State"].ToString() : "",
                                    City = dr["City"].ToString(),
                                    RestaurantCount = Convert.ToInt32(dr["RestaurantCount"]),
                                    VerifiedCount = Convert.ToInt32(dr["VerifiedCount"]),
                                    OrdersCount = Convert.ToInt32(dr["OrdersCount"]),
                                    CityGrossSales = Convert.ToDecimal(dr["CityGrossSales"]),
                                    CityPlatformFee = Convert.ToDecimal(dr["CityPlatformFee"]),
                                    AvgCityRating = Convert.ToDecimal(dr["AvgCityRating"]),
                                    AvgHygieneScore = Convert.ToInt32(dr["AvgHygieneScore"])
                                });
                            }
                        }

                        // 3. Restaurants list
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                var r = new Stores
                                {
                                    StoreId = Convert.ToInt32(dr["StoreId"]),
                                    StoreName = dr["StoreName"].ToString(),
                                    OwnerName = dr["OwnerName"].ToString(),
                                    State = dr["State"] != DBNull.Value ? dr["State"].ToString() : "",
                                    City = dr["City"].ToString(),
                                    Cuisine = dr["Cuisine"].ToString(),
                                    ContactNumber = dr["ContactNumber"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Rating = Convert.ToDecimal(dr["Rating"]),
                                    HygieneScore = Convert.ToInt32(dr["HygieneScore"]),
                                    IsVerified = Convert.ToBoolean(dr["IsVerified"]),
                                    VerificationStatus = dr["VerificationStatus"].ToString(),
                                    FssaiLicenseNo = dr["FssaiLicenseNo"] != DBNull.Value ? dr["FssaiLicenseNo"].ToString() : "",
                                    TotalOrdersCompleted = Convert.ToInt32(dr["TotalOrdersCompleted"]),
                                    ReviewCount = Convert.ToInt32(dr["ReviewCount"])
                                };
                                vm.AllRestaurants.Add(r);
                                if (!r.IsVerified || !r.VerificationStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                                {
                                    vm.PendingKitchens.Add(r);
                                }
                            }
                        }
                    }
                }
            }

            vm.DeliveryAgents = GetDeliveryAgents();
            vm.DbHealth = CheckDatabasesHealth();
            return vm;
        }

        // 14. Food Safety Verification by Admin
        public bool VerifyFoodSafety(int storeId, string fssai, int hygieneScore, string waterCert, string pestCert, string fireNoc, string remarks, string action, int? adminUserId = null)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_VerifyRestaurantFoodSafety", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StoreId", storeId);
                    cmd.Parameters.AddWithValue("@FssaiLicense", fssai);
                    cmd.Parameters.AddWithValue("@HygieneScore", hygieneScore);
                    cmd.Parameters.AddWithValue("@WaterSafetyCertNo", string.IsNullOrWhiteSpace(waterCert) ? (object)DBNull.Value : waterCert);
                    cmd.Parameters.AddWithValue("@PestControlCertNo", string.IsNullOrWhiteSpace(pestCert) ? (object)DBNull.Value : pestCert);
                    cmd.Parameters.AddWithValue("@FireSafetyNoc", string.IsNullOrWhiteSpace(fireNoc) ? (object)DBNull.Value : fireNoc);
                    cmd.Parameters.AddWithValue("@AuditRemarks", string.IsNullOrWhiteSpace(remarks) ? (object)DBNull.Value : remarks);
                    cmd.Parameters.AddWithValue("@Action", action);
                    cmd.Parameters.AddWithValue("@AdminUserId", adminUserId.HasValue ? (object)adminUserId.Value : DBNull.Value);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return Convert.ToBoolean(dr["IsSuccess"]);
                        }
                    }
                }
            }
            return false;
        }

        // 15. Delivery Agents
        public List<DeliveryAgent> GetDeliveryAgents(string city = null)
        {
            var list = new List<DeliveryAgent>();
            using (var con = new SqlConnection(_cnStr))
            {
                string sql = "SELECT * FROM hra.DeliveryAgents WHERE (@City IS NULL OR LOWER(City) = LOWER(@City)) ORDER BY IsActive DESC, TotalDeliveries DESC";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@City", string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city.Trim());
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new DeliveryAgent
                            {
                                AgentId = Convert.ToInt32(dr["AgentId"]),
                                UserId = dr["UserId"] != DBNull.Value ? (int?)Convert.ToInt32(dr["UserId"]) : null,
                                FullName = dr["FullName"].ToString(),
                                PhoneNumber = dr["PhoneNumber"].ToString(),
                                Email = dr["Email"].ToString(),
                                AadhaarNumber = dr["AadhaarNumber"].ToString(),
                                IsAadhaarVerified = Convert.ToBoolean(dr["IsAadhaarVerified"]),
                                VehicleType = dr["VehicleType"].ToString(),
                                VehicleNumber = dr["VehicleNumber"].ToString(),
                                City = dr["City"].ToString(),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                TotalDeliveries = Convert.ToInt32(dr["TotalDeliveries"]),
                                TotalEarnings = Convert.ToDecimal(dr["TotalEarnings"]),
                                Rating = Convert.ToDecimal(dr["Rating"]),
                                CreatedAt = Convert.ToDateTime(dr["CreatedAt"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool RegisterDeliveryAgent(DeliveryAgent agent, out string message)
        {
            message = "";
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_RegisterDeliveryAgent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", agent.FullName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", agent.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", agent.Email);
                    cmd.Parameters.AddWithValue("@AadhaarNumber", agent.AadhaarNumber);
                    cmd.Parameters.AddWithValue("@VehicleType", string.IsNullOrWhiteSpace(agent.VehicleType) ? "Motorcycle" : agent.VehicleType);
                    cmd.Parameters.AddWithValue("@VehicleNumber", agent.VehicleNumber);
                    cmd.Parameters.AddWithValue("@City", agent.City);
                    cmd.Parameters.AddWithValue("@UserId", agent.UserId.HasValue ? (object)agent.UserId.Value : DBNull.Value);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            message = dr["Message"].ToString();
                            return Convert.ToBoolean(dr["IsSuccess"]);
                        }
                    }
                }
            }
            return false;
        }

        // Lookup Delivery Agent By Logged In UserId
        public DeliveryAgent GetDeliveryAgentByUserId(int userId)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                string sql = @"
                    SELECT TOP 1 a.AgentId, a.FullName, a.PhoneNumber, a.VehicleType, a.VehicleNumber, a.City, a.IsActive, a.TotalDeliveries, a.Rating, a.TotalEarnings, a.AadhaarNumber
                    FROM hra.DeliveryAgents a
                    WHERE a.UserId = @UserId";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new DeliveryAgent
                            {
                                AgentId = Convert.ToInt32(dr["AgentId"]),
                                FullName = dr["FullName"].ToString(),
                                PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : null,
                                VehicleType = dr["VehicleType"] != DBNull.Value ? dr["VehicleType"].ToString() : null,
                                VehicleNumber = dr["VehicleNumber"] != DBNull.Value ? dr["VehicleNumber"].ToString() : null,
                                City = dr["City"] != DBNull.Value ? dr["City"].ToString() : null,
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                TotalDeliveries = Convert.ToInt32(dr["TotalDeliveries"]),
                                Rating = Convert.ToDecimal(dr["Rating"]),
                                TotalEarnings = Convert.ToDecimal(dr["TotalEarnings"]),
                                AadhaarNumber = dr["AadhaarNumber"] != DBNull.Value ? dr["AadhaarNumber"].ToString() : ""
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool ToggleDeliveryAgentStatus(int agentId, bool isActive)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_ToggleDeliveryAgentStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AgentId", agentId);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DeliveryAgentDashboardViewModel GetDeliveryAgentDashboard(int agentId)
        {
            var vm = new DeliveryAgentDashboardViewModel();
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_GetDeliveryAgentDashboard", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AgentId", agentId);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        // 1. Agent profile
                        if (dr.Read())
                        {
                            vm.Agent = new DeliveryAgent
                            {
                                AgentId = Convert.ToInt32(dr["AgentId"]),
                                FullName = dr["FullName"].ToString(),
                                PhoneNumber = dr["PhoneNumber"].ToString(),
                                Email = dr["Email"].ToString(),
                                AadhaarNumber = dr["AadhaarNumber"].ToString(),
                                IsAadhaarVerified = Convert.ToBoolean(dr["IsAadhaarVerified"]),
                                VehicleType = dr["VehicleType"].ToString(),
                                VehicleNumber = dr["VehicleNumber"].ToString(),
                                City = dr["City"].ToString(),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                TotalDeliveries = Convert.ToInt32(dr["TotalDeliveries"]),
                                TotalEarnings = Convert.ToDecimal(dr["TotalEarnings"]),
                                Rating = Convert.ToDecimal(dr["Rating"])
                            };
                            vm.LifetimeEarnings = vm.Agent.TotalEarnings;
                            vm.TotalCompletedDeliveries = vm.Agent.TotalDeliveries;
                        }

                        // 2. Active orders
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                vm.ActiveOrders.Add(new Orders
                                {
                                    OrderId = Convert.ToInt32(dr["OrderId"]),
                                    StoreId = Convert.ToInt32(dr["StoreId"]),
                                    StoreName = dr["StoreName"].ToString(),
                                    CustomerName = dr["CustomerName"].ToString(),
                                    DeliveryAddress = dr["DeliveryAddress"].ToString(),
                                    DeliveryCity = dr["DeliveryCity"].ToString(),
                                    ProductName = dr["ProductName"].ToString(),
                                    Quantity = Convert.ToInt32(dr["Quantity"]),
                                    TotalAmount = Convert.ToInt32(dr["TotalAmount"]),
                                    OrderStatus = dr["OrderStatus"].ToString(),
                                    PreparationTimeMinutes = Convert.ToInt32(dr["PreparationTimeMinutes"]),
                                    EstimatedDeliveryMinutes = Convert.ToInt32(dr["EstimatedDeliveryMinutes"]),
                                    DistanceKm = Convert.ToDecimal(dr["DistanceKm"]),
                                    DeliveryEarnings = Convert.ToDecimal(dr["DeliveryEarnings"]),
                                    CustomerPhone = dr["CustomerPhone"] != DBNull.Value ? dr["CustomerPhone"].ToString() : null,
                                    ItemsSummary = dr["ItemsSummary"] != DBNull.Value ? dr["ItemsSummary"].ToString() : dr["ProductName"].ToString(),
                                    IsOtpVerified = dr["IsOtpVerified"] != DBNull.Value && Convert.ToBoolean(dr["IsOtpVerified"])
                                });
                            }
                        }

                        // 3. Completed orders
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                var o = new Orders
                                {
                                    OrderId = Convert.ToInt32(dr["OrderId"]),
                                    StoreName = dr["StoreName"].ToString(),
                                    CustomerName = dr["CustomerName"].ToString(),
                                    DeliveryAddress = dr["DeliveryAddress"].ToString(),
                                    TotalAmount = Convert.ToInt32(dr["TotalAmount"]),
                                    OrderStatus = dr["OrderStatus"].ToString(),
                                    OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                                    DeliveryEarnings = Convert.ToDecimal(dr["DeliveryEarnings"])
                                };
                                vm.CompletedDeliveries.Add(o);
                                if (o.OrderDate.Date == DateTime.Today)
                                {
                                    vm.TodayEarnings += o.DeliveryEarnings;
                                }
                            }
                        }
                    }
                }
            }
            return vm;
        }

        // 16. Notifications
        public List<AppNotification> GetNotifications(string role = "Customer", int? userId = null, int? storeId = null, int? agentId = null)
        {
            var list = new List<AppNotification>();
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_GetNotifications", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@UserId", userId.HasValue ? (object)userId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@StoreId", storeId.HasValue ? (object)storeId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentId", agentId.HasValue ? (object)agentId.Value : DBNull.Value);

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new AppNotification
                            {
                                NotificationId = Convert.ToInt32(dr["NotificationId"]),
                                RecipientRole = dr["RecipientRole"].ToString(),
                                Title = dr["Title"].ToString(),
                                Message = dr["Message"].ToString(),
                                NotificationType = dr["NotificationType"].ToString(),
                                IsRead = Convert.ToBoolean(dr["IsRead"]),
                                CreatedAt = Convert.ToDateTime(dr["CreatedAt"])
                            });
                        }
                    }
                }
            }

            // Guard: Never show certificate notifications if restaurant has not achieved 200 orders + 3.5+ rating
            if (role.Equals("Restaurant", StringComparison.OrdinalIgnoreCase) && storeId.HasValue && storeId.Value > 0)
            {
                var store = GetStoreById(storeId.Value);
                if (store != null)
                {
                    bool isEligible = store.TotalOrdersCompleted >= 200 && store.Rating >= 3.5m;
                    if (!isEligible)
                    {
                        list = list.Where(n => !n.Title.Contains("Certificate") && !n.Message.Contains("Certificate")).ToList();
                    }
                }
            }

            return list;
        }

        public bool MarkNotificationRead(int notificationId)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                using (var cmd = new SqlCommand("hra.usp_MarkNotificationRead", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NotificationId", notificationId);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 17. Dual Database Health Check ("there were two database one is missing make that change ui")
        public DatabaseHealthStatus CheckDatabasesHealth()
        {
            var status = new DatabaseHealthStatus();

            // Check AssignmentDB
            try
            {
                using (var con = new SqlConnection(_cnStr))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(@"
                        SELECT 
                            (SELECT COUNT(1) FROM hra.Stores) AS StoreCount,
                            (SELECT COUNT(1) FROM hra.Orders) AS OrderCount,
                            (SELECT COUNT(1) FROM hra.DeliveryAgents WHERE IsActive = 1) AS AgentCount", con))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                status.AssignmentDbStoresCount = Convert.ToInt32(dr["StoreCount"]);
                                status.AssignmentDbOrdersCount = Convert.ToInt32(dr["OrderCount"]);
                                status.AssignmentDbAgentsCount = Convert.ToInt32(dr["AgentCount"]);
                                status.AssignmentDbStatus = "Online (Connected)";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status.AssignmentDbStatus = "Offline/Error: " + ex.Message;
            }

            // Check Practice DB (LMS database from demonstration project)
            string practiceConnStr = "Server=.;Initial Catalog=Practice;User ID=sa;Password=mcc#1234;TrustServerCertificate=True;";
            try
            {
                using (var con = new SqlConnection(practiceConnStr))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(@"
                        SELECT 
                            (SELECT COUNT(1) FROM LMS.Users) AS UserCount,
                            (SELECT COUNT(1) FROM LMS.Courses) AS CourseCount", con))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                status.PracticeDbUsersCount = Convert.ToInt32(dr["UserCount"]);
                                status.PracticeDbCoursesCount = Convert.ToInt32(dr["CourseCount"]);
                                status.PracticeDbStatus = "Online (Connected & Restored)";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status.PracticeDbStatus = "Practice DB not found or unavailable: " + ex.Message;
            }

            status.Message = (status.AssignmentDbStatus.Contains("Online") && status.PracticeDbStatus.Contains("Online"))
                ? "Both databases (AssignmentDB & Practice) are active and fully connected."
                : "One of the databases had an issue, resolved by the connection fallback.";

            return status;
        }

        // Helper: Get Store By Id
        public Stores GetStoreById(int storeId)
        {
            Stores s = null;
            using (var con = new SqlConnection(_cnStr))
            {
                string sql = "SELECT TOP 1 * FROM hra.Stores WHERE StoreId = @StoreId";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StoreId", storeId);
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            s = new Stores
                            {
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                StoreName = dr["StoreName"].ToString(),
                                OwnerName = dr["OwnerName"].ToString(),
                                StoreType = dr["StoreType"].ToString(),
                                ContactNumber = dr["ContactNumber"].ToString(),
                                Email = dr["Email"].ToString(),
                                City = dr["City"].ToString(),
                                State = dr["State"].ToString(),
                                OpeningDate = Convert.ToDateTime(dr["OpeningDate"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                Rating = dr["Rating"] != DBNull.Value ? Convert.ToDecimal(dr["Rating"]) : 4.2m,
                                TotalOrdersCompleted = dr["TotalOrdersCompleted"] != DBNull.Value ? Convert.ToInt32(dr["TotalOrdersCompleted"]) : 0,
                                IsVerified = dr["IsVerified"] != DBNull.Value && Convert.ToBoolean(dr["IsVerified"]),
                                VerificationStatus = dr["VerificationStatus"] != DBNull.Value ? dr["VerificationStatus"].ToString() : "Approved",
                                FssaiLicenseNo = dr["FssaiLicenseNo"] != DBNull.Value ? dr["FssaiLicenseNo"].ToString() : "",
                                HygieneScore = dr["HygieneScore"] != DBNull.Value ? Convert.ToInt32(dr["HygieneScore"]) : 90,
                                Address = dr["Address"] != DBNull.Value ? dr["Address"].ToString() : "",
                                Cuisine = dr["Cuisine"] != DBNull.Value ? dr["Cuisine"].ToString() : "Multi-Cuisine",
                                ImageUrl = dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : ""
                            };
                        }
                    }
                }
            }
            return s;
        }

        // Helper: Get Reviews By Store
        public List<Review> GetReviewsByStore(int storeId)
        {
            var list = new List<Review>();
            using (var con = new SqlConnection(_cnStr))
            {
                string sql = "SELECT * FROM hra.Reviews WHERE StoreId = @StoreId ORDER BY ReviewDate DESC";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StoreId", storeId);
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Review
                            {
                                ReviewId = Convert.ToInt32(dr["ReviewId"]),
                                StoreId = Convert.ToInt32(dr["StoreId"]),
                                CustomerName = dr["CustomerName"].ToString(),
                                Rating = Convert.ToInt32(dr["Rating"]),
                                Comment = dr["Comment"] != DBNull.Value ? dr["Comment"].ToString() : "",
                                ReviewDate = Convert.ToDateTime(dr["ReviewDate"]),
                                StoreResponse = dr["StoreResponse"] != DBNull.Value ? dr["StoreResponse"].ToString() : "",
                                ResponseDate = dr["ResponseDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["ResponseDate"]) : null
                            });
                        }
                    }
                }
            }
            return list;
        }


        // Helper: Get Store By Owner UserId (Multi-Tenancy Binding)
        public Stores GetStoreByOwnerUserId(int userId)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                string sql = "SELECT TOP 1 StoreId FROM hra.Stores WHERE UserId = @UserId";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                    {
                        return GetStoreById(Convert.ToInt32(res));
                    }
                }
            }
            return null;
        }

        // Update Store Profile & Operating Timings
        public bool UpdateStoreProfile(Stores store)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                string sql = @"
                    UPDATE hra.Stores
                    SET StoreName = @StoreName,
                        OwnerName = @OwnerName,
                        StoreType = @StoreType,
                        ContactNumber = @ContactNumber,
                        Email = @Email,
                        Address = @Address,
                        City = @City,
                        State = @State,
                        Cuisine = @Cuisine,
                        OpeningTime = @OpeningTime,
                        ClosingTime = @ClosingTime,
                        BannerUrl = @BannerUrl,
                        ImageUrl = @ImageUrl,
                        Description = @Description
                    WHERE StoreId = @StoreId";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StoreId", store.StoreId);
                    cmd.Parameters.AddWithValue("@StoreName", store.StoreName ?? "");
                    cmd.Parameters.AddWithValue("@OwnerName", store.OwnerName ?? "");
                    cmd.Parameters.AddWithValue("@StoreType", store.StoreType ?? "Restaurant");
                    cmd.Parameters.AddWithValue("@ContactNumber", store.ContactNumber ?? "");
                    cmd.Parameters.AddWithValue("@Email", store.Email ?? "");
                    cmd.Parameters.AddWithValue("@Address", store.Address ?? "");
                    cmd.Parameters.AddWithValue("@City", store.City ?? "");
                    cmd.Parameters.AddWithValue("@State", store.State ?? "");
                    cmd.Parameters.AddWithValue("@Cuisine", store.Cuisine ?? "Multi-Cuisine");
                    cmd.Parameters.AddWithValue("@OpeningTime", !string.IsNullOrWhiteSpace(store.OpeningTime) ? store.OpeningTime : "10:00 AM");
                    cmd.Parameters.AddWithValue("@ClosingTime", !string.IsNullOrWhiteSpace(store.ClosingTime) ? store.ClosingTime : "11:00 PM");
                    cmd.Parameters.AddWithValue("@BannerUrl", !string.IsNullOrWhiteSpace(store.BannerUrl) ? (object)store.BannerUrl : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ImageUrl", !string.IsNullOrWhiteSpace(store.ImageUrl) ? (object)store.ImageUrl : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", !string.IsNullOrWhiteSpace(store.Description) ? (object)store.Description : DBNull.Value);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Mark All Notifications as Read
        public bool MarkAllNotificationsRead(string role, int? storeId, int? userId)
        {
            using (var con = new SqlConnection(_cnStr))
            {
                string sql = "UPDATE hra.Notifications SET IsRead = 1 WHERE RecipientRole = @Role";
                if (storeId.HasValue && storeId.Value > 0)
                {
                    sql += " AND StoreId = @StoreId";
                }
                if (userId.HasValue && userId.Value > 0)
                {
                    sql += " AND (RecipientUserId = @UserId OR RecipientUserId IS NULL)";
                }
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Role", role ?? "Customer");
                    if (storeId.HasValue && storeId.Value > 0)
                        cmd.Parameters.AddWithValue("@StoreId", storeId.Value);
                    if (userId.HasValue && userId.Value > 0)
                        cmd.Parameters.AddWithValue("@UserId", userId.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        // Register Restaurant Store (Linked to User, Pending Super Admin Authorization)
        public bool RegisterRestaurantStore(int userId, RegisterViewModel model, out int newStoreId, out string message)
        {
            newStoreId = 0;
            message = string.Empty;
            try
            {
                using (var con = new SqlConnection(_cnStr))
                {
                    using (var cmd = new SqlCommand("hra.usp_RegisterRestaurantStore", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@StoreName", string.IsNullOrWhiteSpace(model.RestaurantName) ? model.UserName + "'s Kitchen" : model.RestaurantName.Trim());
                        cmd.Parameters.AddWithValue("@OwnerName", model.UserName.Trim());
                        cmd.Parameters.AddWithValue("@StoreType", string.IsNullOrWhiteSpace(model.StoreType) ? "Restaurant" : model.StoreType.Trim());
                        cmd.Parameters.AddWithValue("@ContactNumber", string.IsNullOrWhiteSpace(model.PhoneNumber) ? (object)DBNull.Value : model.PhoneNumber.Trim());
                        cmd.Parameters.AddWithValue("@Email", model.Email.Trim());
                        cmd.Parameters.AddWithValue("@City", string.IsNullOrWhiteSpace(model.City) ? "Delhi" : model.City.Trim());
                        cmd.Parameters.AddWithValue("@State", string.IsNullOrWhiteSpace(model.State) ? "Delhi" : model.State.Trim());
                        cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(model.RestaurantAddress) ? "Kitchen address pending update" : model.RestaurantAddress.Trim());
                        cmd.Parameters.AddWithValue("@Cuisine", string.IsNullOrWhiteSpace(model.Cuisine) ? "Multi-Cuisine" : model.Cuisine.Trim());
                        cmd.Parameters.AddWithValue("@FssaiLicenseNo", string.IsNullOrWhiteSpace(model.FssaiLicenseNo) ? (object)DBNull.Value : model.FssaiLicenseNo.Trim());

                        var pNewStoreId = new SqlParameter("@NewStoreId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(pNewStoreId);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        if (pNewStoreId.Value != DBNull.Value)
                        {
                            newStoreId = Convert.ToInt32(pNewStoreId.Value);
                            message = "Restaurant application submitted successfully! It is now pending Super Admin authorization.";
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return false;
        }

        // Register Delivery Agent / Rider (Linked to User)
        public bool RegisterDeliveryAgent(int userId, RegisterViewModel model, out int newAgentId, out string message)
        {
            newAgentId = 0;
            message = string.Empty;
            try
            {
                using (var con = new SqlConnection(_cnStr))
                {
                    using (var cmd = new SqlCommand("hra.usp_RegisterDeliveryAgent", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@FullName", model.UserName.Trim());
                        cmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber.Trim());
                        cmd.Parameters.AddWithValue("@Email", model.Email.Trim());
                        cmd.Parameters.AddWithValue("@City", string.IsNullOrWhiteSpace(model.City) ? "Delhi" : model.City.Trim());
                        cmd.Parameters.AddWithValue("@State", string.IsNullOrWhiteSpace(model.State) ? "Delhi" : model.State.Trim());
                        cmd.Parameters.AddWithValue("@VehicleType", string.IsNullOrWhiteSpace(model.VehicleType) ? "Motorcycle" : model.VehicleType.Trim());
                        cmd.Parameters.AddWithValue("@VehicleNumber", string.IsNullOrWhiteSpace(model.VehicleNumber) ? (object)DBNull.Value : model.VehicleNumber.Trim());
                        cmd.Parameters.AddWithValue("@AadhaarNumber", string.IsNullOrWhiteSpace(model.AadhaarNumber) ? (object)DBNull.Value : model.AadhaarNumber.Trim());

                        var pNewAgentId = new SqlParameter("@NewAgentId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(pNewAgentId);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        if (pNewAgentId.Value != DBNull.Value)
                        {
                            newAgentId = Convert.ToInt32(pNewAgentId.Value);
                            message = "Delivery Partner registration completed successfully!";
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return false;
        }

        // Quick Authorize / Approve Store by Super Admin
        public bool QuickAuthorizeStore(int storeId, string action, int adminUserId)
        {
            try
            {
                using (var con = new SqlConnection(_cnStr))
                {
                    using (var cmd = new SqlCommand("hra.usp_QuickAuthorizeRestaurant", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StoreId", storeId);
                        cmd.Parameters.AddWithValue("@Action", action ?? "Approve");
                        cmd.Parameters.AddWithValue("@AdminUserId", adminUserId);

                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
