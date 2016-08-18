namespace NewsSystem.Data.ViewModels.Search
{
    using NSImages;

    using PagedList;

    public class SearchNSImagesGridViewModel : SearchViewModel
    {
        public PagedList<NSImageGridViewModel> NSImages { get; set; }

        public long? SelectedId { get; set; }
    }
}