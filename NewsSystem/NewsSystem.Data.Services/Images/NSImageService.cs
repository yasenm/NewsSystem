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
                    Description = imageTitle,
                    ImageTags = imageTitle,
                    Extension = extension,
                };

                return nsImage;
            }
        }

        public INewsSystemData Data { get; set; }
        private ITagsService TagsService { get; set; }

        public NSImageService(INewsSystemData data, ITagsService tagsService)
        {
            this.Data = data;
            this.TagsService = tagsService;
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
                NSImage image = this.ConvertPostedFileToNSImage(model.ImageBase);
                image.Title = model.Title;
                image.Description = model.Text;
                this.Data.NSImages.Add(image);
                this.Data.SaveChanges();

                this.TagsService.SaveTagsToTagableEntity(image, model.Tags);
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
            var album = this.Data.Albums.GetById(albumId);
            var images = this.Data.NSImages.All()
                .Where(nsi => nsi.Albums.FirstOrDefault(a => a.Id == albumId) != null); //.Where(a => a.Id == albumId).FirstOrDefault() != null);
            return images;
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

        public IQueryable<NSImageGridViewModel> GetImages(string text, string tags)
        {
            var queryableImagesCollection = this.Data.NSImages.All();

            if (text != string.Empty && text != null)
            {
                queryableImagesCollection = queryableImagesCollection.Where(nsi => nsi.Title.Contains(text) || nsi.Description.Contains(text));
            }
            if (tags != string.Empty && tags != null)
            {
                string[] sTags = tags.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tag in sTags)
                {
                    queryableImagesCollection = queryableImagesCollection.Where(nsi => nsi.Tags.FirstOrDefault(t => t.Name.Contains(tag)) != null);
                }
            }
            var result = queryableImagesCollection.OrderByDescending(nsi => nsi.CreatedOn).Project().To<NSImageGridViewModel>();

            return result;
        }

        public IQueryable<NSImageGridViewModel> GetImagesToChoose(string text, string tags, long albumId)
        {
            if (text == null)
            {
                text = string.Empty;
            }
            var album = this.Data.Albums.GetById(albumId);
            var queryableImagesCollection = this.Data.NSImages.All()
                .Where(nsi => nsi.Albums.Where(a => a.Id == albumId).FirstOrDefault() == null);

            if (text != string.Empty && text != null)
            {
                queryableImagesCollection = queryableImagesCollection.Where(nsi => nsi.Title.Contains(text.Trim().ToLower()) || nsi.Description.Contains(text.Trim().ToLower()));
            }

            var result = queryableImagesCollection.OrderByDescending(nsi => nsi.CreatedOn).Project().To<NSImageGridViewModel>();

            return result;
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
            image.Title = model.Title;
            image.Description = model.Description;
            image.Summary = model.Summary;

            if (model.PostedContent != null)
            {
                image.ByteContent = this.ConvertPostedFileToNSImage(model.PostedContent).ByteContent;
            }

            this.Data.SaveChanges();

            if (model.Tags.Count > 0)
            {
                this.TagsService.SaveTagsToTagableEntity(image, model.Tags);

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
                image.Albums.Remove(album);

                this.Data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool PushImagesToAlbum(long albumId, long[] imagesIds)
        {
            try
            {
                Album album = this.Data.Albums.GetById(albumId);
                foreach (var imageId in imagesIds)
                {
                    NSImage nsImage = this.Data.NSImages.GetById(imageId);
                    album.NSImages.Add(nsImage);
                    nsImage.Albums.Add(album);
                    this.Data.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }

        public bool IsImageCoverForAlbum(long imageId, long albumId)
        {
            throw new NotImplementedException();
        }

        public bool CheckIsImageAlbumCover(long imageId, long albumId)
        {
            var album = this.Data.Albums.GetById(albumId);
            return album.CoverImageId == imageId;
        }
    }
}
