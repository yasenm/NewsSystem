namespace NewsSystem.Data.ViewModels.Articles
{
    using Categories;
    using Common;
    using Infrastructure.Mapping;
    using Models;

    using System.Collections.Generic;

    public class ArticleCreateViewModel : DescribableEntityViewModel, IMapFrom<Article>
    {
        public bool IsMain { get; set; }

        public bool IsTopMain { get; set; }

        public List<CategoryCheckboxViewModel> ChosenCategories { get; set; }

        public long CoverImageId { get; set; }

        public long? ThemeId { get; set; }
    }
}
