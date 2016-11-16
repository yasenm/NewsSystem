using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;

namespace NewsSystem.Data.ViewModels.Tags
{
    public class TagClientViewModel : IMapFrom<Tag>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
