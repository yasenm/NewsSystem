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

        public bool SaveImageToDB(NSImageCreateViewModel model)
        {
            try
            {
                NSImage nsImage = this.ConvertPostedFileToNSImage(model.ImageBase);
                this.Data.NSImages.Add(nsImage);
                this.Data.SaveChanges();
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
                Album album = this.Data.Albums.GetById(albumId);
                nsImage.Albums.Add(album);
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
            var albumImages = this.GetAlbumImagesQueryable(albumId)
                .Project()
                .To<NSImageGridViewModel>()
                .ToList();

            return albumImages;
        }

        public ICollection<NSImageOnlyIdViewModel> GetAlbumImagesIds(long albumId)
        {
            var albumImages = this.GetAlbumImagesQueryable(albumId)
                .Project()
                .To<NSImageOnlyIdViewModel>()
                .ToList();

            return albumImages;
        }

        private IQueryable<NSImage> GetAlbumImagesQueryable(long albumId)
        {
            return this.Data.NSImages.All().Where(nsi => nsi.Albums.Where(a => a.Id == albumId).FirstOrDefault() != null);
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
                var coverImage = this.Data.NSImages.GetById(imageId);
                album.CoverImageId = coverImage.Id;
                album.CoverImage = coverImage;

                //this.Data.Albums.Update(album);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IQueryable<NSImageGridViewModel> GetImages()
        {
            var queryableImagesCollection = this.Data.NSImages.All()
                .OrderByDescending(nsi => nsi.CreatedOn)
                .Project()
                .To<NSImageGridViewModel>();

            return queryableImagesCollection;
        }

        public NSImageEditViewModel GetImageForEdit(long nsImageId)
        {
            var nsImage = this.Data.NSImages.GetById(nsImageId);
            var imageForEditModel = Mapper.Map<NSImageEditViewModel>(nsImage);

            return imageForEditModel;
        }

        public bool EditImage(NSImageEditViewModel model)
        {
            NSImage image = this.Data.NSImages.GetById(model.Id);

            if (model.Tokens.Count > 0)
            {
                var tokens = model.Tokens.ToList()[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                image.TokensNSImages.Clear();
                this.Data.SaveChanges();
                foreach (var token in tokens)
                {
                    var dbToken = this.Data.TokensNSImages
                        .All()
                        .FirstOrDefault(tnsi => tnsi.Name.ToLower() == token.ToLower());

                    if (dbToken == null)
                    {
                        dbToken = new TokenNSImage
                        {
                            Name = token,
                        };

                        this.Data.TokensNSImages.Add(dbToken);
                        this.Data.SaveChanges();
                    }

                    dbToken.NSImages.Add(image);
                    image.TokensNSImages.Add(dbToken);
                    this.Data.TokensNSImages.Update(dbToken);
                    this.Data.NSImages.Update(image);
                    this.Data.SaveChanges();
                }

                return true;
            }

            return false;
        }

        public bool RemoveFromAlbum(long imgId, long albumId)
        {
            try
            {
                var image = this.Data.NSImages.GetById(imgId);
                var album = this.Data.Albums.GetById(albumId);

                album.NSImages.Remove(image);
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
