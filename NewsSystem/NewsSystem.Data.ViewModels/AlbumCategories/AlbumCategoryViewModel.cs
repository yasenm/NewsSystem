namespace NewsSystem.Data.ViewModels.AlbumCategories
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class AlbumCategoryViewModel : IMapFrom<AlbumCategory>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public ICollection<AlbumCategoryViewModel> Children { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AlbumCategory, AlbumCategoryViewModel>()
               .ForMember(m => m.Children, opt => opt.MapFrom(ac => ac.Children.Select(child => new AlbumCategoryViewModel
                                                                                                   {
                                                                                                       Id = child.Id,
                                                                                                       Name = child.Name,
                                                                                                       Text = child.Text,
                                                                                                       Children = new List<AlbumCategoryViewModel>()
                                                                                                   })));
        }
    }
}
