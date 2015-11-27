namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_TokensForImages_ForFastSearchAndIndexing : DbMigration
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
                        NSImage_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NSImages", t => t.NSImage_Id)
                .Index(t => t.NSImage_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TokenNSImages", "NSImage_Id", "dbo.NSImages");
            DropIndex("dbo.TokenNSImages", new[] { "NSImage_Id" });
            DropTable("dbo.TokenNSImages");
        }
    }
}
