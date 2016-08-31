namespace NewsSystem.Data.ViewModels.Surveys
{
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;
    using NewsSystem.Data.ViewModels.Common;

    public class AnswerViewModel : DescribableEntityViewModel, IMapFrom<Answer>
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
    }
}
