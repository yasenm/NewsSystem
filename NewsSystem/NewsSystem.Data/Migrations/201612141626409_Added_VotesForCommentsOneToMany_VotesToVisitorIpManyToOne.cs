namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_VotesForCommentsOneToMany_VotesToVisitorIpManyToOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsPositive = c.Boolean(nullable: false),
                        VisitorIpId = c.Long(nullable: false),
                        CommentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId)
                .ForeignKey("dbo.VisitorIps", t => t.VisitorIpId)
                .Index(t => t.VisitorIpId)
                .Index(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "VisitorIpId", "dbo.VisitorIps");
            DropForeignKey("dbo.Votes", "CommentId", "dbo.Comments");
            DropIndex("dbo.Votes", new[] { "CommentId" });
            DropIndex("dbo.Votes", new[] { "VisitorIpId" });
            DropTable("dbo.Votes");
        }
    }
}
