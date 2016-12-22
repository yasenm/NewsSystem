namespace NewsSystem.Data.Services.Surveys
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Contracts.Surveys;
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.UnitOfWork;
    using ViewModels.Surveys;

    using System;
    using System.Linq;
    using Models;

    public class QuestionsService : IDataService, IQuestionsService
    {
        public INewsSystemData _data { get; set; }
        private IAnswersService AnswerService { get; set; }

        public QuestionsService(INewsSystemData data, IAnswersService answerService)
        {
            this._data = data;
            this.AnswerService = answerService;
        }

        public IQueryable<QuestionViewModel> GetQuestions()
        {
            return this._data.Questions.All()
                .OrderBy(q => q.CreatedOn)
                .Project()
                .To<QuestionViewModel>();
        }

        public QuestionViewModel GetQuestionById(int id)
        {
            var question = this._data.Questions.GetById(id);
            var model = Mapper.Map<QuestionViewModel>(question);

            return model;
        }

        public QuestionAdminViewModel Create(QuestionAdminViewModel model)
        {
            try
            {
                var actual = Mapper.Map<Question>(model);
                this._data.Questions.Add(actual);
                this._data.SaveChanges();
                model.Id = actual.Id;
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Edit(QuestionAdminViewModel model)
        {
            try
            {
                var actual = Mapper.Map<Question>(model);
                foreach (var answer in model.Answers)
                {
                    if (answer.Id == 0)
                    {
                        answer.QuestionId = model.Id;
                        this.AnswerService.Create(answer);
                    }
                    else
                    {
                        this.AnswerService.Edit(answer);
                    }
                }
                this._data.Questions.Update(actual);
                this._data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                this._data.Questions.Delete(id);
                this._data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public QuestionAdminViewModel GetQuestionForEditId(int id)
        {
            var question = this._data.Questions.GetById(id);
            var model = Mapper.Map<QuestionAdminViewModel>(question);

            return model;
        }
    }
}
