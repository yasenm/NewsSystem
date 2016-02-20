namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_AlbumToNSImagesBugRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "NSImage_Id", "dbo.NSImages");
            DropForeignKey("dbo.Albums", "CoverImageId", "dbo.NSImages");
            DropForeignKey("dbo.NSImages", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Albums", new[] { "CoverImageId" });
            DropIndex("dbo.Albums", new[] { "NSImage_Id" });
            DropIndex("dbo.NSImages", new[] { "Album_Id" });
            CreateTable(
                "dbo.NSImageAlbums",
                c => new
                    {
                        NSImage_Id = c.Long(nullable: false),
                        Album_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.NSImage_Id, t.Album_Id })
                .ForeignKey("dbo.NSImages", t => t.NSImage_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.NSImage_Id)
                .Index(t => t.Album_Id);
            
            DropColumn("dbo.Albums", "NSImage_Id");
            DropColumn("dbo.NSImages", "Album_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NSImages", "Album_Id", c => c.Long());
            AddColumn("dbo.Albums", "NSImage_Id", c => c.Long());
            DropForeignKey("dbo.NSImageAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.NSImageAlbums", "NSImage_Id", "dbo.NSImages");
            DropIndex("dbo.NSImageAlbums", new[] { "Album_Id" });
            DropIndex("dbo.NSImageAlbums", new[] { "NSImage_Id" });
            DropTable("dbo.NSImageAlbums");
            CreateIndex("dbo.NSImages", "Album_Id");
            CreateIndex("dbo.Albums", "NSImage_Id");
            CreateIndex("dbo.Albums", "CoverImageId");
            AddForeignKey("dbo.NSImages", "Album_Id", "dbo.Albums", "Id");
            AddForeignKey("dbo.Albums", "CoverImageId", "dbo.NSImages", "Id");
            AddForeignKey("dbo.Albums", "NSImage_Id", "dbo.NSImages", "Id");
        }
    }
}
