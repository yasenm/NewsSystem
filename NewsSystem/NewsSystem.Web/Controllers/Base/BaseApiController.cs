
namespace NewsSystem.Web.Controllers.Base
{
    using Microsoft.AspNet.Identity;

    using System.Threading;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}
