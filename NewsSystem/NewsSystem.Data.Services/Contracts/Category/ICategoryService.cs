namespace NewsSystem.Data.Services.Contracts.Category
{
    using System.Collections.Generic;

    using NewsSystem.Data.ViewModels.Categories;

    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> GetAll();

        IEnumerable<CategoryDDLViewModel> GetForDDLAll();
    }
}
