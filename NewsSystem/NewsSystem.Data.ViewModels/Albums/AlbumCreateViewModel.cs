namespace NewsSystem.Data.ViewModels.Albums
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Common.Constants.Error;
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;
    using System.Web.Mvc;

    public class AlbumCreateViewModel : IMapFrom<Album>
    {
        public AlbumCreateViewModel()
        {
            this.Tokens = new List<string>();
        }

        [StringLength(200, MinimumLength = 4, ErrorMessage = "You must use more than 4 and less than 200 characters")]
        public string Name { get; set; }
        
        [AllowHtml]
        [StringLength(5000, MinimumLength = 4, ErrorMessage = "You must use more than 4 and less than 5000 characters")]
        public string Text { get; set; }

        public long AlbumCategoryId { get; set; }

        public ICollection<string> Tokens { get; set; }
    }
}
