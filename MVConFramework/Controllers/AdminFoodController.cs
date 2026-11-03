using System;
using System.Web.Mvc;
using MVConFramework.Filters;
using MVConFramework.Models;
using MVConFramework.ViewModels;

namespace MVConFramework.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AdminFoodController : Controller
    {
        private readonly FoodDeliveryRepo _repo = new FoodDeliveryRepo();

        // GET: AdminFood
        public ActionResult Index(string state, string city)
        {
            var vm = _repo.GetAdminAnalytics(state, city);
            ViewBag.ActiveTab = "Admin";
            return View(vm);
        }

        // POST: AdminFood/VerifyFoodSafety
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyFoodSafety(int storeId, string fssaiLicense, int hygieneScore, string waterCert, string pestCert, string fireNoc, string remarks, string action)
        {
            int? adminId = Session["UserId"] != null ? (int?)Convert.ToInt32(Session["UserId"]) : 1;

            bool ok = _repo.VerifyFoodSafety(storeId, fssaiLicense, hygieneScore, waterCert, pestCert, fireNoc, remarks, action, adminId);
            if (ok)
            {
                TempData["SuccessMessage"] = "Restaurant food safety verification audit has been recorded (" + action + ").";
            }
            else
            {
                TempData["ErrorMessage"] = "Could not record food safety verification.";
            }

            return RedirectToAction("Index");
        }

        // POST: AdminFood/QuickAuthorize
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuickAuthorize(int storeId, string action)
        {
            int adminId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 1;
            bool ok = _repo.QuickAuthorizeStore(storeId, action, adminId);
            if (ok)
            {
                TempData["SuccessMessage"] = action.Equals("Approve", StringComparison.OrdinalIgnoreCase)
                    ? "Restaurant authorized and activated successfully! The owner can now access their Kitchen Dashboard."
                    : "Restaurant application has been rejected/held.";
            }
            else
            {
                TempData["ErrorMessage"] = "Could not update restaurant authorization.";
            }
            return RedirectToAction("Index");
        }

        // GET: AdminFood/DatabaseHealth
        public ActionResult DatabaseHealth()
        {
            var status = _repo.CheckDatabasesHealth();
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
