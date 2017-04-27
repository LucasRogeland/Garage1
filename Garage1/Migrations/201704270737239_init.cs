namespace Garage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        Licens = c.String(nullable: false, maxLength: 128),
                        VehicleType = c.Int(nullable: false),
                        Manufacturer = c.String(),
                        Model = c.String(),
                        Color = c.String(),
                        CheckInTime = c.DateTime(nullable: false),
                        NumberOfWheels = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Licens);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParkedVehicles");
        }
    }
}
