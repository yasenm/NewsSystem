namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First_MigrationShouldResetAllRealtionships : DbMigration
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
                        CoverImageId = c.Long(),
                        AlbumCategoryId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        NSImage_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumCategories", t => t.AlbumCategoryId)
                .ForeignKey("dbo.NSImages", t => t.NSImage_Id)
                .ForeignKey("dbo.NSImages", t => t.CoverImageId)
                .Index(t => t.CoverImageId)
                .Index(t => t.AlbumCategoryId)
                .Index(t => t.NSImage_Id);
            
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
                "dbo.NSImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        ImageTags = c.String(),
                        Text = c.String(),
                        ByteContent = c.Binary(),
                        Extension = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        Album_Id = c.Long(),
                        Article_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.Album_Id)
                .Index(t => t.Article_Id);
            
            CreateTable(
                "dbo.TokenNSImages",
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
                "dbo.Articles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.NSImageTokenNSImages",
                c => new
                    {
                        NSImage_Id = c.Long(nullable: false),
                        TokenNSImage_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.NSImage_Id, t.TokenNSImage_Id })
                .ForeignKey("dbo.NSImages", t => t.NSImage_Id, cascadeDelete: true)
                .ForeignKey("dbo.TokenNSImages", t => t.TokenNSImage_Id, cascadeDelete: true)
                .Index(t => t.NSImage_Id)
                .Index(t => t.TokenNSImage_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.NSImages", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.AlbumCategories", "ParentId", "dbo.AlbumCategories");
            DropForeignKey("dbo.NSImages", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.Albums", "CoverImageId", "dbo.NSImages");
            DropForeignKey("dbo.NSImageTokenNSImages", "TokenNSImage_Id", "dbo.TokenNSImages");
            DropForeignKey("dbo.NSImageTokenNSImages", "NSImage_Id", "dbo.NSImages");
            DropForeignKey("dbo.Albums", "NSImage_Id", "dbo.NSImages");
            DropForeignKey("dbo.AlbumTokenAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.AlbumTokenAlbums", "AlbumToken_Id", "dbo.AlbumTokens");
            DropForeignKey("dbo.Albums", "AlbumCategoryId", "dbo.AlbumCategories");
            DropIndex("dbo.NSImageTokenNSImages", new[] { "TokenNSImage_Id" });
            DropIndex("dbo.NSImageTokenNSImages", new[] { "NSImage_Id" });
            DropIndex("dbo.AlbumTokenAlbums", new[] { "Album_Id" });
            DropIndex("dbo.AlbumTokenAlbums", new[] { "AlbumToken_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.NSImages", new[] { "Article_Id" });
            DropIndex("dbo.NSImages", new[] { "Album_Id" });
            DropIndex("dbo.Albums", new[] { "NSImage_Id" });
            DropIndex("dbo.Albums", new[] { "AlbumCategoryId" });
            DropIndex("dbo.Albums", new[] { "CoverImageId" });
            DropIndex("dbo.AlbumCategories", new[] { "ParentId" });
            DropTable("dbo.NSImageTokenNSImages");
            DropTable("dbo.AlbumTokenAlbums");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Articles");
            DropTable("dbo.TokenNSImages");
            DropTable("dbo.NSImages");
            DropTable("dbo.AlbumTokens");
            DropTable("dbo.Albums");
            DropTable("dbo.AlbumCategories");
        }
    }
}
