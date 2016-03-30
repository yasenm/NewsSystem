namespace NewsSystem.Data.ViewModels.Articles
{
    using AutoMapper;

    using Categories;
    using Common;
    using Infrastructure.Mapping;
    using Models;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ArticleEditViewModel : DescribableEntityViewModel, IMapFrom<Article>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public ICollection<string> Tags { get; set; }

        public List<long> CategoriesIds { get; set; }

        public List<CategoryCheckboxViewModel> ChosenCategories { get; set; }

        public long? RelatedAlbumId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "A choice of image is required!")]
        [Required(ErrorMessage = "A choice of image is required!")]
        public long CoverImageId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleEditViewModel>()
                .ForMember(m => m.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(m => m.CategoriesIds, opt => opt.MapFrom(src => src.Categories.Select(c => c.Id)));
        }
    }
}
