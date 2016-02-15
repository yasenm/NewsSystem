namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_TitleFieldToNSImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NSImages", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NSImages", "Title");
        }
    }
}
