namespace NewsSystem.Data.ViewModels.NSImages
{
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class NSImageOnlyIdViewModel : IMapFrom<NSImage>
    {
        public long Id { get; set; }
    }
}