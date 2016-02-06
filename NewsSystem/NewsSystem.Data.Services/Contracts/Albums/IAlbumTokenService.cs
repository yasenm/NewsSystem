namespace NewsSystem.Data.Services.Contracts.Albums
{
    using System.Collections.Generic;

    using NewsSystem.Data.ViewModels.AlbumTokens;

    public interface IAlbumTokenService
    {
        ICollection<AlbumTokenDDLViewModel> GetFullListOfTokens();
    }
}
