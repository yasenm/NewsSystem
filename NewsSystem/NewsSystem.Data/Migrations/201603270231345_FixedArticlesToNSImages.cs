namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedArticlesToNSImages : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.NSImageTags", newName: "TagNSImages");
            DropPrimaryKey("dbo.TagNSImages");
            CreateTable(
                "dbo.NSImageArticles",
                c => new
                    {
                        NSImage_Id = c.Long(nullable: false),
                        Article_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.NSImage_Id, t.Article_Id })
                .ForeignKey("dbo.NSImages", t => t.NSImage_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.NSImage_Id)
                .Index(t => t.Article_Id);
            
            AddPrimaryKey("dbo.TagNSImages", new[] { "Tag_Id", "NSImage_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NSImageArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.NSImageArticles", "NSImage_Id", "dbo.NSImages");
            DropIndex("dbo.NSImageArticles", new[] { "Article_Id" });
            DropIndex("dbo.NSImageArticles", new[] { "NSImage_Id" });
            DropPrimaryKey("dbo.TagNSImages");
            DropTable("dbo.NSImageArticles");
            AddPrimaryKey("dbo.TagNSImages", new[] { "NSImage_Id", "Tag_Id" });
            RenameTable(name: "dbo.TagNSImages", newName: "NSImageTags");
        }
    }
}
