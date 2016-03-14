namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_AbstractClassDescribableEntityForArticlesImagesAlbumsAlbumCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlbumCategories", "Author", c => c.String());
            AddColumn("dbo.AlbumCategories", "Description", c => c.String());
            AddColumn("dbo.AlbumCategories", "Summary", c => c.String());
            AddColumn("dbo.AlbumCategories", "Title", c => c.String());
            AddColumn("dbo.Albums", "Author", c => c.String());
            AddColumn("dbo.Albums", "Description", c => c.String());
            AddColumn("dbo.Albums", "Summary", c => c.String());
            AddColumn("dbo.Albums", "Title", c => c.String());
            AddColumn("dbo.NSImages", "Author", c => c.String());
            AddColumn("dbo.NSImages", "Description", c => c.String());
            AddColumn("dbo.NSImages", "Summary", c => c.String());
            AddColumn("dbo.Articles", "Author", c => c.String());
            AddColumn("dbo.Articles", "Description", c => c.String());
            AddColumn("dbo.Articles", "Summary", c => c.String());
            DropColumn("dbo.AlbumCategories", "Name");
            DropColumn("dbo.AlbumCategories", "Text");
            DropColumn("dbo.Albums", "Name");
            DropColumn("dbo.Albums", "Text");
            DropColumn("dbo.NSImages", "Text");
            DropColumn("dbo.Articles", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Content", c => c.String());
            AddColumn("dbo.NSImages", "Text", c => c.String());
            AddColumn("dbo.Albums", "Text", c => c.String());
            AddColumn("dbo.Albums", "Name", c => c.String(maxLength: 200));
            AddColumn("dbo.AlbumCategories", "Text", c => c.String());
            AddColumn("dbo.AlbumCategories", "Name", c => c.String());
            DropColumn("dbo.Articles", "Summary");
            DropColumn("dbo.Articles", "Description");
            DropColumn("dbo.Articles", "Author");
            DropColumn("dbo.NSImages", "Summary");
            DropColumn("dbo.NSImages", "Description");
            DropColumn("dbo.NSImages", "Author");
            DropColumn("dbo.Albums", "Title");
            DropColumn("dbo.Albums", "Summary");
            DropColumn("dbo.Albums", "Description");
            DropColumn("dbo.Albums", "Author");
            DropColumn("dbo.AlbumCategories", "Title");
            DropColumn("dbo.AlbumCategories", "Summary");
            DropColumn("dbo.AlbumCategories", "Description");
            DropColumn("dbo.AlbumCategories", "Author");
        }
    }
}
