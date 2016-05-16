namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAwardsToActors : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Award", name: "Actor_Id", newName: "ActorId");
            RenameIndex(table: "dbo.Award", name: "IX_Actor_Id", newName: "IX_ActorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Award", name: "IX_ActorId", newName: "IX_Actor_Id");
            RenameColumn(table: "dbo.Award", name: "ActorId", newName: "Actor_Id");
        }
    }
}
