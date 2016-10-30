using AutoMapper;
using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NewsSystem.Data.ViewModels.Categories
{
    public class CategoryMenuClientViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public List<CategoryMenuClientViewModel> Children { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryMenuClientViewModel>()
                .ForMember(m => m.Children, opt => opt.MapFrom(
                    ac => ac.Children.Select(child => new CategoryMenuClientViewModel
                    {
                        Id = child.Id,
                        Title = child.Title,
                        Children = new List<CategoryMenuClientViewModel>()
                    })));
        }
    }
}
