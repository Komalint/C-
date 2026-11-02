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
        public ActionResult Login(string returnUrl, string role)
        {
            // If already logged in, redirect based on user role
            if (Session["UserId"] != null)
            {
                return RedirectToRoleHome(Session["UserRole"] != null ? Session["UserRole"].ToString() : "User");
            }

            ViewBag.ReturnUrl = returnUrl;
            var vm = new LoginViewModel
            {
                LoginRole = !string.IsNullOrWhiteSpace(role) ? role : "Customer"
            };
            return View(vm);
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

                // Verify "Login As" Role Compatibility
                if (!string.IsNullOrWhiteSpace(model.LoginRole))
                {
                    string selected = model.LoginRole.Trim();
                    string actual = (user.Role ?? "User").Trim();

                    if (selected.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!actual.Equals("User", StringComparison.OrdinalIgnoreCase) &&
                            !actual.Equals("Customer", StringComparison.OrdinalIgnoreCase) &&
                            !actual.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            ModelState.AddModelError("", $"Access Denied: This account is registered as '{actual}'. Please select 'Login As: {actual}' to access your portal.");
                            return View(model);
                        }
                    }
                    else if (selected.Equals("Restaurant", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!actual.Equals("Restaurant", StringComparison.OrdinalIgnoreCase) &&
                            !actual.Equals("Store", StringComparison.OrdinalIgnoreCase) &&
                            !actual.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            ModelState.AddModelError("", $"Access Denied: This account is registered as '{actual}'. Restaurant Partner credentials are required to login as Restaurant.");
                            return View(model);
                        }
                    }
                    else if (selected.Equals("DeliveryAgent", StringComparison.OrdinalIgnoreCase) || selected.Equals("Rider", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!actual.Equals("DeliveryAgent", StringComparison.OrdinalIgnoreCase) &&
                            !actual.Equals("Rider", StringComparison.OrdinalIgnoreCase) &&
                            !actual.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            ModelState.AddModelError("", $"Access Denied: This account is registered as '{actual}'. Delivery Partner credentials are required to login as Delivery Agent.");
                            return View(model);
                        }
                    }
                    else if (selected.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!actual.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            ModelState.AddModelError("", $"Access Denied: This account is registered as '{actual}'. Administrator privileges are required to login as Admin.");
                            return View(model);
                        }
                    }
                }

                // Authentication Successful: Initialize User Session
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;
                Session["UserEmail"] = user.Email;
                Session["UserRole"] = user.Role;
                Session["LoginRole"] = !string.IsNullOrWhiteSpace(model.LoginRole) ? model.LoginRole : user.Role;
                if (!string.IsNullOrWhiteSpace(user.State))
                {
                    Session["UserDeliveryState"] = user.State;
                }
                else if (Session["UserDeliveryState"] == null)
                {
                    Session["UserDeliveryState"] = "Delhi";
                }
                Session["LoginTime"] = DateTime.Now.ToString("dd MMM yyyy, hh:mm tt");

                TempData["SuccessMessage"] = $"Welcome back, {user.UserName}! Logged in as {(Session["LoginRole"] ?? user.Role)}.";

                // Redirect to returnUrl if valid, else Role-based redirection
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                string targetRole = !string.IsNullOrWhiteSpace(model.LoginRole) ? model.LoginRole : user.Role;
                return RedirectToRoleHome(targetRole);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while logging in: " + ex.Message);
                return View(model);
            }
        }

        // Helper for Role-Based Redirection
        private ActionResult RedirectToRoleHome(string role)
        {
            role = (role ?? "").Trim();
            if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "AdminFood");
            }
            if (role.Equals("Restaurant", StringComparison.OrdinalIgnoreCase) || role.Equals("Store", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Restaurant");
            }
            if (role.Equals("DeliveryAgent", StringComparison.OrdinalIgnoreCase) || role.Equals("Rider", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "DeliveryAgent");
            }
            return RedirectToAction("Index", "Customer");
        }

        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToRoleHome(Session["UserRole"] != null ? Session["UserRole"].ToString() : "User");
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
