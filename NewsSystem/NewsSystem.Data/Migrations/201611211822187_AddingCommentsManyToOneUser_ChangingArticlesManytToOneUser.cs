namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCommentsManyToOneUser_ChangingArticlesManytToOneUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Content = c.String(),
                        AuthorId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            AddColumn("dbo.Articles", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Articles", "AuthorId");
            AddForeignKey("dbo.Articles", "AuthorId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Albums", "Author");
            DropColumn("dbo.Categories", "Author");
            DropColumn("dbo.Articles", "Author");
            DropColumn("dbo.NSImages", "Author");
            DropColumn("dbo.Themes", "Author");
            DropColumn("dbo.Answers", "Author");
            DropColumn("dbo.Questions", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Author", c => c.String());
            AddColumn("dbo.Answers", "Author", c => c.String());
            AddColumn("dbo.Themes", "Author", c => c.String());
            AddColumn("dbo.NSImages", "Author", c => c.String());
            AddColumn("dbo.Articles", "Author", c => c.String());
            AddColumn("dbo.Categories", "Author", c => c.String());
            AddColumn("dbo.Albums", "Author", c => c.String());
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropIndex("dbo.Articles", new[] { "AuthorId" });
            DropColumn("dbo.Articles", "AuthorId");
            DropTable("dbo.Comments");
        }
    }
}
