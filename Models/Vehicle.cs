using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vehicle
    {

        public int Id { get; set; }

        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        public string License { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; } 
    }
}
