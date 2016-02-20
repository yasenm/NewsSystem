namespace NewsSystem.Data.ViewModels.Albums
{
    using AutoMapper;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;
    using NewsSystem.Data.ViewModels.NSImages;

    public class AlbumGridViewModel : IMapFrom<Album>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? CoverImageId { get; set; }
    }
}
