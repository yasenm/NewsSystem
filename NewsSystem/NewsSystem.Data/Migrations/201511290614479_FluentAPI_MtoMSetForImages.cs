namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluentAPI_MtoMSetForImages : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TokenNSImageNSImages", newName: "NSImageTokenNSImages");
            DropPrimaryKey("dbo.NSImageTokenNSImages");
            AddPrimaryKey("dbo.NSImageTokenNSImages", new[] { "NSImage_Id", "TokenNSImage_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.NSImageTokenNSImages");
            AddPrimaryKey("dbo.NSImageTokenNSImages", new[] { "TokenNSImage_Id", "NSImage_Id" });
            RenameTable(name: "dbo.NSImageTokenNSImages", newName: "TokenNSImageNSImages");
        }
    }
}
