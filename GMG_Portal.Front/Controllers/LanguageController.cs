using Front.Helpers;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class LanguageController : Controller
    {
        private readonly HttpClient _client;

        private string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";

        public LanguageController()
        {
            if (Common.CurrentLang == "ar")
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-EG");
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Lang
        public static void Change(string selectedLanguage)
        {
            Common.CurrentLang = selectedLanguage;
        }

        public ActionResult ChangeLanguage(string selectedLanguage)
        {
            if (selectedLanguage != null)
            {
                var cookie = new HttpCookie("Language");
                cookie.Value = selectedLanguage;
                Response.Cookies.Add(cookie);
                Common.CurrentLang = selectedLanguage;
                // Thread.CurrentThread.CurrentCulture =CultureInfo.CreateSpecificCulture(selectedLanguage);
                // Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
                //string general = url + "General/GetAll?langId=" + Common.CurrentLang;

                //var homeModels = new HomeModels();
                //var list = new List<Hotels>();

                //HttpResponseMessage responseMessage = await _client.GetAsync(general);
                //if (responseMessage.IsSuccessStatusCode)
                //{
                //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                //    var homesliders = JsonConvert.DeserializeObject<HomeModels>(responseData);
                //    homeModels = homesliders;
                //    foreach (var homeslider in homesliders.Hotels)
                //    {
                //        list.Add(new Hotels
                //        {
                //            Id = homeslider.Id,
                //            DisplayValue = homeslider.DisplayValue
                //        });
                //    }

                //}
                //Common.Hotels = list;
            }

            //Special Case for Details News
            //if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("News/Details/"))
            //{
            //    return Redirect(System.Configuration.ConfigurationManager.AppSettings["HomeUrl"] + "/News");
            //}

            ////Special Case for Details Offers
            //if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("Offers/offerdetails/"))
            //{
            //    return Redirect(System.Configuration.ConfigurationManager.AppSettings["HomeUrl"] + "/Offers");
            //}

            ////Special Case for Details Hotels
            //if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("Hotel/Details/"))
            //{
            //    return Redirect(System.Configuration.ConfigurationManager.AppSettings["HomeUrl"] + "/Hotel");
            //}

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}