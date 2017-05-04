using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ParkingDetails
    {
        public int Id { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int ParkingSpaceId { get; set; }
        public ParkingSpace ParkingSpace;

        public DateTime CheckInTime { get; set; }
    }
}
