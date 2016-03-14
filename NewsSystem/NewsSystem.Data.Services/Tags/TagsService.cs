namespace NewsSystem.Data.Services.Tags
{
    using Models;
    using Models.Groups;
    using Contracts;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UnitOfWork;

    public class TagsService : IDataService, ITagsService
    {
        public INewsSystemData Data { get; set; }

        public TagsService(INewsSystemData data)
        {
            this.Data = data;
        }

        public ICollection<string> GetAllTagsNames()
        {
            var result = this.Data.Tags.All().Select(t => t.Name).ToList();
            return result;
        }

        public ICollection<Tag> GetTagsByTitle(ICollection<string> tagNames)
        {
            var result = this.Data.Tags.All().Where(t => tagNames.Contains(t.Name.ToLower())).ToList();
            return result;
        }

        public bool SaveTagsToTagableEntity(ITagableEntity tagableEntity, ICollection<string> choosenTags)
        {
            if (choosenTags.Count > 0)
            {
                this.RemoveTagsFromTagableEntity(tagableEntity);
                var tags = this.GetTagsArray(choosenTags);

                foreach (var tag in tags)
                {
                    var dbTag = this.Data.Tags
                        .All()
                        .FirstOrDefault(tnsi => tnsi.Name.ToLower().Trim() == tag.ToLower().Trim());

                    if (dbTag == null)
                    {
                        dbTag = new Tag
                        {
                            Name = tag,
                        };

                        this.Data.Tags.Add(dbTag);
                        this.Data.SaveChanges();
                    }
                    else
                    {
                        this.Data.Tags.Update(dbTag);
                    }

                    this.SaveTagToTagableEntity(tagableEntity, dbTag);
                    this.Data.SaveChanges();
                }
            }
            return false;
        }

        private void SaveTagToTagableEntity(ITagableEntity tagableEntity, Tag dbTag)
        {
            var entityIsAlbum = tagableEntity as Album;
            if (entityIsAlbum != null)
            {
                var album = this.Data.Albums.GetById(entityIsAlbum.Id);
                album.Tags.Add(dbTag);
                this.Data.Albums.Update(album);
            }
            var entityIsArticle = tagableEntity as Article;
            if (entityIsArticle != null)
            {
                var article = this.Data.Articles.GetById(entityIsArticle.Id);
                article.Tags.Add(dbTag);
                this.Data.Articles.Update(article);
            }
            var entityIsNSImage = tagableEntity as NSImage;
            if (entityIsNSImage != null)
            {
                var nsImage = this.Data.NSImages.GetById(entityIsNSImage.Id);
                nsImage.Tags.Add(dbTag);
                this.Data.NSImages.Update(nsImage);
            }
        }

        private void RemoveTagsFromTagableEntity(ITagableEntity tagableEntity)
        {
            var entityIsAlbum = tagableEntity as Album;
            if (entityIsAlbum != null)
            {
                var album = this.Data.Albums.GetById(entityIsAlbum.Id);
                var tags = album.Tags;
                foreach (var tag in tags)
                {
                    tag.Albums.Remove(album);
                }
                album.Tags.Clear();
            }

            var entityIsArticle = tagableEntity as Article;
            if (entityIsArticle != null)
            {
                var article = this.Data.Articles.GetById(entityIsArticle.Id);
                var tags = article.Tags;
                foreach (var tag in tags)
                {
                    tag.Articles.Remove(article);
                }
                article.Tags.Clear();
            }

            var entityIsNSImage = tagableEntity as NSImage;
            if (entityIsNSImage != null)
            {
                var nsImage = this.Data.NSImages.GetById(entityIsNSImage.Id);
                var tags = nsImage.Tags;
                foreach (var tag in tags)
                {
                    tag.NSImages.Remove(nsImage);
                }
                nsImage.Tags.Clear();
            }

            this.Data.SaveChanges();
        }

        private string[] GetTagsArray(ICollection<string> rawTags)
        {
            return rawTags.ToList()[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
