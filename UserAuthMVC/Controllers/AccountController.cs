using System;
using System.Web.Mvc;
using System.Web.Security;
using UserAuthMVC.Models;

namespace UserAuthMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepo _userRepo = new UserRepo();

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            // If already logged in, redirect directly to dashboard
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
                UserModel user = _userRepo.ValidateUser(model.EmailOrUsername, model.Password);

                if (user != null)
                {
                    // Authentication successful: Set Session Variables
                    Session["UserId"] = user.UserId;
                    Session["UserName"] = user.UserName;
                    Session["Email"] = user.Email;
                    Session["Role"] = user.Role;
                    Session["PhoneNumber"] = user.PhoneNumber;
                    Session["City"] = user.City;
                    Session["State"] = user.State;

                    if (model.RememberMe)
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, true);
                    }

                    TempData["SuccessMessage"] = $"Welcome back, {user.UserName}! You have successfully logged in.";

                    // Redirect to ReturnUrl if valid, else Dashboard
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email/Username or Password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Database connection error: " + ex.Message);
            }

            return View(model);
        }

        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            // If already logged in, redirect to dashboard
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
                int newUserId = _userRepo.RegisterUser(model, out message);

                if (newUserId > 0)
                {
                    TempData["SuccessMessage"] = message + " Please log in with your credentials.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", message);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Registration failed due to server error: " + ex.Message);
            }

            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            TempData["InfoMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }
    }
}
