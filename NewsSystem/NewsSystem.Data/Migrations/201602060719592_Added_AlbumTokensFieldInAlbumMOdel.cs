namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_AlbumTokensFieldInAlbumMOdel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "AlbumToken_Id", "dbo.AlbumTokens");
            DropIndex("dbo.Albums", new[] { "AlbumToken_Id" });
            CreateTable(
                "dbo.AlbumTokenAlbums",
                c => new
                    {
                        AlbumToken_Id = c.Long(nullable: false),
                        Album_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlbumToken_Id, t.Album_Id })
                .ForeignKey("dbo.AlbumTokens", t => t.AlbumToken_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.AlbumToken_Id)
                .Index(t => t.Album_Id);
            
            DropColumn("dbo.Albums", "AlbumToken_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "AlbumToken_Id", c => c.Long());
            DropForeignKey("dbo.AlbumTokenAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.AlbumTokenAlbums", "AlbumToken_Id", "dbo.AlbumTokens");
            DropIndex("dbo.AlbumTokenAlbums", new[] { "Album_Id" });
            DropIndex("dbo.AlbumTokenAlbums", new[] { "AlbumToken_Id" });
            DropTable("dbo.AlbumTokenAlbums");
            CreateIndex("dbo.Albums", "AlbumToken_Id");
            AddForeignKey("dbo.Albums", "AlbumToken_Id", "dbo.AlbumTokens", "Id");
        }
    }
}
