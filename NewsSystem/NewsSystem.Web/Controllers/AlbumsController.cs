using NewsSystem.Data.Services.Contracts.Albums;
using NewsSystem.Data.ViewModels.Albums;
using NewsSystem.Web.Controllers.Base;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class AlbumsController : BaseController
    {
        private IAlbumClientService _albService;

        public AlbumsController(IAlbumClientService albService)
        {
            _albService = albService;
        }

        public ActionResult Carousel(long id)
        {
            var model = _albService.GetAlbum<AlbumClientMinViewModel>(id);
            return PartialView(model);
        }
    }
}
