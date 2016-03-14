namespace NewsSystem.Data.ViewModels.NSImages
{
    using AutoMapper;

    using Common;
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class NSImageEditViewModel : DescribableEntityViewModel, IMapFrom<NSImage>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string ImageTags { get; set; }

        public ICollection<string> Tags { get; set; }

        public byte[] ByteContent { get; set; }

        public string Extension { get; set; }

        public HttpPostedFileBase PostedContent { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<NSImage, NSImageEditViewModel>()
                .ForMember(m => m.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));
        }
    }
}
