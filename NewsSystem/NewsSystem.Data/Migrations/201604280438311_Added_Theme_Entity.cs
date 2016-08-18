namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Theme_Entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
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
            
            AddColumn("dbo.Categories", "Theme_Id", c => c.Long());
            AddColumn("dbo.Articles", "ThemeId", c => c.Long());
            AddColumn("dbo.Tags", "Theme_Id", c => c.Long());
            CreateIndex("dbo.Categories", "Theme_Id");
            CreateIndex("dbo.Articles", "ThemeId");
            CreateIndex("dbo.Tags", "Theme_Id");
            AddForeignKey("dbo.Articles", "ThemeId", "dbo.Themes", "Id");
            AddForeignKey("dbo.Categories", "Theme_Id", "dbo.Themes", "Id");
            AddForeignKey("dbo.Tags", "Theme_Id", "dbo.Themes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.Categories", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.Articles", "ThemeId", "dbo.Themes");
            DropIndex("dbo.Tags", new[] { "Theme_Id" });
            DropIndex("dbo.Articles", new[] { "ThemeId" });
            DropIndex("dbo.Categories", new[] { "Theme_Id" });
            DropColumn("dbo.Tags", "Theme_Id");
            DropColumn("dbo.Articles", "ThemeId");
            DropColumn("dbo.Categories", "Theme_Id");
            DropTable("dbo.Themes");
        }
    }
}
