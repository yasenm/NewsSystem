namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDED_ViewsCountPropertyToArticles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ViewsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "ViewsCount");
        }
    }
}
