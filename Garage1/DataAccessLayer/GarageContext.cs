using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Garage1.DataAccessLayer
{
    public class GarageContext : DbContext
    {
        public GarageContext()
        {

        }

        public DbSet<Models.Garage> Garages { get; set; }
        public DbSet<Models.ParkedVehicle> ParkedVehicles { get; set; }
        public DbSet<Models.ParkingDetails> ParkingDetails { get; set; }
    }
}