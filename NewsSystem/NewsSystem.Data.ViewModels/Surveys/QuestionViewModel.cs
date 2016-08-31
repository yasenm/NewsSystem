namespace NewsSystem.Data.ViewModels.Surveys
{
    using Common;
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    public class QuestionViewModel : DescribableEntityViewModel, IMapFrom<Question>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }

        public string NewAnswerDescription { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionViewModel>()
                .ForMember(m => m.Answers, opt => opt.MapFrom(src => src.Answers.Select(a => Mapper.Map<AnswerViewModel>(a))));
        }
    }
}
