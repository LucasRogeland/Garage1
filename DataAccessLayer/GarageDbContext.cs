using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class GarageDbContext : DbContext
    {
        public DbSet<Models.Garage> Garages { get; set; }
        public DbSet<Models.ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<Models.ParkingDetails> ParkingDetails { get; set; }
        public DbSet<Models.Vehicle> Vehicles { get; set; }
        public DbSet<Models.VehicleType> VehicleTypes { get; set; }
        public DbSet<Models.Member> Members { get; set; }
    }
}
