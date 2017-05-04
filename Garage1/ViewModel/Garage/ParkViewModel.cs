using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace Garage1.ViewModel.Garage
{
    public class ParkViewModel
    {
        public string MemberName { get; set; }
        public ParkingDetails ParkingDetails { get; set; }
        public SelectList VehicleTypes { get; set; }
        public SelectList Garages { get; set; }
        public SelectList ParkingSpots { get; set; }
        public bool Feedback { get; set; }
        public string CssClass { get; set; }
        public string Message { get; set; }
    }
}