namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class planesAndAirlines : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Award", "ActorId", "dbo.Actor");
            DropIndex("dbo.Award", new[] { "ActorId" });
            CreateTable(
                "dbo.Airline",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plane",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seats = c.Int(nullable: false),
                        Model = c.String(),
                        AirlineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airline", t => t.AirlineId, cascadeDelete: true)
                .Index(t => t.AirlineId);
            
            DropTable("dbo.Award");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Award",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        RandomNumber = c.Int(nullable: false),
                        ActorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Plane", "AirlineId", "dbo.Airline");
            DropIndex("dbo.Plane", new[] { "AirlineId" });
            DropTable("dbo.Plane");
            DropTable("dbo.Airline");
            CreateIndex("dbo.Award", "ActorId");
            AddForeignKey("dbo.Award", "ActorId", "dbo.Actor", "Id");
        }
    }
}
