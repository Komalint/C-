using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVConFramework.Filters;
using MVConFramework.Models;
using MVConFramework.ViewModels;

namespace MVConFramework.Controllers
{
    [CustomAuthorize(Roles = "Restaurant,Admin")]
    public class RestaurantController : Controller
    {
        private readonly FoodDeliveryRepo _repo = new FoodDeliveryRepo();

        // Multi-tenant Store Resolver: Restaurant partners can ONLY access their own restaurant
        private int ResolveStoreId(int? storeId = null)
        {
            string role = (Session["UserRole"] != null ? Session["UserRole"].ToString() : "").Trim();
            string loginRole = (Session["LoginRole"] != null ? Session["LoginRole"].ToString() : role).Trim();
            int userId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0;

            // Strict Multi-Tenant Enforcement for Restaurant Role:
            if (role.Equals("Restaurant", StringComparison.OrdinalIgnoreCase) || 
                loginRole.Equals("Restaurant", StringComparison.OrdinalIgnoreCase))
            {
                if (Session["RestaurantStoreId"] != null)
                {
                    return Convert.ToInt32(Session["RestaurantStoreId"]);
                }

                if (userId > 0)
                {
                    var boundStore = _repo.GetStoreByOwnerUserId(userId);
                    if (boundStore != null)
                    {
                        Session["RestaurantStoreId"] = boundStore.StoreId;
                        Session["RestaurantStoreName"] = boundStore.StoreName;
                        Session["RestaurantStoreCity"] = boundStore.City;
                        Session["RestaurantStoreState"] = boundStore.State;
                        return boundStore.StoreId;
                    }
                }

                // Default fallback for Chef Farhan
                Session["RestaurantStoreId"] = 10;
                Session["RestaurantStoreName"] = "The Royal Biryani House";
                Session["RestaurantStoreCity"] = "Delhi";
                Session["RestaurantStoreState"] = "Delhi";
                return 10;
            }

            // For Admin users only: Allow inspecting specific store if provided
            if (storeId.HasValue && storeId.Value > 0)
            {
                Session["CurrentRestaurantId"] = storeId.Value;
                return storeId.Value;
            }

            if (Session["CurrentRestaurantId"] != null)
            {
                return Convert.ToInt32(Session["CurrentRestaurantId"]);
            }

            return 10;
        }

        private bool IsCurrentStoreAuthorized()
        {
            string role = (Session["UserRole"] != null ? Session["UserRole"].ToString() : "").Trim();
            string loginRole = (Session["LoginRole"] != null ? Session["LoginRole"].ToString() : role).Trim();
            int userId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0;

            if (role.Equals("Restaurant", StringComparison.OrdinalIgnoreCase) || loginRole.Equals("Restaurant", StringComparison.OrdinalIgnoreCase))
            {
                var store = _repo.GetStoreByOwnerUserId(userId);
                if (store != null)
                {
                    if (!store.IsActive || !store.VerificationStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // GET: Restaurant / Dashboard
        public ActionResult Index(int? storeId)
        {
            if (!IsCurrentStoreAuthorized())
            {
                return RedirectToAction("PendingApproval");
            }

            int sId = ResolveStoreId(storeId);
            var vm = _repo.GetRestaurantDashboard(sId);

            if (vm.Store != null)
            {
                Session["RestaurantStoreId"] = vm.Store.StoreId;
                Session["RestaurantStoreName"] = vm.Store.StoreName;
                Session["RestaurantStoreCity"] = vm.Store.City;
                Session["RestaurantStoreState"] = vm.Store.State;
            }

            ViewBag.ActiveTab = "RestaurantOrders";
            return View(vm);
        }

        // Real-Time Kitchen Updates AJAX Polling Endpoint
        [HttpGet]
        public JsonResult GetLiveKitchenUpdates(int? lastKnownOrderId)
        {
            try
            {
                int sId = ResolveStoreId();
                var vm = _repo.GetRestaurantDashboard(sId);

                int lastId = lastKnownOrderId ?? 0;
                var newOrders = vm.LiveOrders
                    .Where(o => o.OrderId > lastId)
                    .OrderByDescending(o => o.OrderId)
                    .Select(o => new
                    {
                        OrderId = o.OrderId,
                        CustomerName = o.CustomerName,
                        DeliveryAddress = !string.IsNullOrWhiteSpace(o.DeliveryAddress) ? o.DeliveryAddress : "Customer Address",
                        DeliveryCity = o.DeliveryCity,
                        ProductName = o.ProductName,
                        Quantity = o.Quantity,
                        TotalAmount = o.TotalAmount,
                        PrepTimeMinutes = o.PreparationTimeMinutes,
                        OrderStatus = o.OrderStatus,
                        AssignedAgentName = !string.IsNullOrEmpty(o.AssignedAgentName) ? o.AssignedAgentName : "Assigning upon packing",
                        AssignedAgentPhone = o.AssignedAgentPhone ?? "",
                        OrderDate = o.OrderDate.ToString("hh:mm tt, dd MMM")
                    }).ToList();

                int incomingCount = vm.LiveOrders.Count(o => o.OrderStatus == "Placed" || o.OrderStatus == "Preparing");
                int outForDeliveryCount = vm.LiveOrders.Count(o => o.OrderStatus == "ReadyForPickup" || o.OrderStatus == "OutForDelivery");
                int deliveredFromList = vm.LiveOrders.Count(o => o.OrderStatus == "Delivered");
                int completedCount = Math.Max(vm.CompletedOrders, deliveredFromList);
                int totalCount = vm.LiveOrders.Count;
                bool isEligible = completedCount >= 200 && vm.AverageRating >= 3.5m;
                int ordersNeeded = Math.Max(0, 200 - completedCount);

                return Json(new
                {
                    success = true,
                    storeId = sId,
                    storeName = vm.Store != null ? vm.Store.StoreName : "",
                    incomingCount = incomingCount,
                    outForDeliveryCount = outForDeliveryCount,
                    completedCount = completedCount,
                    completedOrders = completedCount,
                    isCertificateEligible = isEligible,
                    ordersNeeded = ordersNeeded,
                    averageRating = vm.AverageRating,
                    totalCount = totalCount,
                    grossSales = vm.GrossSales,
                    platformFee = vm.PlatformFeeDeducted,
                    netPayout = vm.NetPayout,
                    newOrders = newOrders,
                    maxOrderId = vm.LiveOrders.Any() ? vm.LiveOrders.Max(o => o.OrderId) : 0
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Restaurant/UpdateOrderStatus
        [HttpPost]
        public ActionResult UpdateOrderStatus(int orderId, string status)
        {
            int sId = ResolveStoreId();
            bool ok = _repo.UpdateOrderStatus(orderId, status);
            if (ok)
            {
                if (status == "ReadyForPickup")
                {
                    TempData["SuccessMessage"] = $"Order #{orderId} marked as Packed & Ready! An active delivery rider has been auto-assigned from the fleet.";
                }
                else if (status == "Delivered")
                {
                    TempData["SuccessMessage"] = $"Order #{orderId} confirmed Delivered! ₹60 delivery fee paid to rider and 93% net payout credited to your restaurant.";
                }
                else
                {
                    TempData["SuccessMessage"] = $"Order #{orderId} status updated to {status}.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update order status.";
            }
            return RedirectToAction("Index");
        }

        // GET: Restaurant/ManageMenu
        public ActionResult ManageMenu(int? storeId)
        {
            if (!IsCurrentStoreAuthorized())
            {
                return RedirectToAction("PendingApproval");
            }

            int sId = ResolveStoreId(storeId);
            var store = _repo.GetStoreById(sId);
            var menu = _repo.GetMenuItems(sId);

            ViewBag.Store = store;
            ViewBag.ActiveTab = "RestaurantMenu";
            return View(menu);
        }

        // POST: Restaurant/SaveMenuItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMenuItem(MenuItem item)
        {
            int sId = ResolveStoreId();
            item.StoreId = sId; // Multi-tenant guarantee: force item to belong to this store

            if (string.IsNullOrWhiteSpace(item.ItemName))
            {
                TempData["ErrorMessage"] = "Item name is required.";
                return RedirectToAction("ManageMenu");
            }

            bool ok = _repo.AddOrUpdateMenuItem(item);
            if (ok)
            {
                TempData["SuccessMessage"] = "Menu item '" + item.ItemName + "' saved successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Could not save menu item.";
            }
            return RedirectToAction("ManageMenu");
        }

        // POST: Restaurant/DeleteMenuItem
        [HttpPost]
        public ActionResult DeleteMenuItem(int id)
        {
            bool ok = _repo.DeleteMenuItem(id);
            if (ok)
            {
                TempData["SuccessMessage"] = "Menu item deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Could not delete menu item.";
            }
            return RedirectToAction("ManageMenu");
        }

        // GET: Restaurant/Invoices
        public ActionResult Invoices(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var vm = _repo.GetRestaurantDashboard(sId);
            ViewBag.ActiveTab = "RestaurantInvoices";
            return View(vm);
        }

        // GET: Restaurant/Reviews
        public ActionResult Reviews(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var store = _repo.GetStoreById(sId);
            var reviews = _repo.GetReviewsByStore(sId);

            ViewBag.Store = store;
            ViewBag.ActiveTab = "RestaurantReviews";
            return View(reviews);
        }

        // POST: Restaurant/RespondReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RespondReview(int reviewId, string response)
        {
            if (string.IsNullOrWhiteSpace(response))
            {
                TempData["ErrorMessage"] = "Response cannot be blank.";
            }
            else
            {
                bool ok = _repo.RespondToReview(reviewId, response);
                if (ok)
                {
                    TempData["SuccessMessage"] = "Your response has been posted.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not post response.";
                }
            }
            return RedirectToAction("Reviews");
        }

        // GET: Restaurant/Certificate
        public ActionResult Certificate(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var store = _repo.GetStoreById(sId);
            if (store == null)
            {
                TempData["ErrorMessage"] = "Restaurant profile not found.";
                return RedirectToAction("Index");
            }

            bool isEligible = store.TotalOrdersCompleted >= 200 && store.Rating >= 3.5m;
            RestaurantCertificate cert = null;
            if (isEligible)
            {
                cert = _repo.GetRestaurantCertificate(sId);
            }

            ViewBag.Store = store;
            ViewBag.IsEligible = isEligible;
            ViewBag.OrdersCompleted = store.TotalOrdersCompleted;
            ViewBag.Rating = store.Rating;
            ViewBag.OrdersRemaining = Math.Max(0, 200 - store.TotalOrdersCompleted);
            ViewBag.ActiveTab = "RestaurantCert";
            return View(cert);
        }

        // GET: Restaurant/DownloadCertificate (Guarded against ineligible restaurants)
        public ActionResult DownloadCertificate(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var store = _repo.GetStoreById(sId);
            if (store == null || store.TotalOrdersCompleted < 200 || store.Rating < 3.5m)
            {
                TempData["ErrorMessage"] = "You are not eligible for certification. A minimum of 200 completed orders and a 3.5+ star rating are required.";
                return RedirectToAction("Certificate", new { storeId = sId });
            }

            return RedirectToAction("Certificate", new { storeId = sId, print = 1 });
        }

        // GET: Restaurant/EditProfile (Kitchen Profile, Operating Hours & Banner Upload)
        public ActionResult EditProfile(int? storeId)
        {
            if (!IsCurrentStoreAuthorized())
            {
                return RedirectToAction("PendingApproval");
            }

            int sId = ResolveStoreId(storeId);
            var store = _repo.GetStoreById(sId);
            if (store == null)
            {
                TempData["ErrorMessage"] = "Restaurant profile not found.";
                return RedirectToAction("Index");
            }
            ViewBag.ActiveTab = "RestaurantSettings";
            return View(store);
        }

        // POST: Restaurant/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Stores model, HttpPostedFileBase bannerFile, HttpPostedFileBase logoFile)
        {
            int sId = ResolveStoreId();
            model.StoreId = sId; // Multi-tenant guarantee

            // Handle Banner File Upload
            if (bannerFile != null && bannerFile.ContentLength > 0)
            {
                try
                {
                    string uploadDir = Server.MapPath("~/Uploads/Banners");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }
                    string ext = Path.GetExtension(bannerFile.FileName);
                    string fileName = $"banner_{sId}_{DateTime.Now.Ticks}{ext}";
                    string fullPath = Path.Combine(uploadDir, fileName);
                    bannerFile.SaveAs(fullPath);
                    model.BannerUrl = "/Uploads/Banners/" + fileName;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Could not upload banner file: " + ex.Message;
                }
            }

            // Handle Logo / Image File Upload
            if (logoFile != null && logoFile.ContentLength > 0)
            {
                try
                {
                    string uploadDir = Server.MapPath("~/Uploads/Logos");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }
                    string ext = Path.GetExtension(logoFile.FileName);
                    string fileName = $"logo_{sId}_{DateTime.Now.Ticks}{ext}";
                    string fullPath = Path.Combine(uploadDir, fileName);
                    logoFile.SaveAs(fullPath);
                    model.ImageUrl = "/Uploads/Logos/" + fileName;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Could not upload logo file: " + ex.Message;
                }
            }

            bool ok = _repo.UpdateStoreProfile(model);
            if (ok)
            {
                Session["RestaurantStoreName"] = model.StoreName;
                Session["RestaurantStoreCity"] = model.City;
                Session["RestaurantStoreState"] = model.State;
                TempData["SuccessMessage"] = $"Kitchen profile and operating hours ({model.OpeningTime} - {model.ClosingTime}) updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Could not update restaurant profile.";
            }

            return RedirectToAction("EditProfile");
        }

        // GET: Restaurant/PendingApproval
        public ActionResult PendingApproval()
        {
            int userId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0;
            var store = _repo.GetStoreByOwnerUserId(userId);
            if (store == null && Session["RestaurantStoreId"] != null)
            {
                store = _repo.GetStoreById(Convert.ToInt32(Session["RestaurantStoreId"]));
            }

            if (store != null && store.IsActive && store.VerificationStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index");
            }

            ViewBag.ActiveTab = "PendingApproval";
            return View(store);
        }

        // AJAX: Restaurant/CheckApprovalStatus
        [HttpGet]
        public JsonResult CheckApprovalStatus()
        {
            int userId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0;
            var store = _repo.GetStoreByOwnerUserId(userId);
            if (store == null && Session["RestaurantStoreId"] != null)
            {
                store = _repo.GetStoreById(Convert.ToInt32(Session["RestaurantStoreId"]));
            }

            bool isApproved = store != null && store.IsActive && store.VerificationStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase);
            return Json(new
            {
                isApproved = isApproved,
                storeName = store?.StoreName,
                status = store?.VerificationStatus ?? "PendingApproval"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
