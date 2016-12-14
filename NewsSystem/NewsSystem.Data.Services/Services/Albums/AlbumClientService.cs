using AutoMapper;
using AutoMapper.QueryableExtensions;
using NewsSystem.Data.Services.Contracts.Albums;
using NewsSystem.Data.UnitOfWork;
using System.Linq;

namespace NewsSystem.Data.Services.Albums
{
    public class AlbumClientService : IAlbumClientService
    {
        private INewsSystemData _data;

        public AlbumClientService(INewsSystemData data)
        {
            _data = data;
        }

        public IQueryable<T> GetAlbums<T>()
        {
            var result = _data.Albums.All()
                .ProjectTo<T>();
            return result;
        }

        public T GetAlbum<T>(long albumId)
        {
            var album = _data.Albums.GetById(albumId);
            var model = Mapper.Map<T>(album);
            return model;
        }
    }
}