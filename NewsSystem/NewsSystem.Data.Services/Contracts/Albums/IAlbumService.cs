namespace NewsSystem.Data.Services.Contracts.Albums
{
    using ViewModels.Albums;
    using System.Collections.Generic;

    public interface IAlbumService
    {
        T GetAlbumForEdit<T>(long albumId);

        bool EditAlbum(AlbumEditViewModel editModel);

        bool Create(AlbumCreateViewModel albumModel);

        bool Delete(long albumId);

        IEnumerable<T> GetAlbums<T>();

        IEnumerable<AlbumGridViewModel> GetAlbumsBySearchText(string searchText, string tags);

        IEnumerable<AlbumGridViewModel> GetAlbumsByCategoryId(long categoryId);
    }
}
