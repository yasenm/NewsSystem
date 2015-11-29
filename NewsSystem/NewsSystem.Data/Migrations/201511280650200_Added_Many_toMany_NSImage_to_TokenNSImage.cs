namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Many_toMany_NSImage_to_TokenNSImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TokenNSImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TokenNSImageNSImages",
                c => new
                    {
                        TokenNSImage_Id = c.Long(nullable: false),
                        NSImage_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TokenNSImage_Id, t.NSImage_Id })
                .ForeignKey("dbo.TokenNSImages", t => t.TokenNSImage_Id, cascadeDelete: true)
                .ForeignKey("dbo.NSImages", t => t.NSImage_Id, cascadeDelete: true)
                .Index(t => t.TokenNSImage_Id)
                .Index(t => t.NSImage_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TokenNSImageNSImages", "NSImage_Id", "dbo.NSImages");
            DropForeignKey("dbo.TokenNSImageNSImages", "TokenNSImage_Id", "dbo.TokenNSImages");
            DropIndex("dbo.TokenNSImageNSImages", new[] { "NSImage_Id" });
            DropIndex("dbo.TokenNSImageNSImages", new[] { "TokenNSImage_Id" });
            DropTable("dbo.TokenNSImageNSImages");
            DropTable("dbo.TokenNSImages");
        }
    }
}
