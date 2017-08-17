using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace GMG_Portal.UI.Controllers.Admin
{
    public class NewsLetterController : Controller
    {
        // GET: NewsLetter
        public ActionResult Index()
        {
            return View();
        }
      
    }
}