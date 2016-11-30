using NewsSystem.Data.Services.Contracts.Comments;
using NewsSystem.Data.ViewModels.Comments;
using NewsSystem.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class CommentsController : BaseController
    {
        private ICommentsClientService _commentsService;

        public CommentsController(ICommentsClientService commentsService)
        {
            _commentsService = commentsService;
        }

        public ActionResult CommentsSection(long newsId)
        {
            return PartialView("CommentsSection", newsId);
        }

        [ChildActionOnly]
        public ActionResult ForNews(long newsId)
        {
            var model = _commentsService.GetByNewsId<CommentBasicViewModel>(newsId);
            return PartialView("ForNews", model);
        }

        [HttpGet]
        public ActionResult Form(long newsId)
        {
            var model = new CommentAddOrUpdateViewModel
            {
                ArticleId = newsId,
            };
            return PartialView("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form(CommentAddOrUpdateViewModel model)
        {
            if (ValidateRecaptcha())
            {
                ModelState.AddModelError("reCaptcha", "reCaptcha didn't go as expected.");
            }

            if (ModelState.IsValid)
            {
                var added = _commentsService.AddOrUpdate(model);
                if (added)
                {
                    return ForNews(model.ArticleId);
                }
            }

            return PartialView(model);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(long commentId, long newsId)
        {
            if (_commentsService.Delete(commentId))
            {
                return ForNews(newsId);
            }

            throw new HttpException(406, "Delete message went wrong");
        }
    }
}