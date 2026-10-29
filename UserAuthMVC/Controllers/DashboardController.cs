using System;
using System.Web.Mvc;
using UserAuthMVC.Models;

namespace UserAuthMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserRepo _userRepo = new UserRepo();

        // GET: Dashboard/Index
        public ActionResult Index()
        {
            // Protect Dashboard: Check Session
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Please log in first to access your dashboard.";
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            UserModel user = _userRepo.GetUserById(userId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User session expired or user not found.";
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        // GET: Dashboard/UserProfile
        public ActionResult UserProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            UserModel user = _userRepo.GetUserById(userId);

            return View(user);
        }

        // GET: Dashboard/EditProfile
        public ActionResult EditProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            UserModel user = _userRepo.GetUserById(userId);

            return View(user);
        }

        // POST: Dashboard/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserModel model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                model.UserId = Convert.ToInt32(Session["UserId"]);
                bool success = _userRepo.UpdateUserProfile(model);

                if (success)
                {
                    Session["UserName"] = model.UserName;
                    Session["PhoneNumber"] = model.PhoneNumber;
                    Session["City"] = model.City;
                    Session["State"] = model.State;

                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to update profile. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating profile: " + ex.Message);
            }

            return View(model);
        }
    }
}
