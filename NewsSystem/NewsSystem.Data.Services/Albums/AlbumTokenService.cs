namespace NewsSystem.Data.Services.Albums
{
    using System.Collections.Generic;
    using System.Linq;

    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Albums;
    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.AlbumTokens;

    using AutoMapper.QueryableExtensions;

    public class AlbumTokenService : IDataService, IAlbumTokenService
    {
        public INewsSystemData Data { get; set; }

        public AlbumTokenService(INewsSystemData data)
        {
            this.Data = data;
        }

        public ICollection<AlbumTokenDDLViewModel> GetFullListOfTokens()
        {
            return this.Data.AlbumTokens.All()
                                        .Project()
                                        .To<AlbumTokenDDLViewModel>()
                                        .ToList();
        }
    }
}
