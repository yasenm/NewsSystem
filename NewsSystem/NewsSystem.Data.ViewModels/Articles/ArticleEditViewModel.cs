namespace NewsSystem.Data.ViewModels.Articles
{
    using Common;
    using Infrastructure.Mapping;
    using Models;

    using AutoMapper;

    using System.Collections.Generic;
    using System.Linq;

    public class ArticleEditViewModel : DescribableEntityViewModel, IMapFrom<Article>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public ICollection<string> Tags { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleEditViewModel>()
                .ForMember(m => m.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));
        }
    }
}
