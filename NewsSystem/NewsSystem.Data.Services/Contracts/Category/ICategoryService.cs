namespace NewsSystem.Data.Services.Contracts.Category
{
    using Models.Groups;
    using ViewModels.Categories;

    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> GetAll();

        IEnumerable<CategoryCheckboxViewModel> GetAllCheckbox();

        SelectList GetDDL();

        void SaveCategorableEntityToCategories(ICategorableEntity entity, ICollection<CategoryCheckboxViewModel> categories);

        void SaveCategorableToCategories(ICategorableEntity entity, ICollection<long> categoriesIds);

        void RemoveCategoriesFromCategorableEntity(ICategorableEntity entity);
    }
}
