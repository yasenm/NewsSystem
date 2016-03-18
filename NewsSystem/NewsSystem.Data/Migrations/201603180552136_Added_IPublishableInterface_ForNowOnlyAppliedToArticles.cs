namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_IPublishableInterface_ForNowOnlyAppliedToArticles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "PublicationDate", c => c.DateTime());
            AddColumn("dbo.Articles", "IsPublished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Articles", "PublishApprovedBy", c => c.String());
            AddColumn("dbo.Articles", "IsQueuedForPublish", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "IsQueuedForPublish");
            DropColumn("dbo.Articles", "PublishApprovedBy");
            DropColumn("dbo.Articles", "IsPublished");
            DropColumn("dbo.Articles", "PublicationDate");
        }
    }
}
