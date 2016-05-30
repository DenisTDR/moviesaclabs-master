namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class planesAndAirlines_bugfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Plane", "AirlineId", "dbo.Airline");
            DropIndex("dbo.Plane", new[] { "AirlineId" });
            RenameColumn(table: "dbo.Plane", name: "AirlineId", newName: "Airline_Id");
            AlterColumn("dbo.Plane", "Airline_Id", c => c.Int());
            CreateIndex("dbo.Plane", "Airline_Id");
            AddForeignKey("dbo.Plane", "Airline_Id", "dbo.Airline", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plane", "Airline_Id", "dbo.Airline");
            DropIndex("dbo.Plane", new[] { "Airline_Id" });
            AlterColumn("dbo.Plane", "Airline_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Plane", name: "Airline_Id", newName: "AirlineId");
            CreateIndex("dbo.Plane", "AirlineId");
            AddForeignKey("dbo.Plane", "AirlineId", "dbo.Airline", "Id", cascadeDelete: true);
        }
    }
}
