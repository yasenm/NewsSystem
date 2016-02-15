namespace NewsSystem.Data.ViewModels.NSImages
{
    using System.Web;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class NSImageCreateViewModel : IMapFrom<NSImage>
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public byte[] ByteContent { get; set; }

        public HttpPostedFileBase ImageBase { get; set; }
    }
}
