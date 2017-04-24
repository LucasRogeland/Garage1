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

        public DbSet<Models.ParkedVehicle> Vehicles { get; set; }
    }
}