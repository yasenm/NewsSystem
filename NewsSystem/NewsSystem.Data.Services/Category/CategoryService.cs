namespace NewsSystem.Data.Services.Albums
{
    using AutoMapper.QueryableExtensions;
    using Models;
    using Models.Groups;
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Category;
    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.Categories;

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class CategoryService : IDataService, ICategoryService
    {
        public INewsSystemData Data { get; set; }

        public CategoryService(INewsSystemData data)
        {
            this.Data = data;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var collection = this.Data.Categories.All()
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
            var collection = this.Data.Categories.All()
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
            var collection = this.Data.Categories.All()
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
                    var dbCategory = this.Data.Categories.All().FirstOrDefault(c => c.Id == catId);

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
            var entityIsAlbum = tagableEntity as Album;
            if (entityIsAlbum != null)
            {
                var album = this.Data.Albums.GetById(entityIsAlbum.Id);
                album.Categories.Add(dbCategory);
                this.Data.Albums.Update(album);
                dbCategory.Albums.Add(album);
            }
            var entityIsArticle = tagableEntity as Article;
            if (entityIsArticle != null)
            {
                var article = this.Data.Articles.GetById(entityIsArticle.Id);
                article.Categories.Add(dbCategory);
                this.Data.Articles.Update(article);
                dbCategory.Articles.Add(article);
            }
            this.Data.Categories.Update(dbCategory);
            this.Data.SaveChanges();
        }

        public void RemoveCategoriesFromCategorableEntity(ICategorableEntity entity)
        {
            var entityIsAlbum = entity as Album;
            if (entityIsAlbum != null)
            {
                var album = this.Data.Albums.GetById(entityIsAlbum.Id);
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
                var article = this.Data.Articles.GetById(entityIsArticle.Id);
                var categories = article.Categories;
                foreach (var category in categories)
                {
                    category.Articles.Remove(article);
                }
                article.Categories.Clear();
            }
        }
    }
}
