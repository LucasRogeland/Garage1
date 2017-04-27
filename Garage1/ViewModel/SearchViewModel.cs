using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage1.Enums;
namespace Garage1.ViewModel
{
    public class SearchViewModel
    {
        public string License { get; set; }
        public string Manufacturer { get; set; }
        public string VModel { get; set; }
        public string Color { get; set; }
        public Vehicles VehicleType { get; set; }
        public string CheckInTime { get; set; }
    }
}