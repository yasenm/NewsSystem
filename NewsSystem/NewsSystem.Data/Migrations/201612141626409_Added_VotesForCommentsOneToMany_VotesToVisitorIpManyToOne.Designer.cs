// <auto-generated />
namespace NewsSystem.Data.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class Added_VotesForCommentsOneToMany_VotesToVisitorIpManyToOne : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(Added_VotesForCommentsOneToMany_VotesToVisitorIpManyToOne));
        
        string IMigrationMetadata.Id
        {
            get { return "201612141626409_Added_VotesForCommentsOneToMany_VotesToVisitorIpManyToOne"; }
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