namespace NewsSystem.Data.ViewModels.Albums
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using Infrastructure.Mapping;
    using Models;
    using NSImages;
    using Common;
    using Categories;

    public class AlbumEditViewModel : DescribableEntityViewModel, IMapFrom<Album>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public long AlbumCategoryId { get; set; }

        public long? CoverImageId { get; set; }

        public ICollection<string> Tags { get; set; }

        public ICollection<HttpPostedFileBase> AlbumPostedImages { get; set; }

        public ICollection<NSImageGridViewModel> AlbumImages { get; set; }

        public ICollection<long> CategoriesIds { get; set; }

        public List<CategoryCheckboxViewModel> ChosenCategories { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Album, AlbumEditViewModel>()
                .ForMember(m => m.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(m => m.CategoriesIds, opt => opt.MapFrom(src => src.Categories.Select(c => c.Id)));
        }
    }
}
