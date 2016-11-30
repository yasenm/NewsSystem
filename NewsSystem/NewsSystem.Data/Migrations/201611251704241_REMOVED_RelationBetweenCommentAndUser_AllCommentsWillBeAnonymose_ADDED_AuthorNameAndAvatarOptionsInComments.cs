namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REMOVED_RelationBetweenCommentAndUser_AllCommentsWillBeAnonymose_ADDED_AuthorNameAndAvatarOptionsInComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            AddColumn("dbo.Comments", "AuthorName", c => c.String());
            AddColumn("dbo.Comments", "AvatarId", c => c.Long(nullable: false));
            AddColumn("dbo.Comments", "AvatarUrl", c => c.String());
            DropColumn("dbo.Comments", "AuthorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "AuthorId", c => c.String(maxLength: 128));
            DropColumn("dbo.Comments", "AvatarUrl");
            DropColumn("dbo.Comments", "AvatarId");
            DropColumn("dbo.Comments", "AuthorName");
            CreateIndex("dbo.Comments", "AuthorId");
            AddForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers", "Id");
        }
    }
}
