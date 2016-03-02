namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;
    using Data.Services.Contracts;

    public class TagController : AdminBaseController
    {
        private ITagsService TagsService;

        public TagController(ITagsService tagsService)
        {
            this.TagsService = tagsService;
        }

        [HttpGet]
        public ActionResult GetTokens()
        {
            var tags = this.TagsService.GetAllTagsNames();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}