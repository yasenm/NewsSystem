namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyArticleToComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ArticleId", c => c.Long(nullable: false));
            CreateIndex("dbo.Comments", "ArticleId");
            AddForeignKey("dbo.Comments", "ArticleId", "dbo.Articles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropColumn("dbo.Comments", "ArticleId");
        }
    }
}
