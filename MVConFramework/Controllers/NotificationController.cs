using System;
using System.Linq;
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
            {
                role = Session["LoginRole"] != null ? Session["LoginRole"].ToString() 
                     : (Session["UserRole"] != null ? Session["UserRole"].ToString() : "Customer");
            }

            int? userId = Session["UserId"] != null ? (int?)Convert.ToInt32(Session["UserId"]) : null;
            int? storeId = Session["RestaurantStoreId"] != null ? (int?)Convert.ToInt32(Session["RestaurantStoreId"])
                         : (Session["CurrentRestaurantId"] != null ? (int?)Convert.ToInt32(Session["CurrentRestaurantId"]) : null);

            // If storeId is still null but user is restaurant, look up by owner or default to Store 10
            if (!storeId.HasValue && role.Equals("Restaurant", StringComparison.OrdinalIgnoreCase))
            {
                if (userId.HasValue)
                {
                    var bound = _repo.GetStoreByOwnerUserId(userId.Value);
                    if (bound != null) storeId = bound.StoreId;
                }
                if (!storeId.HasValue)
                {
                    storeId = 10;
                }
            }

            int? agentId = Session["CurrentAgentId"] != null ? (int?)Convert.ToInt32(Session["CurrentAgentId"]) : null;

            var list = _repo.GetNotifications(role, userId, storeId, agentId);

            var formatted = list.Select(n => new
            {
                NotificationId = n.NotificationId,
                Title = SanitizeNotificationText(n.Title),
                Message = SanitizeNotificationText(n.Message),
                NotificationType = n.NotificationType,
                IsRead = n.IsRead,
                TimeAgo = GetTimeAgo(n.CreatedAt),
                CreatedAt = n.CreatedAt.ToString("hh:mm tt, dd MMM")
            }).ToList();

            int unreadCount = formatted.Count(n => !n.IsRead);

            return Json(new
            {
                success = true,
                unreadCount = unreadCount,
                notifications = formatted
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MarkRead(int id)
        {
            bool ok = _repo.MarkNotificationRead(id);
            return Json(new { success = ok });
        }

        [HttpPost]
        public ActionResult MarkAllRead(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                role = Session["LoginRole"] != null ? Session["LoginRole"].ToString() 
                     : (Session["UserRole"] != null ? Session["UserRole"].ToString() : "Customer");
            }

            int? userId = Session["UserId"] != null ? (int?)Convert.ToInt32(Session["UserId"]) : null;
            int? storeId = Session["RestaurantStoreId"] != null ? (int?)Convert.ToInt32(Session["RestaurantStoreId"])
                         : (Session["CurrentRestaurantId"] != null ? (int?)Convert.ToInt32(Session["CurrentRestaurantId"]) : null);

            bool ok = _repo.MarkAllNotificationsRead(role, storeId, userId);
            return Json(new { success = ok });
        }

        private static string SanitizeNotificationText(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return text.Replace("â‚¹", "\u20B9")
                       .Replace(",1", "\u20B9")
                       .Replace("ðŸ †", "🏆")
                       .Replace("dY?+", "🏆")
                       .Replace("₹˜…", "★")
                       .Replace("~.", "★");
        }

        private string GetTimeAgo(DateTime dt)
        {
            var span = DateTime.Now - dt;
            if (span.TotalSeconds < 60) return "Just now";
            if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes}m ago";
            if (span.TotalHours < 24) return $"{(int)span.TotalHours}h ago";
            return dt.ToString("dd MMM");
        }
    }
}
