namespace NewsSystem.Data.Services.Images
{
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.NSImages;
    using NewsSystem.Data.UnitOfWork;

    public class NSImageService : IDataService, INSImageService
    {
        public INewsSystemData Data { get; set; }

        public NSImageService(INewsSystemData data)
        {
            this.Data = data;
        }
    }
}
