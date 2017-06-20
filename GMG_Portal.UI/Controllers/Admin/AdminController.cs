using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMG_Portal.UI.Controllers.Admin
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Layout
        public ActionResult pageTop()
        {
            return View();
        }

        public ActionResult ContentTop()
        {
            return View();
        }

        public ActionResult BaSidebar()
        {
            return View();
        }

        public ActionResult MsgCenter()
        {
            return View();
        }
    }
}