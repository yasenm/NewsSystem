namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRelationshipBetweenArticlesAndNSImages : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TagNSImages", newName: "NSImageTags");
            DropForeignKey("dbo.NSImageArticles", "NSImage_Id", "dbo.NSImages");
            DropForeignKey("dbo.NSImageArticles", "Article_Id", "dbo.Articles");
            DropIndex("dbo.NSImageArticles", new[] { "NSImage_Id" });
            DropIndex("dbo.NSImageArticles", new[] { "Article_Id" });
            DropPrimaryKey("dbo.NSImageTags");
            AddPrimaryKey("dbo.NSImageTags", new[] { "NSImage_Id", "Tag_Id" });
            DropTable("dbo.NSImageArticles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NSImageArticles",
                c => new
                    {
                        NSImage_Id = c.Long(nullable: false),
                        Article_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.NSImage_Id, t.Article_Id });
            
            DropPrimaryKey("dbo.NSImageTags");
            AddPrimaryKey("dbo.NSImageTags", new[] { "Tag_Id", "NSImage_Id" });
            CreateIndex("dbo.NSImageArticles", "Article_Id");
            CreateIndex("dbo.NSImageArticles", "NSImage_Id");
            AddForeignKey("dbo.NSImageArticles", "Article_Id", "dbo.Articles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NSImageArticles", "NSImage_Id", "dbo.NSImages", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.NSImageTags", newName: "TagNSImages");
        }
    }
}
