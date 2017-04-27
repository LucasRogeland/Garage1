namespace Garage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class License : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ParkedVehicles");
            AddColumn("dbo.ParkedVehicles", "License", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ParkedVehicles", "License");
            DropColumn("dbo.ParkedVehicles", "Licens");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkedVehicles", "Licens", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.ParkedVehicles");
            DropColumn("dbo.ParkedVehicles", "License");
            AddPrimaryKey("dbo.ParkedVehicles", "Licens");
        }
    }
}
