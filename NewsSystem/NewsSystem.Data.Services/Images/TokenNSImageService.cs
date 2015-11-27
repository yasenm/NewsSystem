namespace NewsSystem.Data.Services.Images
{
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.NSImages;
    using NewsSystem.Data.UnitOfWork;

    public class TokenNSImageService : IDataService, ITokenNSImageService
    {
        public INewsSystemData Data { get; set; }

        public TokenNSImageService(INewsSystemData data)
        {
            this.Data = data;
        }
    }
}
