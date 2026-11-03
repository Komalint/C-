using System;
using System.Web.Mvc;
using MVConFramework.Models;
using MVConFramework.ViewModels;

namespace MVConFramework.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepo _userRepo = new UserRepo();
        private readonly FoodDeliveryRepo _foodRepo = new FoodDeliveryRepo();

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

                // If logging in as Restaurant, bind their specific restaurant store
                string effectiveRole = !string.IsNullOrWhiteSpace(model.LoginRole) ? model.LoginRole : user.Role;
                if (effectiveRole.Equals("Restaurant", StringComparison.OrdinalIgnoreCase) || (user.Role != null && user.Role.Equals("Restaurant", StringComparison.OrdinalIgnoreCase)))
                {
                    var store = _foodRepo.GetStoreByOwnerUserId(user.UserId);
                    if (store != null)
                    {
                        Session["RestaurantStoreId"] = store.StoreId;
                        Session["RestaurantStoreName"] = store.StoreName;
                        Session["RestaurantStoreCity"] = store.City;
                        Session["RestaurantStoreState"] = store.State;
                        Session["RestaurantVerificationStatus"] = store.VerificationStatus;
                        Session["RestaurantIsActive"] = store.IsActive;

                        // Check authorization: if pending Super Admin approval or not active, direct to pending approval page
                        if (!store.IsActive || !store.VerificationStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                        {
                            TempData["InfoMessage"] = $"Your restaurant application for '{store.StoreName}' is currently pending Super Admin authorization.";
                            return RedirectToAction("PendingApproval", "Restaurant");
                        }
                    }
                    else
                    {
                        // Fallback to Royal Biryani House (Store 10)
                        Session["RestaurantStoreId"] = 10;
                        Session["RestaurantStoreName"] = "The Royal Biryani House";
                        Session["RestaurantStoreCity"] = "Delhi";
                        Session["RestaurantStoreState"] = "Delhi";
                    }
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
        public ActionResult Register(string role)
        {
            if (Session["UserId"] != null)
            {
                return RedirectToRoleHome(Session["UserRole"] != null ? Session["UserRole"].ToString() : "User");
            }

            var vm = new RegisterViewModel();
            if (!string.IsNullOrWhiteSpace(role))
            {
                vm.Role = role;
            }
            return View(vm);
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            // Validate role-specific requirements
            string effectiveRole = model.Role ?? "Customer";

            if (effectiveRole.Equals("Restaurant", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(model.RestaurantName))
                {
                    ModelState.AddModelError("RestaurantName", "Restaurant / Kitchen Name is required for restaurant partners.");
                }
                if (string.IsNullOrWhiteSpace(model.RestaurantAddress))
                {
                    ModelState.AddModelError("RestaurantAddress", "Physical kitchen address is required for food safety audits.");
                }
            }
            else if (effectiveRole.Equals("DeliveryAgent", StringComparison.OrdinalIgnoreCase) || effectiveRole.Equals("Rider", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(model.VehicleNumber))
                {
                    ModelState.AddModelError("VehicleNumber", "Vehicle registration number is required for delivery partners.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                int newUserId;
                string message;
                bool isRegistered = _userRepo.RegisterUser(model, out newUserId, out message);

                if (isRegistered)
                {
                    if (effectiveRole.Equals("Restaurant", StringComparison.OrdinalIgnoreCase))
                    {
                        int storeId;
                        string storeMsg;
                        _foodRepo.RegisterRestaurantStore(newUserId, model, out storeId, out storeMsg);

                        TempData["SuccessMessage"] = "Restaurant application submitted! Your kitchen is currently pending Super Admin authorization. Please login to track your status.";
                        return RedirectToAction("Login", new { role = "Restaurant" });
                    }
                    else if (effectiveRole.Equals("DeliveryAgent", StringComparison.OrdinalIgnoreCase) || effectiveRole.Equals("Rider", StringComparison.OrdinalIgnoreCase))
                    {
                        int agentId;
                        string agentMsg;
                        _foodRepo.RegisterDeliveryAgent(newUserId, model, out agentId, out agentMsg);

                        TempData["SuccessMessage"] = "Delivery Partner registered successfully! Please login to begin taking delivery orders.";
                        return RedirectToAction("Login", new { role = "DeliveryAgent" });
                    }
                    else
                    {
                        TempData["SuccessMessage"] = !string.IsNullOrEmpty(message)
                            ? message
                            : "Registration successful! Please login with your credentials.";
                        return RedirectToAction("Login", new { role = "Customer" });
                    }
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
