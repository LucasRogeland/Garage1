namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class garageName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Garages", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Garages", "Name");
        }
    }
}
