namespace NewsSystem.Data.Services.Albums
{
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Albums;
    using NewsSystem.Data.UnitOfWork;

    public class AlbumService : IDataService, IAlbumService
    {
        public INewsSystemData Data { get; set; }

        public AlbumService(INewsSystemData data)
        {
            this.Data = data;
        }
    }
}
