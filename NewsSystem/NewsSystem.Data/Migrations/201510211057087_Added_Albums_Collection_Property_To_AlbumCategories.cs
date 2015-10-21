namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Albums_Collection_Property_To_AlbumCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "AlbumCategory_Id", c => c.Long());
            CreateIndex("dbo.Albums", "AlbumCategory_Id");
            AddForeignKey("dbo.Albums", "AlbumCategory_Id", "dbo.AlbumCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "AlbumCategory_Id", "dbo.AlbumCategories");
            DropIndex("dbo.Albums", new[] { "AlbumCategory_Id" });
            DropColumn("dbo.Albums", "AlbumCategory_Id");
        }
    }
}
