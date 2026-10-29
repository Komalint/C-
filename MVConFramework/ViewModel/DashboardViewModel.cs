using System.Collections.Generic;
using MVConFramework.Models;

namespace MVConFramework.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalStores { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalUsers { get; set; }

        public string CurrentUserName { get; set; }
        public string CurrentUserEmail { get; set; }
        public string CurrentUserRole { get; set; }

        public List<Orders> RecentOrders { get; set; } = new List<Orders>();
    }
}
