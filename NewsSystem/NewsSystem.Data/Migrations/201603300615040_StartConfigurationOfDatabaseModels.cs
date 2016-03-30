namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartConfigurationOfDatabaseModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CoverImageId = c.Long(),
                        Author = c.String(),
                        Description = c.String(),
                        Summary = c.String(),
                        Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsRoot = c.Boolean(nullable: false),
                        ParentId = c.Long(),
                        Author = c.String(),
                        Description = c.String(),
                        Summary = c.String(),
                        Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PublicationDate = c.DateTime(),
                        IsPublished = c.Boolean(nullable: false),
                        PublishApprovedBy = c.String(),
                        IsQueuedForPublish = c.Boolean(nullable: false),
                        CoverImageId = c.Long(nullable: false),
                        RelatedAlbumId = c.Long(),
                        Author = c.String(),
                        Description = c.String(),
                        Summary = c.String(),
                        Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
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
                "dbo.NSImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageTags = c.String(),
                        ByteContent = c.Binary(),
                        Extension = c.String(),
                        Author = c.String(),
                        Description = c.String(),
                        Summary = c.String(),
                        Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        Category_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Article_Id = c.Long(nullable: false),
                        Category_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_Id, t.Category_Id })
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Article_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.TagAlbums",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        Album_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Album_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Album_Id);
            
            CreateTable(
                "dbo.TagArticles",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        Article_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Article_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Article_Id);
            
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
            
            CreateTable(
                "dbo.NSImageTags",
                c => new
                    {
                        NSImage_Id = c.Long(nullable: false),
                        Tag_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.NSImage_Id, t.Tag_Id })
                .ForeignKey("dbo.NSImages", t => t.NSImage_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.NSImage_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.NSImages", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.NSImageTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.NSImageTags", "NSImage_Id", "dbo.NSImages");
            DropForeignKey("dbo.NSImageAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.NSImageAlbums", "NSImage_Id", "dbo.NSImages");
            DropForeignKey("dbo.TagArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.TagArticles", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.TagAlbums", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.ArticleCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ArticleCategories", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.CategoryAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.CategoryAlbums", "Category_Id", "dbo.Categories");
            DropIndex("dbo.NSImageTags", new[] { "Tag_Id" });
            DropIndex("dbo.NSImageTags", new[] { "NSImage_Id" });
            DropIndex("dbo.NSImageAlbums", new[] { "Album_Id" });
            DropIndex("dbo.NSImageAlbums", new[] { "NSImage_Id" });
            DropIndex("dbo.TagArticles", new[] { "Article_Id" });
            DropIndex("dbo.TagArticles", new[] { "Tag_Id" });
            DropIndex("dbo.TagAlbums", new[] { "Album_Id" });
            DropIndex("dbo.TagAlbums", new[] { "Tag_Id" });
            DropIndex("dbo.ArticleCategories", new[] { "Category_Id" });
            DropIndex("dbo.ArticleCategories", new[] { "Article_Id" });
            DropIndex("dbo.CategoryAlbums", new[] { "Album_Id" });
            DropIndex("dbo.CategoryAlbums", new[] { "Category_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.NSImages", new[] { "Category_Id" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropTable("dbo.NSImageTags");
            DropTable("dbo.NSImageAlbums");
            DropTable("dbo.TagArticles");
            DropTable("dbo.TagAlbums");
            DropTable("dbo.ArticleCategories");
            DropTable("dbo.CategoryAlbums");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NSImages");
            DropTable("dbo.Tags");
            DropTable("dbo.Articles");
            DropTable("dbo.Categories");
            DropTable("dbo.Albums");
        }
    }
}
