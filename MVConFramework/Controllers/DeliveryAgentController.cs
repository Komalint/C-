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
            if (agentId.HasValue && agentId.Value > 0)
            {
                Session["CurrentAgentId"] = agentId.Value;
                return agentId.Value;
            }

            if (Session["CurrentAgentId"] != null)
            {
                return Convert.ToInt32(Session["CurrentAgentId"]);
            }

            var agents = _repo.GetDeliveryAgents();
            if (agents != null && agents.Count > 0)
            {
                int defaultId = agents.First().AgentId;
                Session["CurrentAgentId"] = defaultId;
                return defaultId;
            }

            return 1;
        }

        // GET: DeliveryAgent / Dashboard
        [CustomAuthorize(Roles = "DeliveryAgent,Rider,Admin")]
        public ActionResult Index(int? agentId)
        {
            int aId = ResolveAgentId(agentId);
            var vm = _repo.GetDeliveryAgentDashboard(aId);
            ViewBag.AllAgents = _repo.GetDeliveryAgents();
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

        // POST: DeliveryAgent/CompleteDelivery
        [HttpPost]
        [CustomAuthorize(Roles = "DeliveryAgent,Rider,Admin")]
        public ActionResult CompleteDelivery(int orderId, int agentId)
        {
            bool ok = _repo.UpdateOrderStatus(orderId, "Delivered");
            if (ok)
            {
                TempData["SuccessMessage"] = "Order #" + orderId + " marked as Delivered! ₹60 delivery fee has been credited to your earnings.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update order status.";
            }
            return RedirectToAction("Index", new { agentId });
        }
    }
}
