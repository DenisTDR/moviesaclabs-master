namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAward : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Award",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        RandomNumber = c.Int(nullable: false),
                        Actor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actor", t => t.Actor_Id)
                .Index(t => t.Actor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Award", "Actor_Id", "dbo.Actor");
            DropIndex("dbo.Award", new[] { "Actor_Id" });
            DropTable("dbo.Award");
        }
    }
}
