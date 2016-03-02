namespace NewsSystem.Data.ViewModels.NSImages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class NSImageEditViewModel : IMapFrom<NSImage>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string ImageTags { get; set; }

        public ICollection<string> Tokens { get; set; }

        [AllowHtml]
        public string Text { get; set; }

        public byte[] ByteContent { get; set; }

        public string Extension { get; set; }

        public HttpPostedFileBase PostedContent { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<NSImage, NSImageEditViewModel>()
                .ForMember(m => m.Tokens, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));
        }
    }
}
