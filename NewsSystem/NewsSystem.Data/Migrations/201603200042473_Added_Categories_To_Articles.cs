namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Categories_To_Articles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Articles", new[] { "Category_Id" });
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
            
            DropColumn("dbo.Articles", "Category_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Category_Id", c => c.Long());
            DropForeignKey("dbo.ArticleCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ArticleCategories", "Article_Id", "dbo.Articles");
            DropIndex("dbo.ArticleCategories", new[] { "Category_Id" });
            DropIndex("dbo.ArticleCategories", new[] { "Article_Id" });
            DropTable("dbo.ArticleCategories");
            CreateIndex("dbo.Articles", "Category_Id");
            AddForeignKey("dbo.Articles", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
