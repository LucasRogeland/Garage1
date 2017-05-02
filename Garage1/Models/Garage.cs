using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Garage1.Models
{
    public class Garage
    {

        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }

        public virtual ICollection<ParkingDetails> ParkingDetails { get; set; }

    }
}