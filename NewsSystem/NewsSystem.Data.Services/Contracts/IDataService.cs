namespace NewsSystem.Data.Services.Contracts
{
    using NewsSystem.Data.UnitOfWork;

    public interface IDataService
    {
        INewsSystemData _data { get; set; }
    }
}
