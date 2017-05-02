namespace Garage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Garages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkingSpace = c.Int(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        Vehicle_Licens = c.String(nullable: false, maxLength: 128),
                        Garage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkedVehicles", t => t.Vehicle_Licens, cascadeDelete: true)
                .ForeignKey("dbo.Garages", t => t.Garage_Id)
                .Index(t => t.Vehicle_Licens)
                .Index(t => t.Garage_Id);
            
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        Licens = c.String(nullable: false, maxLength: 128),
                        VehicleType = c.Int(nullable: false),
                        Manufacturer = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Color = c.String(),
                        NumberOfWheels = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Licens);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingDetails", "Garage_Id", "dbo.Garages");
            DropForeignKey("dbo.ParkingDetails", "Vehicle_Licens", "dbo.ParkedVehicles");
            DropIndex("dbo.ParkingDetails", new[] { "Garage_Id" });
            DropIndex("dbo.ParkingDetails", new[] { "Vehicle_Licens" });
            DropTable("dbo.ParkedVehicles");
            DropTable("dbo.ParkingDetails");
            DropTable("dbo.Garages");
        }
    }
}
