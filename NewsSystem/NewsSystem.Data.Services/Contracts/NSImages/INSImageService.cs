namespace NewsSystem.Data.Services.Contracts.NSImages
{
    using System.Collections.Generic;
    using System.Web;

    using NewsSystem.Data.Models;
    using NewsSystem.Data.ViewModels.NSImages;
    using System.Linq;

    public interface INSImageService
    {
        bool SaveImagesToAlbum(ICollection<HttpPostedFileBase> images, long albumId);

        bool SaveImageToDB(NSImageCreateViewModel model);

        bool SaveImageToDB(HttpPostedFileBase imageFile); 

        bool SaveImageToDB(HttpPostedFileBase imageFile, long albumId);

        NSImage ConvertPostedFileToNSImage(HttpPostedFileBase imageFile);

        ICollection<NSImageGridViewModel> GetAlbumImages(long albumId);

        ICollection<NSImageOnlyIdViewModel> GetAlbumImagesIds(long albumId);

        NSImageGridViewModel GetImageById(long albumId);

        bool ChangeAlbumCoverByImageId(long imageId, long albumId);

        IQueryable<NSImageGridViewModel> GetImages();

        IQueryable<NSImageGridViewModel> GetImages(string text, string tags);

        IQueryable<NSImageGridViewModel> GetImagesToChoose(string text, string tags, long albumId);

        NSImageEditViewModel GetImageForEdit(long nsImageId);

        bool EditImage(NSImageEditViewModel model);

        bool RemoveFromAlbum(long imgId, long albumId);

        bool PushImagesToAlbum(long albumId, long[] imagesIds);
    }
}
