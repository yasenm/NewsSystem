namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_SetOfArticlesForNSImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NSImages", "Article_Id", "dbo.Articles");
            DropIndex("dbo.NSImages", new[] { "Article_Id" });
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
            
            DropColumn("dbo.NSImages", "Article_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NSImages", "Article_Id", c => c.Long());
            DropForeignKey("dbo.NSImageArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.NSImageArticles", "NSImage_Id", "dbo.NSImages");
            DropIndex("dbo.NSImageArticles", new[] { "Article_Id" });
            DropIndex("dbo.NSImageArticles", new[] { "NSImage_Id" });
            DropTable("dbo.NSImageArticles");
            CreateIndex("dbo.NSImages", "Article_Id");
            AddForeignKey("dbo.NSImages", "Article_Id", "dbo.Articles", "Id");
        }
    }
}
