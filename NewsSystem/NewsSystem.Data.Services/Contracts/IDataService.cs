namespace NewsSystem.Data.Services.Contracts
{
    using NewsSystem.Data.UnitOfWork;

    public interface IDataService
    {
        INewsSystemData Data { get; set; }
    }
}
