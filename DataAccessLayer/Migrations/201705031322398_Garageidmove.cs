namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Garageidmove : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "GarageId", "dbo.Garages");
            DropForeignKey("dbo.ParkingSpaces", "Garage_Id", "dbo.Garages");
            DropIndex("dbo.ParkingSpaces", new[] { "Garage_Id" });
            DropIndex("dbo.Vehicles", new[] { "GarageId" });
            RenameColumn(table: "dbo.ParkingSpaces", name: "Garage_Id", newName: "GarageId");
            AlterColumn("dbo.ParkingSpaces", "GarageId", c => c.Int(nullable: false));
            CreateIndex("dbo.ParkingSpaces", "GarageId");
            AddForeignKey("dbo.ParkingSpaces", "GarageId", "dbo.Garages", "Id", cascadeDelete: true);
            DropColumn("dbo.Vehicles", "GarageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "GarageId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ParkingSpaces", "GarageId", "dbo.Garages");
            DropIndex("dbo.ParkingSpaces", new[] { "GarageId" });
            AlterColumn("dbo.ParkingSpaces", "GarageId", c => c.Int());
            RenameColumn(table: "dbo.ParkingSpaces", name: "GarageId", newName: "Garage_Id");
            CreateIndex("dbo.Vehicles", "GarageId");
            CreateIndex("dbo.ParkingSpaces", "Garage_Id");
            AddForeignKey("dbo.ParkingSpaces", "Garage_Id", "dbo.Garages", "Id");
            AddForeignKey("dbo.Vehicles", "GarageId", "dbo.Garages", "Id", cascadeDelete: true);
        }
    }
}
