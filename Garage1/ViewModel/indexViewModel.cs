using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage1.Models;

namespace Garage1.ViewModel
{
    public class IndexViewModel
    {  
        public IndexViewModel()
        {
            Feedback = false;
        }

        public List<ParkedVehicle> Vehicles { get; set; }
        public bool Feedback { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}