using System;
using System.Linq;
using System.Web.Mvc;
using MVConFramework.Filters;
using MVConFramework.Models;

namespace MVConFramework.Controllers
{
    [CustomAuthorize(Roles = "DeliveryAgent,Rider,Admin")]
    public class DeliveryAgentController : Controller
    {
        private readonly FoodDeliveryRepo _repo = new FoodDeliveryRepo();

        private int ResolveAgentId(int? agentId)
        {
            string role = Session["UserRole"] != null ? Session["UserRole"].ToString() : "";
            bool isAdmin = role.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            // If user is a Rider/DeliveryAgent, strictly resolve from their own logged-in user profile
            if (!isAdmin && Session["UserId"] != null)
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                var myAgent = _repo.GetDeliveryAgentByUserId(userId);
                if (myAgent != null)
                {
                    Session["CurrentAgentId"] = myAgent.AgentId;
                    return myAgent.AgentId;
                }
            }

            // For Admin only, allow switching via agentId
            if (isAdmin && agentId.HasValue && agentId.Value > 0)
            {
                Session["CurrentAgentId"] = agentId.Value;
                return agentId.Value;
            }

            if (Session["CurrentAgentId"] != null)
            {
                return Convert.ToInt32(Session["CurrentAgentId"]);
            }

            // Fallback: Agent 1 (Rajesh Kumar)
            return 1;
        }

        // GET: DeliveryAgent / Dashboard
        [CustomAuthorize(Roles = "DeliveryAgent,Rider,Admin")]
        public ActionResult Index(int? agentId)
        {
            int aId = ResolveAgentId(agentId);
            var vm = _repo.GetDeliveryAgentDashboard(aId);
            // DO NOT expose all agents to riders to guarantee rider order and privacy isolation
            string role = Session["UserRole"] != null ? Session["UserRole"].ToString() : "";
            if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.AllAgents = _repo.GetDeliveryAgents();
            }
            ViewBag.ActiveTab = "DeliveryAgent";
            return View(vm);
        }

        // GET: DeliveryAgent/Enroll
        [HttpGet]
        public ActionResult Enroll()
        {
            ViewBag.ActiveTab = "DeliveryAgent";
            return View(new DeliveryAgent());
        }

        // POST: DeliveryAgent/Enroll
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll(DeliveryAgent agent)
        {
            if (string.IsNullOrWhiteSpace(agent.FullName) || string.IsNullOrWhiteSpace(agent.PhoneNumber) || string.IsNullOrWhiteSpace(agent.AadhaarNumber))
            {
                TempData["ErrorMessage"] = "Please fill in all required fields including Full Name, Phone, and Aadhaar.";
                return View(agent);
            }

            int? userId = Session["UserId"] != null ? (int?)Convert.ToInt32(Session["UserId"]) : null;
            agent.UserId = userId;

            string message;
            bool ok = _repo.RegisterDeliveryAgent(agent, out message);
            if (ok)
            {
                TempData["SuccessMessage"] = message + " Your account is now Active and Online.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = message;
                return View(agent);
            }
        }

        // POST: DeliveryAgent/ToggleStatus
        [HttpPost]
        [CustomAuthorize(Roles = "DeliveryAgent,Rider,Admin")]
        public ActionResult ToggleStatus(int agentId, bool isActive)
        {
            bool ok = _repo.ToggleDeliveryAgentStatus(agentId, isActive);
            if (ok)
            {
                TempData["SuccessMessage"] = isActive ? "You are now Online and ready to receive food delivery orders." : "You are now Offline.";
            }
            return RedirectToAction("Index", new { agentId });
        }

        // POST: DeliveryAgent/CompleteDelivery (Strict Customer OTP Verification)
        [HttpPost]
        [CustomAuthorize(Roles = "DeliveryAgent,Rider,Admin")]
        public ActionResult CompleteDelivery(int orderId, int agentId, string otp)
        {
            if (string.IsNullOrWhiteSpace(otp))
            {
                TempData["ErrorMessage"] = "Please enter the 4-digit Delivery OTP provided by the customer at the doorstep.";
                return RedirectToAction("Index", new { agentId });
            }

            string message;
            bool ok = _repo.VerifyDeliveryOtp(orderId, agentId, otp.Trim(), out message);
            if (ok)
            {
                TempData["SuccessMessage"] = "Delivery OTP Verified! Order #" + orderId + " delivered successfully. ₹60 delivery fee credited to your wallet.";
            }
            else
            {
                TempData["ErrorMessage"] = !string.IsNullOrEmpty(message) ? message : "Invalid Delivery OTP. Please verify with the customer.";
            }
            return RedirectToAction("Index", new { agentId });
        }

        // POST: DeliveryAgent/VerifyDeliveryOtpAjax
        [HttpPost]
        [CustomAuthorize(Roles = "DeliveryAgent,Rider,Admin")]
        public JsonResult VerifyDeliveryOtpAjax(int orderId, int agentId, string otp)
        {
            if (string.IsNullOrWhiteSpace(otp))
            {
                return Json(new { success = false, message = "Please enter the 4-digit Delivery OTP." });
            }

            string message;
            bool ok = _repo.VerifyDeliveryOtp(orderId, agentId, otp.Trim(), out message);
            return Json(new { success = ok, message = message });
        }
    }
}
