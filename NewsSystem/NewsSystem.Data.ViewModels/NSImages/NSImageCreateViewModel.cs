namespace NewsSystem.Data.ViewModels.NSImages
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class NSImageCreateViewModel : IMapFrom<NSImage>
    {
        public NSImageCreateViewModel()
        {
            this.Tokens = new List<string>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field is required!")]
        public string Title { get; set; }

        [AllowHtml]
        public string Text { get; set; }

        public string ImageTags { get; set; }

        public ICollection<string> Tokens { get; set; }

        public byte[] ByteContent { get; set; }

        [Required(ErrorMessage = "Posted picture is required!")]
        public HttpPostedFileBase ImageBase { get; set; }
    }
}
