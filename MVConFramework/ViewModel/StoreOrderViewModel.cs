using System.Collections.Generic;
using MVConFramework.Models;

namespace MVConFramework.ViewModels
{
    public class StoreOrderViewModel
    {
        public Stores Store { get; set; }
        public Orders Order { get; set; }

        public List<Orders> Orders { get; set; }

        public List<Stores> Stores { get; set; }
    }
}