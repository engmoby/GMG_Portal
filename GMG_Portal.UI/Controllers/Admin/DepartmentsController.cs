using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMG_Portal.UI.Controllers.Admin
{
    public class DepartmentsController : Controller
    {
        // GET: AccountTypes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult edit()
        {
            return View();
        }
    }
}