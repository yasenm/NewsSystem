namespace NewsSystem.Data.ViewModels.NSImages
{
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;
    using System.Web;

    public class NSImageEditViewModel : IMapFrom<NSImage>
    {
        public long Id { get; set; }

        public string ImageTags { get; set; }

        public string Text { get; set; }

        public byte[] ByteContent { get; set; }

        public string Extension { get; set; }

        public HttpPostedFileBase PostedContent { get; set; }
    }
}
