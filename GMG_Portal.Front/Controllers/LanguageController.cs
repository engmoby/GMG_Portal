using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Front.Helpers;

namespace Front.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Lang
        public ActionResult ChangeLanguage(string selectedLanguage)
        {
            if (selectedLanguage != null)
            {
                Common.CurrentLang = selectedLanguage;
               // Thread.CurrentThread.CurrentCulture =CultureInfo.CreateSpecificCulture(selectedLanguage);
               // Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
                var cookie = new HttpCookie("Language");
                cookie.Value = selectedLanguage;
                Response.Cookies.Add(cookie);
            }

            //Special Case for Details News
            if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("News/Details/"))
            {
                return Redirect(System.Configuration.ConfigurationManager.AppSettings["HomeUrl"] + "/News");
            }

            //Special Case for Details Offers
            if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("Offers/GetOfferDetails/"))
            {
                return Redirect(System.Configuration.ConfigurationManager.AppSettings["HomeUrl"] + "/Offers");
            }

            //Special Case for Details Hotels
            if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("Hotels/Details/"))
            {
                return Redirect(System.Configuration.ConfigurationManager.AppSettings["HomeUrl"] + "/Hotels");
            }



            return Redirect(Request.UrlReferrer.ToString());

        }
    }
}