namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_NSImages_Albums_AlbumCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Text = c.String(),
                        ParentId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Text = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NSImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageTags = c.String(),
                        Text = c.String(),
                        ByteContent = c.Binary(),
                        Extension = c.String(),
                        AlbumId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        Article_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.AlbumId)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NSImages", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.NSImages", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.AlbumCategories", "ParentId", "dbo.AlbumCategories");
            DropIndex("dbo.NSImages", new[] { "Article_Id" });
            DropIndex("dbo.NSImages", new[] { "AlbumId" });
            DropIndex("dbo.AlbumCategories", new[] { "ParentId" });
            DropTable("dbo.NSImages");
            DropTable("dbo.Albums");
            DropTable("dbo.AlbumCategories");
        }
    }
}
