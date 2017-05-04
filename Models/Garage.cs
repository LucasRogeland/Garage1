using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Garage
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int NrOfParkedVehicles { get; set; }
        public List<ParkingSpace> ParkingSpaces { get; set; }

    }
}
