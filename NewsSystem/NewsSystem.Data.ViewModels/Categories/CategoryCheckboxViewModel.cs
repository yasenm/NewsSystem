namespace NewsSystem.Data.ViewModels.Categories
{
    using AutoMapper;

    using Infrastructure.Mapping;
    using Models;

    using System.Collections.Generic;
    using System.Linq;

    public class CategoryCheckboxViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public bool IsChecked { get; set; }

        public List<CategoryCheckboxViewModel> Children { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryCheckboxViewModel>()
               .ForMember(m => m.Children, opt => opt.MapFrom(ac => ac.Children.Select(child => new CategoryCheckboxViewModel
               {
                   Id = child.Id,
                   Title = child.Title,
                   Children = new List<CategoryCheckboxViewModel>()
               })));
        }
    }
}
