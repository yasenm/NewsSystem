namespace NewsSystem.Data.ViewModels.NSImages
{
    using Infrastructure.Mapping;
    using Models;

    public class NSImageOnlyIdViewModel : IMapFrom<NSImage>
    {
        public long Id { get; set; }
        public string ImgTagClasses { get; set; }
    }
}