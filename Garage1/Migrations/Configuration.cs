namespace Garage1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Garage1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage1.DataAccessLayer.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Garage1.DataAccessLayer.GarageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Vehicles.AddOrUpdate(
            //  p => p.Licens,
            //  new ParkedVehicle { Licens = "TEN-712", VehicleType = Enums.Vehicles.Car, Manufacturer = "Volvo", Model = "XC90", Color = "Black", NumberOfWheels = 4, CheckInTime = DateTime.Now },
            //  new ParkedVehicle { Licens = "HER-034", VehicleType = Enums.Vehicles.Car, Manufacturer = "BMW", Model = "M3", Color = "Red", NumberOfWheels = 4, CheckInTime = DateTime.Now },
            //  new ParkedVehicle { Licens = "FND-258", VehicleType = Enums.Vehicles.Car, Manufacturer = "Saab", Model = "9-3", Color = "Blue", NumberOfWheels = 4, CheckInTime = DateTime.Now }
            //);
            
        }
    }
}
