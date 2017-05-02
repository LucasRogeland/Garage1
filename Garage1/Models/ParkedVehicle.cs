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
        [Display(Name = "License")]
        public string Licens { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required]
        public Vehicles VehicleType { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Model { get; set; }
        public string Color { get; set; }

        [Range(0, 8)]
        public int NumberOfWheels { get; set; }
    }
}