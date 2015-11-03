namespace NewsSystem.Data.Services.Contracts.Albums
{
    using ViewModels.Albums;
    using System.Collections.Generic;

    public interface IAlbumService
    {
        AlbumEditViewModel GetAlbumForEdit(long albumId);

        bool EditAlbum(AlbumEditViewModel editModel);

        bool Create(AlbumCreateViewModel albumModel);

        bool Delete(long albumId);

        IEnumerable<AlbumGridViewModel> GetAlbums();

        IEnumerable<AlbumGridViewModel> GetAlbumsBySearchText(string searchText);

        IEnumerable<AlbumGridViewModel> GetAlbumsByCategoryId(long categoryId); 

        //IEnumerable<AlbumGridViewModel> Search();

        //IEnumerable<AlbumGridViewModel> Search(long albumId);

        //IEnumerable<AlbumGridViewModel> Search(long albumId, string searchText);

        //IEnumerable<AlbumGridViewModel> Search(string searchText);


    }
}
