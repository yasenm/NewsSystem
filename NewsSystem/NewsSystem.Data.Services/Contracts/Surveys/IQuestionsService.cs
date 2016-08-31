namespace NewsSystem.Data.Services.Contracts.Surveys
{
    using System.Linq;

    using ViewModels.Surveys;

    public interface IQuestionsService
    {
        IQueryable<QuestionViewModel> GetQuestions();

        QuestionViewModel GetQuestionById(int id);

        QuestionAdminViewModel Create(QuestionAdminViewModel model);

        bool Edit(QuestionAdminViewModel model);

        bool Delete(int id);
    }
}
