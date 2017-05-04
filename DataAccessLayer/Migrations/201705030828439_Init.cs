namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Garages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                        NrOfParkedVehicles = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkingSpaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Occupied = c.Boolean(nullable: false),
                        Details_Id = c.Int(),
                        Garage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkingDetails", t => t.Details_Id)
                .ForeignKey("dbo.Garages", t => t.Garage_Id)
                .Index(t => t.Details_Id)
                .Index(t => t.Garage_Id);
            
            CreateTable(
                "dbo.ParkingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        ParkingSpaceId = c.Int(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        License = c.String(),
                        Make = c.String(),
                        Model = c.String(),
                        Color = c.String(),
                        GarageId = c.Int(nullable: false),
                        VehicleType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Garages", t => t.GarageId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleType_Id)
                .Index(t => t.GarageId)
                .Index(t => t.VehicleType_Id);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingSpaces", "Garage_Id", "dbo.Garages");
            DropForeignKey("dbo.ParkingSpaces", "Details_Id", "dbo.ParkingDetails");
            DropForeignKey("dbo.ParkingDetails", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "VehicleType_Id", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "GarageId", "dbo.Garages");
            DropForeignKey("dbo.ParkingDetails", "MemberId", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "VehicleType_Id" });
            DropIndex("dbo.Vehicles", new[] { "GarageId" });
            DropIndex("dbo.ParkingDetails", new[] { "VehicleId" });
            DropIndex("dbo.ParkingDetails", new[] { "MemberId" });
            DropIndex("dbo.ParkingSpaces", new[] { "Garage_Id" });
            DropIndex("dbo.ParkingSpaces", new[] { "Details_Id" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Members");
            DropTable("dbo.ParkingDetails");
            DropTable("dbo.ParkingSpaces");
            DropTable("dbo.Garages");
        }
    }
}
