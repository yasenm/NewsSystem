namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoverImageIdNullableAddedToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "CoverImageId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "CoverImageId");
        }
    }
}
