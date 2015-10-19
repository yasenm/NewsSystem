namespace NewsSystem.Web.Controllers.Base
{
    using System.Web.Mvc;
    using System.Threading;

    using Microsoft.AspNet.Identity;

    public class BaseController : Controller
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
	}
}