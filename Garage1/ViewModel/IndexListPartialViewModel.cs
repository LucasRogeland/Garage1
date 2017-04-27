using Garage1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage1.ViewModel
{
    public class IndexListPartialViewModel
    {
        public IEnumerable<ParkedVehicle> Vehicles { get; set; }
        public string CssClassDesc { get; set; }
        public string Target { get; set; }
    }
}