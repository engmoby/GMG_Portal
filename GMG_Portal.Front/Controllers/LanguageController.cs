using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Lang
        public ActionResult ChangeLanguage(string selectedLanguage)
        {
            if (selectedLanguage != null)
            {
                Thread.CurrentThread.CurrentCulture =
                    CultureInfo.CreateSpecificCulture(selectedLanguage);
                Thread.CurrentThread.CurrentUICulture =
                    new CultureInfo(selectedLanguage);
                var cookie = new HttpCookie("Language");
                cookie.Value = selectedLanguage;
                Response.Cookies.Add(cookie);
            }
            //   return RedirectToAction("Index", "Home");
            return Redirect(Request.UrlReferrer.ToString());

        }
    }
}