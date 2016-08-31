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
        public INewsSystemData Data { get; set; }

        public AnswersService(INewsSystemData data)
        {
            this.Data = data;
        }

        public IQueryable<AnswerViewModel> GetAllAnswers()
        {
            return this.Data.Answers.All()
                .Project()
                .To<AnswerViewModel>();
        }

        public AnswerViewModel GetById(int id)
        {
            var answer = this.Data.Answers.GetById(id);
            var model = Mapper.Map<AnswerViewModel>(answer);

            return model;
        }

        public bool Create(AnswerAdminViewModel model)
        {
            try
            {
                var actual = Mapper.Map<Answer>(model);
                this.Data.Answers.Add(actual);
                this.Data.SaveChanges();
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
                this.Data.Answers.Update(actual);
                this.Data.SaveChanges();
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
                this.Data.Answers.Delete(id);
                this.Data.SaveChanges();
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<AnswerViewModel> GetAllAnswersForQuestion(int questionId)
        {
            return this.Data.Answers.All()
                .Where(a => a.QuestionId == questionId)
                .Project()
                .To<AnswerViewModel>()
                .ToList();
        }
    }
}
