namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.GarageDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessLayer.GarageDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Garages.AddOrUpdate(
              p => p.Id,
              new Garage { Name = "South", Capacity = 5, NrOfParkedVehicles = 0}
            );

            context.ParkingSpaces.AddOrUpdate(
              p => p.Id,
              new ParkingSpace { Occupied = false, GarageId = 1},
              new ParkingSpace { Occupied = false, GarageId = 1 },
              new ParkingSpace { Occupied = false, GarageId = 1 },
              new ParkingSpace { Occupied = false, GarageId = 1 },
              new ParkingSpace { Occupied = false, GarageId = 1 }
            );

            context.Members.AddOrUpdate(
               p => p.Id,
               new Member { FirstName = "Lucas", LastName = "Rogeland" },
               new Member { FirstName = "Hans", LastName = "Ruin" }
             );

            context.VehicleTypes.AddOrUpdate(
               p => p.Id,
               new VehicleType { Name = "Car", Size = 2 },
               new VehicleType { Name = "Buss", Size = 6 },
               new VehicleType { Name = "Motorcycle", Size = 1 }
             );

        }
    }
}
