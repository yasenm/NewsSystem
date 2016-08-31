namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using Data.Services.Contracts.Surveys;
    using Data.ViewModels.Surveys;
    using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;
    using System.Web.Mvc;

    public class AnswerController : AdminBaseController
    {
        private IQuestionsService QuestionService;
        private IAnswersService AnswersService;

        public AnswerController(IQuestionsService qService, IAnswersService anService)
        {
            this.QuestionService = qService;
            this.AnswersService = anService;
        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult AnswerAddPartial()
        //{
        //    var model = new AnswerAdminViewModel();
        //    return this.PartialView(model);
        //}
        
        //[HttpGet]
        //public ActionResult AddAnswersToQuestion(int questionId)
        //{
        //    var model = this.QuestionService.GetQuestionById(questionId);
        //    return this.View(model);
        //}
        
        //public ActionResult AddAnswersToQuestionListPartial(int questionId)
        //{
        //    var model = this.AnswersService.GetAllAnswersForQuestion(questionId);
        //    return this.PartialView(model);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult Add(int questionId, string aDescription)
        //{
        //    var newAnswer = new AnswerAdminViewModel
        //    {
        //        Description = aDescription,
        //        QuestionId = questionId
        //    };

        //    this.AnswersService.Create(newAnswer);
        //    return this.AddAnswersToQuestionListPartial(questionId);
        //}
    }
}