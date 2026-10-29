using System;
using System.Web.Mvc;
using MVConFramework.Models;
using MVConFramework.ViewModels;

namespace MVConFramework.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserRepo _userRepo = new UserRepo();

        // GET: Dashboard
        public ActionResult Index()
        {
            // Simple Session-based Authentication Guard
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Please log in to access the Dashboard.";
                return RedirectToAction("Login", "Account");
            }

            var stats = _userRepo.GetDashboardStats();

            stats.CurrentUserName = Session["UserName"] != null ? Session["UserName"].ToString() : "User";
            stats.CurrentUserEmail = Session["UserEmail"] != null ? Session["UserEmail"].ToString() : "";
            stats.CurrentUserRole = Session["UserRole"] != null ? Session["UserRole"].ToString() : "User";

            return View(stats);
        }
    }
}
