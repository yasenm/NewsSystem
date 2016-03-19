namespace NewsSystem.Web.Areas.AdminPanel.Controllers.Base
{
    using NewsSystem.Common.Constants;

    using System.IO;
    using System.Web.Mvc;

    [Authorize(Roles = UsersConstants.AdminRole)]
    public abstract class AdminBaseController : Controller
    {
        protected string GetRenderedPartial(string viewName, object model = null)
        {
            using (var sw = new StringWriter())
            {
                if (ControllerContext != null)
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View, new ViewDataDictionary(model), TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                }

                return sw.ToString();
            }
        }
    }
}