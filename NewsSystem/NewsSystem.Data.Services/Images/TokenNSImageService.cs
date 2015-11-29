namespace NewsSystem.Data.Services.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.TokeNSImages;
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.NSImages;

    public class TokenNSImageService : IDataService, ITokenNSImageService
    {
        public INewsSystemData Data { get; set; }

        public TokenNSImageService(INewsSystemData data)
        {
            this.Data = data;
        }

        public ICollection<TokenNSImageDDLViewModel> GetFullListOfTokens()
        {
            var tokensFullList = this.Data.TokensNSImages.All()
                                        .Project()
                                        .To<TokenNSImageDDLViewModel>()
                                        .ToList();

            return tokensFullList;
        }
    }
}
