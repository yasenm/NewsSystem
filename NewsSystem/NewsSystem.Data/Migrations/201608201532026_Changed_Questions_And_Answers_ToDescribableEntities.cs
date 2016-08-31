namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Questions_And_Answers_ToDescribableEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Author", c => c.String());
            AddColumn("dbo.Answers", "Summary", c => c.String());
            AddColumn("dbo.Answers", "Title", c => c.String());
            AddColumn("dbo.Questions", "Author", c => c.String());
            AddColumn("dbo.Questions", "Summary", c => c.String());
            AddColumn("dbo.Questions", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Title");
            DropColumn("dbo.Questions", "Summary");
            DropColumn("dbo.Questions", "Author");
            DropColumn("dbo.Answers", "Title");
            DropColumn("dbo.Answers", "Summary");
            DropColumn("dbo.Answers", "Author");
        }
    }
}
