namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_AlbumTokens : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlbumTokenAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.AlbumTokenAlbums", "AlbumToken_Id", "dbo.AlbumTokens");
            DropIndex("dbo.AlbumTokenAlbums", new[] { "Album_Id" });
            DropIndex("dbo.AlbumTokenAlbums", new[] { "AlbumToken_Id" });
            DropTable("dbo.AlbumTokenAlbums");
            DropTable("dbo.AlbumTokens");
        }
    }
}
