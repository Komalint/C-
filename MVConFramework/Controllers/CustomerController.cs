using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVConFramework.Filters;
using MVConFramework.Models;
using MVConFramework.ViewModels;
using Newtonsoft.Json;

namespace MVConFramework.Controllers
{
    public class CustomerController : Controller
    {
        private readonly FoodDeliveryRepo _repo = new FoodDeliveryRepo();

        // GET: Customer / Browse Restaurants
        public ActionResult Index(string state, string city, string search)
        {
            // Resolve customer's active delivery state:
            // 1. Explicit query parameter 'state'
            // 2. Session["UserDeliveryState"]
            // 3. Session["UserState"] (from logged-in profile)
            // 4. Fallback default to "Delhi"
            string selectedState = state;
            if (string.IsNullOrWhiteSpace(selectedState))
            {
                if (Session["UserDeliveryState"] != null && !string.IsNullOrWhiteSpace(Session["UserDeliveryState"].ToString()))
                {
                    selectedState = Session["UserDeliveryState"].ToString();
                }
                else if (Session["UserState"] != null && !string.IsNullOrWhiteSpace(Session["UserState"].ToString()))
                {
                    selectedState = Session["UserState"].ToString();
                }
                else
                {
                    selectedState = "Delhi";
                }
            }

            // Persist customer delivery state in session
            Session["UserDeliveryState"] = selectedState;

            // Strict State Filtering: Only load restaurants matching the customer's state!
            var stores = _repo.GetStores(selectedState, city, search, false);

            var vm = new CustomerBrowseViewModel
            {
                SelectedState = selectedState,
                SelectedCity = city,
                SearchTerm = search,
                UserState = selectedState,
                StoresList = stores
            };

            ViewBag.ActiveTab = "Customer";
            return View(vm);
        }

        // GET: Customer/RestaurantMenu/5
        public ActionResult RestaurantMenu(int id)
        {
            var vm = _repo.GetStoreDetailsWithMenu(id);
            if (vm.Store == null)
            {
                TempData["ErrorMessage"] = "Restaurant not found.";
                return RedirectToAction("Index");
            }

            // Verify customer delivery state against restaurant state
            string customerState = Session["UserDeliveryState"] != null ? Session["UserDeliveryState"].ToString() : "Delhi";
            vm.CustomerState = customerState;

            if (vm.IsInterStateRestricted)
            {
                ViewBag.InterStateWarning = $"⚠️ Inter-State Ordering Restricted: You are currently located in {customerState}, but {vm.Store.StoreName} is located in {vm.Store.State}. Inter-state food deliveries are prohibited. Please switch your location to {vm.Store.State} to order from here.";
            }

            ViewBag.ActiveTab = "Customer";
            return View(vm);
        }

        // GET: Customer/Checkout
        [HttpGet]
        [CustomAuthorize]
        public ActionResult Checkout(int storeId, string cartJson, string deliveryState)
        {
            var store = _repo.GetStoreById(storeId);
            if (store == null)
            {
                TempData["ErrorMessage"] = "Store not found.";
                return RedirectToAction("Index");
            }

            // Resolve delivery state
            string customerState = deliveryState;
            if (string.IsNullOrWhiteSpace(customerState))
            {
                customerState = Session["UserDeliveryState"] != null ? Session["UserDeliveryState"].ToString() : store.State;
            }

            var vm = new CheckoutViewModel
            {
                StoreId = storeId,
                StoreName = store.StoreName,
                StoreState = store.State,
                DeliveryCity = store.City,
                DeliveryState = customerState,
                CustomerName = Session["UserName"] != null ? Session["UserName"].ToString() : "Komal Gupta",
                CustomerPhone = "9812345678",
                DeliveryAddress = "Flat 402, Sunshine Heights, " + store.City + ", " + customerState
            };

            // Inter-state check warning
            if (vm.IsInterStateRestricted)
            {
                ViewBag.InterStateWarning = $"Inter-State Ordering Restricted: Your delivery state is set to '{customerState}', but {store.StoreName} is in '{store.State}'. Orders cannot be fulfilled across state lines.";
            }

            // Parse cart items if supplied via JSON
            if (!string.IsNullOrWhiteSpace(cartJson))
            {
                try
                {
                    vm.Items = JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
                }
                catch
                {
                    vm.Items = new List<CartItem>();
                }
            }

            // Fallback default sample cart if empty
            if (vm.Items == null || vm.Items.Count == 0)
            {
                var menu = _repo.GetMenuItems(storeId);
                if (menu != null && menu.Count > 0)
                {
                    var item = menu.First();
                    vm.Items = new List<CartItem>
                    {
                        new CartItem { MenuItemId = item.MenuItemId, ItemName = item.ItemName, Price = item.Price, Quantity = 2, IsVeg = item.IsVeg }
                    };
                }
                else
                {
                    vm.Items = new List<CartItem>
                    {
                        new CartItem { MenuItemId = 1, ItemName = "Special Thali / Platter", Price = 299, Quantity = 2, IsVeg = true }
                    };
                }
            }

            // Calculations
            vm.TotalItemCount = vm.Items.Sum(i => i.Quantity);
            vm.ItemTotal = vm.Items.Sum(i => i.Subtotal);
            vm.TaxAmount = Math.Round(vm.ItemTotal * 0.05m, 2); // 5% GST
            vm.DeliveryFee = vm.ItemTotal > 500 ? 0m : 40m; // Free delivery over 500
            vm.TotalAmount = vm.ItemTotal + vm.TaxAmount + vm.DeliveryFee;

            // Allot preparation time based on item count:
            // <= 2 items -> 15 min
            // 3 to 5 (or 5+) items -> 30 min
            // > 10 items -> 60 min (1 hr)
            if (vm.TotalItemCount > 10)
                vm.PreparationTimeMinutes = 60;
            else if (vm.TotalItemCount >= 3)
                vm.PreparationTimeMinutes = 30;
            else
                vm.PreparationTimeMinutes = 15;

            vm.ItemsSummary = string.Join(", ", vm.Items.Select(i => i.Quantity + "x " + i.ItemName));
            ViewBag.ActiveTab = "Customer";
            return View(vm);
        }

        // POST: Customer/ProcessPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult ProcessPayment(CheckoutViewModel model)
        {
            if (model.StoreId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid store details.";
                return RedirectToAction("Index");
            }

            var store = _repo.GetStoreById(model.StoreId);
            if (store == null)
            {
                TempData["ErrorMessage"] = "Restaurant not found.";
                return RedirectToAction("Index");
            }

            // STRICT STATE-RESTRICTION LOGIC:
            // User cannot order sitting in Delhi from Gujarat (or any other state)!
            if (!string.IsNullOrWhiteSpace(model.DeliveryState) && !string.IsNullOrWhiteSpace(store.State))
            {
                if (!string.Equals(model.DeliveryState.Trim(), store.State.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    TempData["ErrorMessage"] = $"Inter-State Ordering Restricted: You are located in {model.DeliveryState}, but {store.StoreName} is in {store.State}. Under state food delivery regulations, you can only order from restaurants within your state ({model.DeliveryState}).";
                    return RedirectToAction("Checkout", new { storeId = model.StoreId, deliveryState = model.DeliveryState });
                }
            }

            int? customerId = null;
            if (Session["UserId"] != null)
            {
                customerId = Convert.ToInt32(Session["UserId"]);
            }

            // Execute order placement
            var receipt = _repo.PlaceCustomerOrder(model, customerId);

            // Check if rejected by DB stored procedure logic
            if (receipt.OrderId <= 0 || string.Equals(receipt.OrderStatus, "Rejected", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = !string.IsNullOrWhiteSpace(receipt.RejectionMessage) 
                    ? receipt.RejectionMessage 
                    : $"Inter-State Ordering Restricted: Delivery to {model.DeliveryState} from {store.State} is not permitted.";
                return RedirectToAction("Checkout", new { storeId = model.StoreId, deliveryState = model.DeliveryState });
            }

            TempData["SuccessMessage"] = "Payment successful! Order #" + receipt.OrderId + " confirmed.";
            return RedirectToAction("Receipt", new { id = receipt.OrderId });
        }

        // GET: Customer/Receipt/5
        public ActionResult Receipt(int id)
        {
            var receipt = _repo.GetOrderReceipt(id);
            if (receipt == null)
            {
                TempData["ErrorMessage"] = "Receipt not found for order #" + id;
                return RedirectToAction("MyOrders");
            }

            ViewBag.ActiveTab = "Customer";
            return View(receipt);
        }

        // GET: Customer/TrackOrder/5
        public ActionResult TrackOrder(int id)
        {
            var tracking = _repo.GetOrderForTracking(id);
            if (tracking == null)
            {
                TempData["ErrorMessage"] = "Order #" + id + " not found.";
                return RedirectToAction("MyOrders");
            }

            ViewBag.ActiveTab = "Customer";
            return View(tracking);
        }

        // GET: Customer/GetOrderStatus/5 (Real-time live status for polling and dynamic updates)
        [HttpGet]
        public ActionResult GetOrderStatus(int id)
        {
            var tracking = _repo.GetOrderForTracking(id);
            if (tracking == null)
            {
                return Json(new { success = false, message = "Order not found." }, JsonRequestBehavior.AllowGet);
            }

            // Real-time allotment rule:
            // Rider is allotted ONLY when food is packed (ReadyForPickup, OutForDelivery, Delivered)
            bool isRiderAllotted = tracking.OrderStatus == "ReadyForPickup" 
                                || tracking.OrderStatus == "OutForDelivery" 
                                || tracking.OrderStatus == "Delivered";

            int travelTime = 20;
            if (tracking.DistanceKm <= 2.0m) travelTime = 15;
            else if (tracking.DistanceKm <= 4.0m) travelTime = 25;
            else if (tracking.DistanceKm <= 6.5m) travelTime = 35;
            else travelTime = 45;

            // Resolve real coordinates based on city/state
            double rLat = 28.6315, rLng = 77.2167; // Delhi default
            double cLat = 28.6520, cLng = 77.2340;

            string cityOrState = (tracking.DeliveryCity ?? tracking.DeliveryState ?? "").ToLower();
            if (cityOrState.Contains("gujarat") || cityOrState.Contains("ahmedabad") || cityOrState.Contains("surat") || cityOrState.Contains("vadodara"))
            {
                rLat = 23.0225; rLng = 72.5714;
                cLat = 23.0380; cLng = 72.5520;
            }
            else if (cityOrState.Contains("maharashtra") || cityOrState.Contains("mumbai") || cityOrState.Contains("pune"))
            {
                rLat = 19.0596; rLng = 72.8295;
                cLat = 19.0760; cLng = 72.8777;
            }
            else if (cityOrState.Contains("rajasthan") || cityOrState.Contains("jaipur") || cityOrState.Contains("udaipur"))
            {
                rLat = 26.9124; rLng = 75.7873;
                cLat = 26.9230; cLng = 75.8200;
            }
            else if (cityOrState.Contains("uttar") || cityOrState.Contains("lucknow") || cityOrState.Contains("kanpur") || cityOrState.Contains("varanasi"))
            {
                rLat = 26.8467; rLng = 80.9462;
                cLat = 26.8580; cLng = 80.9650;
            }

            return Json(new
            {
                success = true,
                orderId = tracking.OrderId,
                orderStatus = tracking.OrderStatus,
                storeName = tracking.StoreName,
                customerName = tracking.CustomerName,
                deliveryAddress = tracking.DeliveryAddress,
                deliveryCity = tracking.DeliveryCity,
                preparationTimeMinutes = tracking.PreparationTimeMinutes,
                distanceKm = tracking.DistanceKm,
                travelTimeMinutes = travelTime,
                isRiderAllotted = isRiderAllotted,
                assignedAgentName = tracking.AssignedAgentName,
                assignedAgentPhone = tracking.AssignedAgentPhone,
                restaurantLat = rLat,
                restaurantLng = rLng,
                customerLat = cLat,
                customerLng = cLng
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: Customer/AdvanceStatus (Simulation helper for demonstration)
        [HttpPost]
        public ActionResult AdvanceStatus(int orderId)
        {
            var order = _repo.GetOrderForTracking(orderId);
            if (order == null)
            {
                return Json(new { success = false, message = "Order not found." });
            }

            string nextStatus = "Preparing";
            if (order.OrderStatus == "Placed")
                nextStatus = "Preparing";
            else if (order.OrderStatus == "Preparing")
                nextStatus = "ReadyForPickup"; // automatically assigns delivery agent!
            else if (order.OrderStatus == "ReadyForPickup")
                nextStatus = "OutForDelivery";
            else if (order.OrderStatus == "OutForDelivery")
                nextStatus = "Delivered";

            bool ok = _repo.UpdateOrderStatus(orderId, nextStatus);
            return Json(new { success = ok, newStatus = nextStatus, message = "Order transitioned to " + nextStatus });
        }

        // GET: Customer/MyOrders
        [HttpGet]
        [CustomAuthorize]
        public ActionResult MyOrders()
        {
            string userName = Session["UserName"] != null ? Session["UserName"].ToString() : null;
            int? userId = Session["UserId"] != null ? (int?)Convert.ToInt32(Session["UserId"]) : null;

            var orders = _repo.GetCustomerOrders(userName, userId);
            ViewBag.ActiveTab = "Customer";
            return View(orders);
        }

        // POST: Customer/SubmitReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult SubmitReview(int storeId, int rating, string comment, int? orderId)
        {
            string customerName = Session["UserName"] != null ? Session["UserName"].ToString() : "Verified Customer";
            int? customerId = Session["UserId"] != null ? (int?)Convert.ToInt32(Session["UserId"]) : null;

            bool success = _repo.AddReview(storeId, customerName, rating, comment, orderId, customerId);
            if (success)
            {
                TempData["SuccessMessage"] = "Thank you for your rating! Your review has been submitted.";
            }
            else
            {
                TempData["ErrorMessage"] = "Could not submit review. Please ensure rating is between 1 and 5.";
            }

            if (orderId.HasValue)
            {
                return RedirectToAction("MyOrders");
            }
            return RedirectToAction("RestaurantMenu", new { id = storeId });
        }
    }
}
