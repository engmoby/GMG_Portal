using Front.Helpers;
using GMG_Portal.API.Models.SystemParameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _client;

        private string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";

        public NewsController()
        {
            if (Common.CurrentLang == "ar")
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

            var newsModel = new NewsModel();
            newsModel.LangId = "En";
            string news = "";
            if (id == 0)
                news = url + "News/GetAll?langId=" + Common.CurrentLang;
            else
                news = url + "News/GetAllByCatrgoryId?categoryId=" + id + "&langId=" + Common.CurrentLang;
            var newsModels = new List<NewsModel>();

            if (news == null) throw new ArgumentNullException(nameof(news));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(news);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var newsList = JsonConvert.DeserializeObject<List<NewsModel>>(responseData);
                newsModels = newsList;
                if (newsList.Count == 0)
                {
                    TempData["alertMessage"] = "No News for this category, Kindly redirect to home";
                    TempData["alertNoNews"] = "No News, Kindly redirect to home";

                    return RedirectToAction("Index", "Error");
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
                news = url + "News/GetAllByCatrgoryId?categoryId=" + id + "&langId=" + Common.CurrentLang;

            var newsModels = new List<NewsModel>();

            if (news == null) throw new ArgumentNullException(nameof(news));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(news);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var newsList = JsonConvert.DeserializeObject<List<NewsModel>>(responseData);
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
            var newsModels = new NewsModel();

            if (newsDetails == null) throw new ArgumentNullException(nameof(newsDetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(newsDetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                newsModels = JsonConvert.DeserializeObject<NewsModel>(responseData);
            }

            string news = "";

            news = url + "News/GetLatestNewsWithOutCurrentId?newsId=" + id + "&langId= " + Common.CurrentLang;

            var newsModelList = new List<NewsModel>();

            if (news == null) throw new ArgumentNullException(nameof(news));
            HttpResponseMessage responseMessage = await _client.GetAsync(news);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                ViewBag.NewsList = JsonConvert.DeserializeObject<List<NewsModel>>(responseData);
            }

            return View(newsModels);
        }

        public async Task<ActionResult> Search(string keyWord)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string news = url + "News/SearchNews?keyword=" + keyWord + "&langId=" + Common.CurrentLang;

            var newsresult = new List<NewsModel>();

            if (news == null) throw new ArgumentNullException(nameof(news));
            // HttpResponseMessage responseMessageApi = await _client.GetAsync(news);
            HttpResponseMessage responseMessageApi = await _client.GetAsync(news);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var newsList = JsonConvert.DeserializeObject<List<NewsModel>>(responseData);
                newsresult = newsList;
            }
            // return RedirectToAction("SearchResults",newsresult);

            return View("SearchResult", newsresult);

            //return View(newsModels);
        }

        //public ActionResult SearchResults(List<News> newsList)
        //{
        //    return View("SearchResult",newsList);

        //}
    }
}