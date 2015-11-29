namespace NewsSystem.Data.Services.Contracts.NSImages
{
    using System.Collections.Generic;

    using NewsSystem.Data.ViewModels.TokeNSImages;

    public interface ITokenNSImageService
    {
        ICollection<TokenNSImageDDLViewModel> GetFullListOfTokens();
    }
}
