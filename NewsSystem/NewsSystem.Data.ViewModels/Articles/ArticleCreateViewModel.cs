namespace NewsSystem.Data.ViewModels.Articles
{
    using Categories;
    using Common;
    using Infrastructure.Mapping;
    using Models;

    using System.Collections.Generic;

    public class ArticleCreateViewModel : DescribableEntityViewModel, IMapFrom<Article>
    {
        public List<CategoryCheckboxViewModel> ChosenCategories { get; set; }
    }
}
