using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage1.Models
{
    public class ParkingDetails
    {
        
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Parking Space")]
        public int ParkingSpace { get; set; }


        [Required]
        [Display(Name = "Check In Time")]
        public DateTime CheckInTime { get; set; }

        [Display(Name = "Parked Vehicle")]
        public virtual ParkedVehicle Vehicle { get; set; }
    }
}