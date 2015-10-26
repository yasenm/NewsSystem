namespace NewsSystem.Data.ViewModels.NSImages
{
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class NSImageGridViewModel : IMapFrom<NSImage>
    {
        public long Id { get; set; }

        public string ImageTags { get; set; }

        public byte[] ByteContent { get; set; }

        public string Extension { get; set; }
    }
}
