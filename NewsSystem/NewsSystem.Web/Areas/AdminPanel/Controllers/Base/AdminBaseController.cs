namespace NewsSystem.Web.Areas.AdminPanel.Controllers.Base
{
    using System.Web.Mvc;

    using NewsSystem.Common.Constants;

    [Authorize(Roles = UsersConstants.AdminRole)]
    public abstract class AdminBaseController : Controller
    {

    }
}