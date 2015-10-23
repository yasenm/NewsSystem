namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.Albums;

    public class AlbumController : Controller
    {
        private IAlbumService AlbumService;

        public AlbumController(IAlbumService albumService)
        {
            this.AlbumService = albumService;
        }

        [HttpGet]
        public ActionResult AlbumsGrid()
        {
            var model = new object();
            return this.View("AlbumsGrid", model);
        }

        [HttpGet]
        public ActionResult AlbumsGrid(long albumId)
        {
            var model = new object();
            return this.View("AlbumsGrid", model);
        }

        [HttpGet]
        public ActionResult AlbumsGrid(long albumId, string searchText)
        {
            var model = new object();
            return this.View("AlbumsGrid", model);
        }

        [HttpGet]
        public ActionResult AlbumsGrid(string searchText)
        {
            var model = new object();
            return this.View("AlbumsGrid", model);
        }

        [HttpGet]
        public ActionResult CreateAlbum(string searchText)
        {
            var model = new object();
            return this.View("AlbumsGrid", model);
        }
	}
}