namespace NewsSystem.Data.Services.Surveys
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Contracts.Surveys;
    using NewsSystem.Data.Services.Contracts;
    using UnitOfWork;
    using ViewModels.Surveys;

    using System;
    using System.Linq;
    using Models;
    using System.Collections.Generic;

    public class AnswersService : IDataService, IAnswersService
    {
        public INewsSystemData _data { get; set; }

        public AnswersService(INewsSystemData data)
        {
            this._data = data;
        }

        public IQueryable<AnswerViewModel> GetAllAnswers()
        {
            return this._data.Answers.All()
                .Project()
                .To<AnswerViewModel>();
        }

        public AnswerViewModel GetById(int id)
        {
            var answer = this._data.Answers.GetById(id);
            var model = Mapper.Map<AnswerViewModel>(answer);

            return model;
        }

        public bool Create(AnswerAdminViewModel model)
        {
            try
            {
                var actual = Mapper.Map<Answer>(model);
                this._data.Answers.Add(actual);
                this._data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Edit(AnswerAdminViewModel model)
        {
            try
            {
                var actual = Mapper.Map<Answer>(model);
                this._data.Answers.Update(actual);
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
                this._data.Answers.Delete(id);
                this._data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<AnswerViewModel> GetAllAnswersForQuestion(int questionId)
        {
            return this._data.Answers.All()
                .Where(a => a.QuestionId == questionId)
                .Project()
                .To<AnswerViewModel>()
                .ToList();
        }
    }
}
