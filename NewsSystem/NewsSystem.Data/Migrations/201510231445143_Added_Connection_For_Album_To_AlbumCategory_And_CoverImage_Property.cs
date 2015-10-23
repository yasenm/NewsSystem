namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Connection_For_Album_To_AlbumCategory_And_CoverImage_Property : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NSImages", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Albums", new[] { "AlbumCategory_Id" });
            RenameColumn(table: "dbo.Albums", name: "AlbumCategory_Id", newName: "AlbumCategoryId");
            AddColumn("dbo.Albums", "CoverImageId", c => c.Long());
            AddColumn("dbo.NSImages", "Album_Id", c => c.Long());
            AlterColumn("dbo.Albums", "AlbumCategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.Albums", "CoverImageId");
            CreateIndex("dbo.Albums", "AlbumCategoryId");
            CreateIndex("dbo.NSImages", "Album_Id");
            AddForeignKey("dbo.Albums", "CoverImageId", "dbo.NSImages", "Id");
            AddForeignKey("dbo.NSImages", "Album_Id", "dbo.Albums", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NSImages", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.Albums", "CoverImageId", "dbo.NSImages");
            DropIndex("dbo.NSImages", new[] { "Album_Id" });
            DropIndex("dbo.Albums", new[] { "AlbumCategoryId" });
            DropIndex("dbo.Albums", new[] { "CoverImageId" });
            AlterColumn("dbo.Albums", "AlbumCategoryId", c => c.Long());
            DropColumn("dbo.NSImages", "Album_Id");
            DropColumn("dbo.Albums", "CoverImageId");
            RenameColumn(table: "dbo.Albums", name: "AlbumCategoryId", newName: "AlbumCategory_Id");
            CreateIndex("dbo.Albums", "AlbumCategory_Id");
            AddForeignKey("dbo.NSImages", "AlbumId", "dbo.Albums", "Id");
        }
    }
}
