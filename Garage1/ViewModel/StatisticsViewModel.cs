using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage1.ViewModel
{
    public class StatisticsViewModel
    {
        public int NrOfParkedVehicles { get; set; }
        public int NrOfParkedCars { get; set; }
        public int NrOfParkedMotorcycle { get; set; }
        public int NrOfParkedBuss { get; set; }
        public int NrOfWheels { get; set; }
        public double TotalCost { get; set; }
    }
}