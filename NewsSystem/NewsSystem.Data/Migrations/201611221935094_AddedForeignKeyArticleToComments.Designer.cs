// <auto-generated />
namespace NewsSystem.Data.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class AddedForeignKeyArticleToComments : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddedForeignKeyArticleToComments));
        
        string IMigrationMetadata.Id
        {
            get { return "201611221935094_AddedForeignKeyArticleToComments"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
