using NewsSystem.Data.Services.Contracts.Tags;
using NewsSystem.Data.ViewModels.Tags;
using NewsSystem.Web.Controllers.Base;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class TagController : BaseController
    {
        private ITagsClientService _tagsService;

        public TagController(ITagsClientService tagsService)
        {
            _tagsService = tagsService;
        }

        public ActionResult NewsTags(long newsId)
        {
            var result = _tagsService.GetAllGenericForArticle<TagClientViewModel>(newsId);

            return PartialView(result);
        }
    }
}