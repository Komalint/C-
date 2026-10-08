using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagementSystem.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal OdometerReading { get; set; }
        public string IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
