namespace NewsSystem.Data.Services.Contracts.NSImages
{
    using System.Collections.Generic;
    using System.Web;

    using NewsSystem.Data.Models;
    using NewsSystem.Data.ViewModels.NSImages;

    public interface INSImageService
    {
        bool SaveImagesToAlbum(ICollection<HttpPostedFileBase> images, long albumId);

        bool SaveImageToDB(HttpPostedFileBase imageFile);

        bool SaveImageToDB(HttpPostedFileBase imageFile, long albumId);

        NSImage ConvertPostedFileToNSImage(HttpPostedFileBase imageFile);

        ICollection<NSImageGridViewModel> GetAlbumImages(long albumId);

        ICollection<NSImageOnlyIdViewModel> GetAlbumImagesIds(long albumId);

        NSImageGridViewModel GetImageById(long albumId);

        bool ChangeAlbumCoverByImageId(long imageId, long albumId);
    }
}
