using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
namespace Garage1.ViewModel.Garage
{
    public class ParkedViewModel
    {
        public bool Feedback { get; set; }
        public string CssClass { get; set; }
        public string Message { get; set; }
        public List<ParkingDetails> ParkingDetails { get; set; }
    }
}