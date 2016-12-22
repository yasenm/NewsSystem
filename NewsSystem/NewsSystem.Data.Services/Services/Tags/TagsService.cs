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
        public INewsSystemData _data { get; set; }

        public TagsService(INewsSystemData data)
        {
            this._data = data;
        }

        public ICollection<string> GetAllTagsNames()
        {
            var result = this._data.Tags.All().Select(t => t.Name).ToList();
            return result;
        }

        public ICollection<Tag> GetTagsByTitle(ICollection<string> tagNames)
        {
            var result = this._data.Tags.All().Where(t => tagNames.Contains(t.Name.ToLower())).ToList();
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
                    var dbTag = this._data.Tags
                        .All()
                        .FirstOrDefault(tnsi => tnsi.Name.ToLower().Trim() == tag.ToLower().Trim());

                    if (dbTag == null)
                    {
                        dbTag = new Tag
                        {
                            Name = tag,
                        };

                        this._data.Tags.Add(dbTag);
                        this._data.SaveChanges();
                    }
                    else
                    {
                        this._data.Tags.Update(dbTag);
                    }

                    this.SaveTagToTagableEntity(tagableEntity, dbTag);
                    this._data.SaveChanges();
                }
            }
            return false;
        }

        private void SaveTagToTagableEntity(ITagableEntity tagableEntity, Tag dbTag)
        {
            var entityIsAlbum = tagableEntity as Album;
            if (entityIsAlbum != null)
            {
                var album = this._data.Albums.GetById(entityIsAlbum.Id);
                album.Tags.Add(dbTag);
                this._data.Albums.Update(album);
            }
            var entityIsArticle = tagableEntity as Article;
            if (entityIsArticle != null)
            {
                var article = this._data.Articles.GetById(entityIsArticle.Id);
                article.Tags.Add(dbTag);
                this._data.Articles.Update(article);
            }
            var entityIsNSImage = tagableEntity as NSImage;
            if (entityIsNSImage != null)
            {
                var nsImage = this._data.NSImages.GetById(entityIsNSImage.Id);
                nsImage.Tags.Add(dbTag);
                this._data.NSImages.Update(nsImage);
            }
        }

        private void RemoveTagsFromTagableEntity(ITagableEntity tagableEntity)
        {
            var entityIsAlbum = tagableEntity as Album;
            if (entityIsAlbum != null)
            {
                var album = this._data.Albums.GetById(entityIsAlbum.Id);
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
                var article = this._data.Articles.GetById(entityIsArticle.Id);
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
                var nsImage = this._data.NSImages.GetById(entityIsNSImage.Id);
                var tags = nsImage.Tags;
                foreach (var tag in tags)
                {
                    tag.NSImages.Remove(nsImage);
                }
                nsImage.Tags.Clear();
            }

            this._data.SaveChanges();
        }

        private string[] GetTagsArray(ICollection<string> rawTags)
        {
            return rawTags.ToList()[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
