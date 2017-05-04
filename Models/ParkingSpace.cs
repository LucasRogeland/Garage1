using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public bool Occupied { get; set; } 
        public ParkingDetails Details { get; set; }

        public int GarageId { get; set; }
        public Garage Garage { get; set; }
    }
}
