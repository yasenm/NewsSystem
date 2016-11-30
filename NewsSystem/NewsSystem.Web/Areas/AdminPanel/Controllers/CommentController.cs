using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    public class CommentController : AdminBaseController
    {
        // GET: AdminPanel/Comment
        public ActionResult Index()
        {
            return View();
        }
    }
}