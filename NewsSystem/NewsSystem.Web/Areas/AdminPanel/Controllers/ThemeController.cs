namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using Base;
    using Data.Services.Contracts;
    using Data.ViewModels.Themes;
    using System.Web.Mvc;

    public class ThemeController : AdminBaseController
    {
        private IThemeService ThemeService { get; set; }

        public ThemeController(IThemeService themeService)
        {
            this.ThemeService = themeService;
        }

        // GET: AdminPanel/Theme
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ThemeCreateViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThemeCreateViewModel themeModel)
        {
            return this.View();
        }

        public ActionResult ThemesTable()
        {
            var collection = this.ThemeService.GetAll();
            return this.View(collection);
        }
    }
}