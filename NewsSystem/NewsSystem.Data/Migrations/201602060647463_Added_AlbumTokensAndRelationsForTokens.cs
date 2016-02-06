namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_AlbumTokensAndRelationsForTokens : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumTokens",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Albums", "AlbumToken_Id", c => c.Long());
            CreateIndex("dbo.Albums", "AlbumToken_Id");
            AddForeignKey("dbo.Albums", "AlbumToken_Id", "dbo.AlbumTokens", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "AlbumToken_Id", "dbo.AlbumTokens");
            DropIndex("dbo.Albums", new[] { "AlbumToken_Id" });
            DropColumn("dbo.Albums", "AlbumToken_Id");
            DropTable("dbo.AlbumTokens");
        }
    }
}
