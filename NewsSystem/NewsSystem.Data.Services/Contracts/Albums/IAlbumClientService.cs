using System.Collections.Generic;
using System.Linq;

namespace NewsSystem.Data.Services.Contracts.Albums
{
    public interface IAlbumClientService
    {
        T GetAlbum<T>(long albumId);

        IQueryable<T> GetAlbums<T>();
    }
}
