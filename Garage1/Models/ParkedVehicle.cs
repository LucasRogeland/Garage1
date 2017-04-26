using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage1.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Garage1.Models
{
    public class ParkedVehicle
    {
        [Key]
        public string Licens { get; set; }
        public Vehicles VehicleType { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime CheckInTime { get; set; }
        public int NumberOfWheels { get; set; }
    }
}