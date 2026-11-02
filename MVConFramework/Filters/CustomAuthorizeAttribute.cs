using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVConFramework.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Comma-separated list of allowed roles, e.g. "Admin" or "Restaurant,Admin" or "DeliveryAgent,Admin"
        /// If omitted or empty, any authenticated user is allowed.
        /// </summary>
        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            // 1. Authentication Check: Ensure user is logged in
            if (session == null || session["UserId"] == null)
            {
                string returnUrl = filterContext.HttpContext.Request.RawUrl;

                string requestedRole = "Customer";
                string prompt = "Authentication Required: Please log in before proceeding.";

                if (!string.IsNullOrWhiteSpace(Roles))
                {
                    if (Roles.IndexOf("Admin", StringComparison.OrdinalIgnoreCase) >= 0 && Roles.IndexOf("Restaurant", StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        requestedRole = "Admin";
                        prompt = "Authentication Required: Please log in as Administrator to access the Admin Portal.";
                    }
                    else if (Roles.IndexOf("Restaurant", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        requestedRole = "Restaurant";
                        prompt = "Authentication Required: Please log in as Restaurant Partner to access the Kitchen Dashboard.";
                    }
                    else if (Roles.IndexOf("DeliveryAgent", StringComparison.OrdinalIgnoreCase) >= 0 || Roles.IndexOf("Rider", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        requestedRole = "DeliveryAgent";
                        prompt = "Authentication Required: Please log in as Delivery Partner to access the Rider Portal.";
                    }
                    else
                    {
                        requestedRole = "Customer";
                        prompt = "Authentication Required: Please log in as Customer before placing an order or viewing your orders.";
                    }
                }
                else
                {
                    string path = (filterContext.HttpContext.Request.RawUrl ?? "").ToLower();
                    if (path.Contains("checkout") || path.Contains("order"))
                    {
                        requestedRole = "Customer";
                        prompt = "Authentication Required: Please log in before placing an order.";
                    }
                }

                filterContext.Controller.TempData["ErrorMessage"] = prompt;

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" },
                        { "returnUrl", returnUrl },
                        { "role", requestedRole }
                    });
                return;
            }

            // 2. Authorization Check: Role Verification
            if (!string.IsNullOrWhiteSpace(Roles))
            {
                string userRole = session["UserRole"] != null ? session["UserRole"].ToString().Trim() : "User";
                string[] allowedRoles = Roles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                bool isAuthorized = false;
                foreach (var r in allowedRoles)
                {
                    if (string.Equals(r.Trim(), userRole, StringComparison.OrdinalIgnoreCase))
                    {
                        isAuthorized = true;
                        break;
                    }
                }

                if (!isAuthorized)
                {
                    // User is logged in but lacks authorization for this role
                    filterContext.Controller.TempData["ErrorMessage"] = 
                        $"Access Denied: You do not have permission to access the {Roles} area. Your current role is '{userRole}'.";

                    // Redirect safely based on actual role
                    if (string.Equals(userRole, "Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "AdminFood" }, { "action", "Index" } });
                    }
                    else if (string.Equals(userRole, "Restaurant", StringComparison.OrdinalIgnoreCase))
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "Restaurant" }, { "action", "Index" } });
                    }
                    else if (string.Equals(userRole, "DeliveryAgent", StringComparison.OrdinalIgnoreCase) || string.Equals(userRole, "Rider", StringComparison.OrdinalIgnoreCase))
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "DeliveryAgent" }, { "action", "Index" } });
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "Customer" }, { "action", "Index" } });
                    }
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
