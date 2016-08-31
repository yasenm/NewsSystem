namespace NewsSystem.Data.ViewModels.Surveys
{
    using Infrastructure.Mapping;
    using Models;

    public class AnswerAdminViewModel : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
