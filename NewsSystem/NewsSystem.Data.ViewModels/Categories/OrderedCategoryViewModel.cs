using System.Collections.Generic;

namespace NewsSystem.Data.ViewModels.Categories
{
    public class OrderedCategoryViewModel
    {
        public long Id { get; set; }
        public List<long> Children { get; set; }
    }
}