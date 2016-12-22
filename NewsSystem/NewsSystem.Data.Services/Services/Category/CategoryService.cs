namespace NewsSystem.Data.Services.Category
{
    using AutoMapper.QueryableExtensions;
    using Models;
    using Models.Groups;
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Category;
    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.Categories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class CategoryService : ICategoryService
    {
        private INewsSystemData _data { get; set; }

        public CategoryService(INewsSystemData data)
        {
            this._data = data;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var collection = this._data.Categories.All()
                .Where(ac => ac.ParentId == null)
                .ToList()
                .AsQueryable()
                .Project()
                .To<CategoryViewModel>()
                .ToList();

            return collection;
        }

        public IEnumerable<CategoryCheckboxViewModel> GetAllCheckbox()
        {
            var collection = this._data.Categories.All()
                .Where(ac => ac.ParentId == null)
                .ToList()
                .AsQueryable()
                .Project()
                .To<CategoryCheckboxViewModel>()
                .ToList();

            return collection;
        }

        public SelectList GetDDL()
        {
            var collection = this._data.Categories.All()
                .Where(c => c.ParentId == null)
                .Project()
                .To<CategoryDDLViewModel>()
                .ToList();

            List<SelectListItem> ddl = new List<SelectListItem>();
            ddl.AddRange(new SelectList(collection, "Id", "Title"));

            return new SelectList(ddl, "Value", "Text");
        }

        public void SaveCategorableEntityToCategories(ICategorableEntity entity, ICollection<CategoryCheckboxViewModel> categories)
        {
            this.RemoveCategoriesFromCategorableEntity(entity);

            foreach (var parent in categories)
            {
                this.SaveCategorableToCategories(entity, parent.Children.Where(c => c.IsChecked).Select(c => c.Id).ToList());
            }
        }

        public void SaveCategorableToCategories(ICategorableEntity entity, ICollection<long> categoriesIds)
        {

            if (categoriesIds.Count > 0)
            {
                foreach (var catId in categoriesIds)
                {
                    var dbCategory = this._data.Categories.All().FirstOrDefault(c => c.Id == catId);

                    if (dbCategory == null)
                    {
                        continue;
                    }

                    this.SaveCategoryToCategorableEntity(entity, dbCategory);
                }
            }
        }

        private void SaveCategoryToCategorableEntity(ICategorableEntity tagableEntity, Category dbCategory)
        {
            // Should make it more Generic!!!!
            //var type = tagableEntity.GetType();
            //this.Data.GetDeletableEntityRepository<tagableEntity as de>()

            var entityIsAlbum = tagableEntity as Album;
            if (entityIsAlbum != null)
            {
                var album = this._data.Albums.GetById(entityIsAlbum.Id);
                album.Categories.Add(dbCategory);
                this._data.Albums.Update(album);
                dbCategory.Albums.Add(album);
            }
            var entityIsArticle = tagableEntity as Article;
            if (entityIsArticle != null)
            {
                var article = this._data.Articles.GetById(entityIsArticle.Id);
                article.Categories.Add(dbCategory);
                this._data.Articles.Update(article);
                dbCategory.Articles.Add(article);
            }
            this._data.Categories.Update(dbCategory);
            this._data.SaveChanges();
        }

        public void RemoveCategoriesFromCategorableEntity(ICategorableEntity entity)
        {
            var entityIsAlbum = entity as Album;
            if (entityIsAlbum != null)
            {
                var album = this._data.Albums.GetById(entityIsAlbum.Id);
                var categories = album.Categories;
                foreach (var category in categories)
                {
                    category.Albums.Remove(album);
                }
                album.Categories.Clear();
            }

            var entityIsArticle = entity as Article;
            if (entityIsArticle != null)
            {
                var article = this._data.Articles.GetById(entityIsArticle.Id);
                var categories = article.Categories;
                foreach (var category in categories)
                {
                    category.Articles.Remove(article);
                }
                article.Categories.Clear();
            }
        }

        public bool UpdateCategoryList(List<OrderedCategoryViewModel> model)
        {
            try
            {
                foreach (var catModel in model)
                {
                    var dbCat = _data.Categories.GetById(catModel.Id);
                    dbCat.Children.Clear();
                    foreach (var childCat in catModel.Children)
                    {
                        var dbChildCat = _data.Categories.GetById(childCat);
                        dbCat.Children.Add(dbChildCat);
                        dbChildCat.Parent = dbCat;
                        _data.Categories.Update(dbChildCat);
                    }
                    _data.Categories.Update(dbCat);
                }
                _data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditCatName(long id, string newName)
        {
            try
            {
                var dbCat = _data.Categories.GetById(id);
                dbCat.Title = newName.Trim();
                _data.Categories.Update(dbCat);
                _data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
