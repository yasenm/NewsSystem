namespace NewsSystem.Data.ViewModels.Albums
{
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class AlbumCreateViewModel : IMapFrom<Album>
    {
        public string Name { get; set; }
    }
}
