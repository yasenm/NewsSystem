using NewsSystem.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class TagController : BaseController
    {
        public ActionResult NewsTags(int newsId)
        {

            return View();
        }
    }
}