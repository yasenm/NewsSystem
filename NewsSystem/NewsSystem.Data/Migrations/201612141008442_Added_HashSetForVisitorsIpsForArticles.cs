namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_HashSetForVisitorsIpsForArticles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisitorIps",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IpAddress = c.String(maxLength: 39),
                        LastVisit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IpAddress, unique: true);
            
            CreateTable(
                "dbo.VisitorIpArticles",
                c => new
                    {
                        VisitorIp_Id = c.Long(nullable: false),
                        Article_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.VisitorIp_Id, t.Article_Id })
                .ForeignKey("dbo.VisitorIps", t => t.VisitorIp_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.VisitorIp_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitorIpArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.VisitorIpArticles", "VisitorIp_Id", "dbo.VisitorIps");
            DropIndex("dbo.VisitorIpArticles", new[] { "Article_Id" });
            DropIndex("dbo.VisitorIpArticles", new[] { "VisitorIp_Id" });
            DropIndex("dbo.VisitorIps", new[] { "IpAddress" });
            DropTable("dbo.VisitorIpArticles");
            DropTable("dbo.VisitorIps");
        }
    }
}
