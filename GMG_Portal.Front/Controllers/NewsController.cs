using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
                return RedirectToAction("Index", "News");
            }

            var newsModel = new News();
            newsModel.LangId = "En";
            string news = "";
            if (id == 0)
                news = url + "News/GetAll?langId=" + "En";
            else
                news = url + "News/GetAllByCatrgoryId?categoryId=" + id + "&postedNews=" + newsModel;
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

        public async Task<ActionResult> IndexCat(int id)
        {
            string news = "";
            if (id == 0)
                news = url + "News/GetAll";
            else
                news = url + "News/GetAllByCatrgoryId?categoryId=" + id;
            // news = "http://localhost:2798/SystemParameters/News/GetAllByCatrgoryId?categoryId=2";
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
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "News");
            }
            string newsDetails = url + "News/GetNewsDetails/" + id + "?langId=" + "En";
            var newsModels = new News();

            if (newsDetails == null) throw new ArgumentNullException(nameof(newsDetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(newsDetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                newsModels = JsonConvert.DeserializeObject<News>(responseData);

            }

            string news = "";

            news = url + "News/GetAll";

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
            string news = url + "News/SearchNews?keyword=" + keyWord;
            // news = "http://localhost:2798/SystemParameters/News/GetAllByCatrgoryId?categoryId=2";
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