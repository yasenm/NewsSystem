namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_CategoriesClass_Removed_AlbumCategories_CategoriesNowAreLinkedToAlbumsOnlySoonToOthers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AlbumCategories", newName: "Categories");
            RenameTable(name: "dbo.ArticleTags", newName: "TagArticles");
            DropForeignKey("dbo.Albums", "AlbumCategoryId", "dbo.AlbumCategories");
            DropIndex("dbo.Albums", new[] { "AlbumCategoryId" });
            DropPrimaryKey("dbo.TagArticles");
            CreateTable(
                "dbo.CategoryAlbums",
                c => new
                    {
                        Category_Id = c.Long(nullable: false),
                        Album_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Album_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Album_Id);
            
            AddColumn("dbo.Categories", "IsRoot", c => c.Boolean(nullable: false));
            AddColumn("dbo.NSImages", "Category_Id", c => c.Long());
            AddColumn("dbo.Articles", "Category_Id", c => c.Long());
            AddPrimaryKey("dbo.TagArticles", new[] { "Tag_Id", "Article_Id" });
            CreateIndex("dbo.Articles", "Category_Id");
            CreateIndex("dbo.NSImages", "Category_Id");
            AddForeignKey("dbo.Articles", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.NSImages", "Category_Id", "dbo.Categories", "Id");
            DropColumn("dbo.Albums", "AlbumCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "AlbumCategoryId", c => c.Long(nullable: false));
            DropForeignKey("dbo.NSImages", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Articles", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.CategoryAlbums", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CategoryAlbums", new[] { "Album_Id" });
            DropIndex("dbo.CategoryAlbums", new[] { "Category_Id" });
            DropIndex("dbo.NSImages", new[] { "Category_Id" });
            DropIndex("dbo.Articles", new[] { "Category_Id" });
            DropPrimaryKey("dbo.TagArticles");
            DropColumn("dbo.Articles", "Category_Id");
            DropColumn("dbo.NSImages", "Category_Id");
            DropColumn("dbo.Categories", "IsRoot");
            DropTable("dbo.CategoryAlbums");
            AddPrimaryKey("dbo.TagArticles", new[] { "Article_Id", "Tag_Id" });
            CreateIndex("dbo.Albums", "AlbumCategoryId");
            AddForeignKey("dbo.Albums", "AlbumCategoryId", "dbo.AlbumCategories", "Id");
            RenameTable(name: "dbo.TagArticles", newName: "ArticleTags");
            RenameTable(name: "dbo.Categories", newName: "AlbumCategories");
        }
    }
}
