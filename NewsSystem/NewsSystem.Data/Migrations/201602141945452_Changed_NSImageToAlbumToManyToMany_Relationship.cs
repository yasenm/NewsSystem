namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_NSImageToAlbumToManyToMany_Relationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NSImages", "AlbumId", "dbo.Albums");
            DropIndex("dbo.NSImages", new[] { "AlbumId" });
            AddColumn("dbo.Albums", "NSImage_Id", c => c.Long());
            CreateIndex("dbo.Albums", "NSImage_Id");
            AddForeignKey("dbo.Albums", "NSImage_Id", "dbo.NSImages", "Id");
            DropColumn("dbo.NSImages", "AlbumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NSImages", "AlbumId", c => c.Long(nullable: false));
            DropForeignKey("dbo.Albums", "NSImage_Id", "dbo.NSImages");
            DropIndex("dbo.Albums", new[] { "NSImage_Id" });
            DropColumn("dbo.Albums", "NSImage_Id");
            CreateIndex("dbo.NSImages", "AlbumId");
            AddForeignKey("dbo.NSImages", "AlbumId", "dbo.Albums", "Id");
        }
    }
}
