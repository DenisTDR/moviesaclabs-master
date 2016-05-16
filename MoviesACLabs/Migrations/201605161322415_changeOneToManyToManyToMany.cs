namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOneToManyToManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Award", "ActorId", "dbo.Actor");
            DropIndex("dbo.Award", new[] { "ActorId" });
            CreateTable(
                "dbo.AwardActor",
                c => new
                    {
                        Award_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Award_Id, t.Actor_Id })
                .ForeignKey("dbo.Award", t => t.Award_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actor", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Award_Id)
                .Index(t => t.Actor_Id);
            
            DropColumn("dbo.Award", "ActorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Award", "ActorId", c => c.Int());
            DropForeignKey("dbo.AwardActor", "Actor_Id", "dbo.Actor");
            DropForeignKey("dbo.AwardActor", "Award_Id", "dbo.Award");
            DropIndex("dbo.AwardActor", new[] { "Actor_Id" });
            DropIndex("dbo.AwardActor", new[] { "Award_Id" });
            DropTable("dbo.AwardActor");
            CreateIndex("dbo.Award", "ActorId");
            AddForeignKey("dbo.Award", "ActorId", "dbo.Actor", "Id");
        }
    }
}
