namespace NewsSystem.Data.ViewModels.Albums
{
    using Categories;
    using Common;
    using Infrastructure.Mapping;
    using Models;

    using System.Collections.Generic;

    public class AlbumCreateViewModel : DescribableEntityViewModel, IMapFrom<Album>
    {
        public AlbumCreateViewModel()
        {
            this.Tags = new List<string>();
        }

        public ICollection<string> Tags { get; set; }

        public List<CategoryCheckboxViewModel> ChosenCategories { get; set; }
    }
}
