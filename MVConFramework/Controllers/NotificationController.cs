using System;
using System.Web.Mvc;
using MVConFramework.Models;

namespace MVConFramework.Controllers
{
    public class NotificationController : Controller
    {
        private readonly FoodDeliveryRepo _repo = new FoodDeliveryRepo();

        [HttpGet]
        public ActionResult GetRecent(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                role = "Customer";

            int? userId = Session["UserId"] != null ? (int?)Convert.ToInt32(Session["UserId"]) : null;
            int? storeId = Session["CurrentRestaurantId"] != null ? (int?)Convert.ToInt32(Session["CurrentRestaurantId"]) : null;
            int? agentId = Session["CurrentAgentId"] != null ? (int?)Convert.ToInt32(Session["CurrentAgentId"]) : null;

            var list = _repo.GetNotifications(role, userId, storeId, agentId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MarkRead(int id)
        {
            bool ok = _repo.MarkNotificationRead(id);
            return Json(new { success = ok });
        }
    }
}
