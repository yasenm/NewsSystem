namespace NewsSystem.Data.ViewModels.Categories
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<CategoryViewModel> Children { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
               .ForMember(m => m.Children, opt => opt.MapFrom(ac => ac.Children.Select(child => new CategoryViewModel
                                                                                                   {
                                                                                                       Id = child.Id,
                                                                                                       Title = child.Title,
                                                                                                       Description = child.Description,
                                                                                                       Children = new List<CategoryViewModel>()
                                                                                                   })));
        }
    }
}
