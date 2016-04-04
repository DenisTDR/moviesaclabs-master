namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Revenue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.Movie_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.MovieActor",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id })
                .ForeignKey("dbo.Movie", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actor", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Actor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "Movie_Id", "dbo.Movie");
            DropForeignKey("dbo.MovieActor", "Actor_Id", "dbo.Actor");
            DropForeignKey("dbo.MovieActor", "Movie_Id", "dbo.Movie");
            DropIndex("dbo.MovieActor", new[] { "Actor_Id" });
            DropIndex("dbo.MovieActor", new[] { "Movie_Id" });
            DropIndex("dbo.Review", new[] { "Movie_Id" });
            DropTable("dbo.MovieActor");
            DropTable("dbo.Review");
            DropTable("dbo.Movie");
            DropTable("dbo.Actor");
        }
    }
}
