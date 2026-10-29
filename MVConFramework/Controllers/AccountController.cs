using System;
using System.Web.Mvc;
using MVConFramework.Models;
using MVConFramework.ViewModels;

namespace MVConFramework.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepo _userRepo = new UserRepo();

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            // If already logged in, redirect directly to Dashboard
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = _userRepo.ValidateUser(model.EmailOrUsername, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Username/Email or Password. Please try again.");
                    return View(model);
                }

                // Authentication Successful: Initialize User Session
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;
                Session["UserEmail"] = user.Email;
                Session["UserRole"] = user.Role;
                Session["LoginTime"] = DateTime.Now.ToString("dd MMM yyyy, hh:mm tt");

                TempData["SuccessMessage"] = $"Welcome back, {user.UserName}!";

                // Redirect to returnUrl if valid, else Dashboard
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while logging in: " + ex.Message);
                return View(model);
            }
        }

        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(new RegisterViewModel());
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string message;
                bool isRegistered = _userRepo.RegisterUser(model, out message);

                if (isRegistered)
                {
                    TempData["SuccessMessage"] = !string.IsNullOrEmpty(message)
                        ? message
                        : "Registration successful! Please login with your credentials.";

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", !string.IsNullOrEmpty(message) ? message : "Registration failed.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred during registration: " + ex.Message);
                return View(model);
            }
        }

        // GET or POST: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login", "Account");
        }
    }
}
