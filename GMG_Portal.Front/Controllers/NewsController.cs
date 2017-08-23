using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Front.Helpers;
using GMG_Portal.API.Models.SystemParameters;
using Newtonsoft.Json;

namespace Front.Controllers
{
    public class NewsController : Controller
    {
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
        public NewsController()
        {
            if (Common.CurrentLang== "ar")
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-EG");  


            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: news
        public async Task<ActionResult> Index(int? id)
        {
             
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "News", new { Id = 0 });
            }

            var newsModel = new News();
            newsModel.LangId = "En";
            string news = "";
            if (id == 0)
                news = url + "News/GetAll?langId=" + Common.CurrentLang;
            else
                news = url + "News/GetAllByCatrgoryId?categoryId=" + id + "&langId=" + Common.CurrentLang;
            var newsModels = new List<News>();


            if (news == null) throw new ArgumentNullException(nameof(news));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(news);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var newsList = JsonConvert.DeserializeObject<List<News>>(responseData);
                newsModels = newsList;
                if (newsList.Count == 0)
                {
                    TempData["alertMessage"] = "No News for this category, Kindly redirect to home";
                    TempData["alertNoNews"] = "No News, Kindly redirect to home";

                    return RedirectToAction("Index", "Home");


                }
            }

            return View(newsModels);

        }

        public async Task<ActionResult> IndexCat(int id)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string news = "";
            if (id == 0)
                news = url + "News/GetAll?langId=" + Common.CurrentLang;
            else
                news = url + "News/GetAllByCatrgoryId?categoryId="  + id + "&langId=" + Common.CurrentLang;

            var newsModels = new List<News>();


            if (news == null) throw new ArgumentNullException(nameof(news));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(news);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var newsList = JsonConvert.DeserializeObject<List<News>>(responseData);
                newsModels = newsList;
            }

            return View(newsModels);

        }
        public async Task<ActionResult> Details(int? id)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "News", new { Id = 0 });
            }
            string newsDetails = url + "News/GetNewsDetails/" + id + "?langId=" + Common.CurrentLang;
            var newsModels = new News();

            if (newsDetails == null) throw new ArgumentNullException(nameof(newsDetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(newsDetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                newsModels = JsonConvert.DeserializeObject<News>(responseData);

            }

            string news = "";

            news = url + "News/GetAll?langId=" + Common.CurrentLang;

            var newsModelList = new List<News>();

            if (news == null) throw new ArgumentNullException(nameof(news));
            HttpResponseMessage responseMessage = await _client.GetAsync(news);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                ViewBag.NewsList = JsonConvert.DeserializeObject<List<News>>(responseData);

            }

            return View(newsModels);
        }

        public async Task<ActionResult> Search(string keyWord)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string news = url + "News/SearchNews?keyword=" + keyWord + "&langId=" + Common.CurrentLang;

            var newsModels = new List<News>();


            if (news == null) throw new ArgumentNullException(nameof(news));
            // HttpResponseMessage responseMessageApi = await _client.GetAsync(news);
            HttpResponseMessage responseMessageApi = await _client.GetAsync(news);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var newsList = JsonConvert.DeserializeObject<List<News>>(responseData);
                newsModels = newsList;
            }
            return RedirectToAction("SearchResult", newsModels);

            //return View(newsModels);

        }
        public ActionResult SearchResult(List<News> newsList)
        {
            return View(newsList);

        }
    }
}