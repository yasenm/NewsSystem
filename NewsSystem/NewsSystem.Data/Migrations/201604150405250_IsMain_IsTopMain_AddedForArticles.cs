namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsMain_IsTopMain_AddedForArticles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "IsMain", c => c.Boolean(nullable: false));
            AddColumn("dbo.Articles", "IsTopMain", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "IsTopMain");
            DropColumn("dbo.Articles", "IsMain");
        }
    }
}
