namespace NewsSystem.Web.Helpers.Contracts
{
    using Grid.Mvc.Ajax.GridExtensions;

    using System.Linq;
    using System.Web.Mvc;

    public interface IGridMvcHelper
    {
        AjaxGrid<T> GetAjaxGrid<T>(IQueryable<T> items) where T : class;
        AjaxGrid<T> GetAjaxGrid<T>(IQueryable<T> items, int? page) where T : class;
        object GetGridJsonData<T>(AjaxGrid<T> grid, string gridPartialViewPath, Controller controller) where T : class;
    }
}
