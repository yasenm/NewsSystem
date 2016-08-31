namespace NewsSystem.Data.ViewModels.Surveys
{
    using System.Collections.Generic;

    public class AnswersCollectionAdminViewModel
    {
        public int QuestionId { get; set; }

        public ICollection<AnswerAdminViewModel> Answers { get; set; }

        public AnswerAdminViewModel NewAnswer { get; set; }
    }
}
