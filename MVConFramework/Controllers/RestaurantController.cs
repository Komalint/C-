using System;
using System.Linq;
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

        // Helper to resolve current store id
        private int ResolveStoreId(int? storeId)
        {
            if (storeId.HasValue && storeId.Value > 0)
            {
                Session["CurrentRestaurantId"] = storeId.Value;
                return storeId.Value;
            }

            if (Session["CurrentRestaurantId"] != null)
            {
                return Convert.ToInt32(Session["CurrentRestaurantId"]);
            }

            // Default to first active store in DB
            var stores = _repo.GetStores(null, null, true);
            if (stores != null && stores.Count > 0)
            {
                int defaultId = stores.First().StoreId;
                Session["CurrentRestaurantId"] = defaultId;
                return defaultId;
            }

            return 1;
        }

        // GET: Restaurant / Dashboard
        public ActionResult Index(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var vm = _repo.GetRestaurantDashboard(sId);
            ViewBag.AllStores = _repo.GetStores(null, null, true);
            ViewBag.ActiveTab = "Restaurant";
            return View(vm);
        }

        // POST: Restaurant/UpdateOrderStatus
        [HttpPost]
        public ActionResult UpdateOrderStatus(int orderId, string status, int storeId)
        {
            bool ok = _repo.UpdateOrderStatus(orderId, status);
            if (ok)
            {
                TempData["SuccessMessage"] = "Order #" + orderId + " marked as " + status + ".";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update order status.";
            }
            return RedirectToAction("Index", new { storeId });
        }

        // GET: Restaurant/ManageMenu
        public ActionResult ManageMenu(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var store = _repo.GetStoreById(sId);
            var menu = _repo.GetMenuItems(sId);

            ViewBag.Store = store;
            ViewBag.AllStores = _repo.GetStores(null, null, true);
            ViewBag.ActiveTab = "Restaurant";
            return View(menu);
        }

        // POST: Restaurant/SaveMenuItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMenuItem(MenuItem item)
        {
            if (string.IsNullOrWhiteSpace(item.ItemName))
            {
                TempData["ErrorMessage"] = "Item name is required.";
                return RedirectToAction("ManageMenu", new { storeId = item.StoreId });
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
            return RedirectToAction("ManageMenu", new { storeId = item.StoreId });
        }

        // POST: Restaurant/DeleteMenuItem
        [HttpPost]
        public ActionResult DeleteMenuItem(int id, int storeId)
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
            return RedirectToAction("ManageMenu", new { storeId });
        }

        // GET: Restaurant/Invoices
        public ActionResult Invoices(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var vm = _repo.GetRestaurantDashboard(sId);
            ViewBag.AllStores = _repo.GetStores(null, null, true);
            ViewBag.ActiveTab = "Restaurant";
            return View(vm);
        }

        // GET: Restaurant/Reviews
        public ActionResult Reviews(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var store = _repo.GetStoreById(sId);
            var reviews = _repo.GetReviewsByStore(sId);

            ViewBag.Store = store;
            ViewBag.AllStores = _repo.GetStores(null, null, true);
            ViewBag.ActiveTab = "Restaurant";
            return View(reviews);
        }

        // POST: Restaurant/RespondReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RespondReview(int reviewId, string response, int storeId)
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
            return RedirectToAction("Reviews", new { storeId });
        }

        // GET: Restaurant/Certificate
        public ActionResult Certificate(int? storeId)
        {
            int sId = ResolveStoreId(storeId);
            var store = _repo.GetStoreById(sId);
            var cert = _repo.GetRestaurantCertificate(sId);

            ViewBag.Store = store;
            ViewBag.AllStores = _repo.GetStores(null, null, true);
            ViewBag.ActiveTab = "Restaurant";
            return View(cert);
        }
    }
}
