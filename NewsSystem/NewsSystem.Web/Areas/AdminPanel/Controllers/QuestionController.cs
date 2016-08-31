namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using Data.Services.Contracts.Surveys;
    using Data.ViewModels.Surveys;
    using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;
    using System.Web.Mvc;

    public class QuestionController : AdminBaseController
    {
        private IQuestionsService QuestionService;

        public QuestionController(IQuestionsService qService)
        {
            this.QuestionService = qService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuestionsTable()
        {
            var model = this.QuestionService.GetQuestions();
            return this.PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new QuestionAdminViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionAdminViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var updatedModel = this.QuestionService.Create(model);
                if (updatedModel != null)
                {
                    return this.RedirectToAction("AddAnswers", "Answer", new { questionId = updatedModel.Id });
                }
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult QuestionDetails(int questionId)
        {
            var model = this.QuestionService.GetQuestionById(questionId);
            return this.PartialView(model);
        }
    }
}