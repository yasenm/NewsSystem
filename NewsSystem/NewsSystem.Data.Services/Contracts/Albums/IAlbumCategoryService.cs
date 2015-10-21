namespace NewsSystem.Data.Services.Contracts.Albums
{
    using System.Collections.Generic;

    using NewsSystem.Data.ViewModels.AlbumCategories;

    public interface IAlbumCategoryService
    {
        IEnumerable<AlbumCategoryViewModel> GetAll();
    }
}
