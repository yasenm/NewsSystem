namespace NewsSystem.Data.ViewModels.Common
{
    using Data.Common.Models;
    using Infrastructure.Mapping;

    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public abstract class DescribableEntityViewModel : IMapFrom<DescribableEntity>
    {
        [StringLength(200,  MinimumLength = 4, ErrorMessage = "You must use more than 4 and less than 200 characters")]
        public string Title { get; set; }

        [AllowHtml]
        [StringLength(5000, MinimumLength = 4, ErrorMessage = "You must use more than 4 and less than 5000 characters")]
        public string Description { get; set; }

        [AllowHtml]
        [StringLength(5000, MinimumLength = 4, ErrorMessage = "You must use more than 4 and less than 5000 characters")]
        [Required(ErrorMessage = "The field is required!")]
        public string Summary { get; set; }

        public string Author { get; set; }
    }
}
