using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;
using System.Web.Mvc;

namespace NewsSystem.Data.ViewModels.Categories
{
    public class CategoryFormViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public long? ParentId { get; set; }
        public SelectList Categories { get; set; }
    }
}
