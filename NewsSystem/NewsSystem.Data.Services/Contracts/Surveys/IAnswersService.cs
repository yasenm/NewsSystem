namespace NewsSystem.Data.Services.Contracts.Surveys
{
    using NewsSystem.Data.ViewModels.Surveys;
    using System.Collections.Generic;
    using System.Linq;

    public interface IAnswersService
    {
        IQueryable<AnswerViewModel> GetAllAnswers();

        AnswerViewModel GetById(int id);

        bool Create(AnswerAdminViewModel model);

        bool Edit(AnswerAdminViewModel model);

        bool Delete(int id);

        IEnumerable<AnswerViewModel> GetAllAnswersForQuestion(int questionId);
    }
}
