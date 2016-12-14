using System;

namespace NewsSystem.Data.ViewModels.Shared
{
    public class StatsViewModel
    {
        public DateTime? CreatedOn { get; set; }
        public int VisitorsCount { get; set; }
        public int CommentsCount { get; set; }
    }
}