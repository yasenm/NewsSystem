namespace NewsSystem.Data.Services.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Web;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Models;
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.NSImages;
    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.NSImages;

    public class NSImageService : IDataService, INSImageService
    {
        public NSImage ConvertPostedFileToNSImage(HttpPostedFileBase imageFile)
        {
            using (var memory = new MemoryStream())
            {
                imageFile.InputStream.CopyTo(memory);

                var imageBytes = memory.GetBuffer();
                var imageTitle = imageFile.FileName;
                var extension = imageTitle.Substring(imageTitle.LastIndexOf('.'));

                var nsImage = new NSImage
                {
                    ByteContent = imageBytes,
                    Text = imageTitle,
                    ImageTags = imageTitle,
                    Extension = extension,
                };

                return nsImage;
            }
        }

        public INewsSystemData Data { get; set; }

        public NSImageService(INewsSystemData data)
        {
            this.Data = data;
        }

        public bool SaveImagesToAlbum(ICollection<HttpPostedFileBase> images, long albumId)
        {
            try
            {
                foreach (var image in images)
                {
                    var succeded = this.SaveImageToDB(image, albumId);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SaveImageToDB(HttpPostedFileBase imageFile)
        {
            try
            {
                NSImage nsImage = this.ConvertPostedFileToNSImage(imageFile);
                this.Data.NSImages.Add(nsImage);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SaveImageToDB(HttpPostedFileBase imageFile, long albumId)
        {
            try
            {
                NSImage nsImage = this.ConvertPostedFileToNSImage(imageFile);
                nsImage.AlbumId = albumId;
                this.Data.NSImages.Add(nsImage);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ICollection<NSImageGridViewModel> GetAlbumImages(long albumId)
        {
            var albumImages = this.Data.NSImages.All()
                .Where(nsi => nsi.AlbumId == albumId)
                .Project()
                .To<NSImageGridViewModel>()
                .ToList();

            return albumImages;
        }

        public ICollection<NSImageOnlyIdViewModel> GetAlbumImagesIds(long albumId)
        {
            var albumImages = this.Data.NSImages.All()
                .Where(nsi => nsi.AlbumId == albumId)
                .Project()
                .To<NSImageOnlyIdViewModel>()
                .ToList();

            return albumImages;
        }

        public NSImageGridViewModel GetImageById(long albumId)
        {
            var nsImage = this.Data.NSImages.GetById(albumId);
            var result = Mapper.Map<NSImageGridViewModel>(nsImage);

            return result;
        }

        public bool ChangeAlbumCoverByImageId(long imageId, long albumId)
        {
            try
            {
                var album = this.Data.Albums.GetById(albumId);
                album.CoverImageId = imageId;

                this.Data.Albums.Update(album);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
