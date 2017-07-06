using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Front.Models;
using Newtonsoft.Json;
using About = Front.Models.About;
using HomeSlider = Front.Models.HomeSlider;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        readonly HttpClient _client;
        string url = "http://localhost:2798/SystemParameters/";

        public HomeController()
        {
            //this._about = about;
            //this._homeSlider = homeSlider;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            string _homeSlider = url + "HomeSliders/GetAll";
            string _about = url + "Abouts/GetAll";
            var _HotelFeatures = url + "Features/GetAll";
            var _News = url + "News/GetAll";
            var homeModels = new HomeModels();
            var homesliders = new List<HomeSlider>();
            var aboutList = new List<About>();
            var hotelFeatures = new List<HotelFeatures>();
            var newsList = new List<News>();


            await CallApi(_homeSlider, homesliders, homeModels);
            await CallAbout(_about, aboutList, homeModels);
            await CallFacilities(_HotelFeatures, hotelFeatures, homeModels);
            await CallNews(_News, newsList, homeModels);


            return View(homeModels);

        }

        private async Task CallAbout(string _about, List<About> aboutList, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageAbout = await _client.GetAsync(_about);
            if (responseMessageAbout.IsSuccessStatusCode)
            {
                var responseData = responseMessageAbout.Content.ReadAsStringAsync().Result;
                var about = JsonConvert.DeserializeObject<List<About>>(responseData);


                foreach (var forAbout in about)
                {
                    aboutList.Add(new About
                    {
                        DisplayValue = forAbout.DisplayValue,
                        Id = forAbout.Id,
                        DisplayValueDesc = forAbout.DisplayValueDesc,
                        URL = forAbout.URL
                    });
                }
                homeModels.About = aboutList;
            }
        }
         
        private async Task CallFacilities(string _HotelFeatures, List<HotelFeatures> hotelFeatureslist, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageAbout = await _client.GetAsync(_HotelFeatures);
            if (responseMessageAbout.IsSuccessStatusCode)
            {
                var responseData = responseMessageAbout.Content.ReadAsStringAsync().Result;
                var featureslist = JsonConvert.DeserializeObject<List<HotelFeatures>>(responseData);


                foreach (var forAbout in featureslist)
                {
                    hotelFeatureslist.Add(new HotelFeatures
                    {
                        DisplayValue = forAbout.DisplayValue,
                        Id = forAbout.Id,
                        DisplayValueDesc = forAbout.DisplayValueDesc,
                        URL = forAbout.URL
                    });
                }
                homeModels.Features = hotelFeatureslist;
            }
        }

        private async Task CallApi(string homeSlider, List<HomeSlider> homesliders, HomeModels homeModels)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(homeSlider);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var home = JsonConvert.DeserializeObject<List<HomeSlider>>(responseData);

                foreach (var forHomeSlider in home)
                {
                    homesliders.Add(new HomeSlider
                    {
                        DisplayValue = forHomeSlider.DisplayValue,
                        Id = forHomeSlider.Id,
                        DisplayValueDesc = forHomeSlider.DisplayValueDesc,
                        Image = forHomeSlider.Image
                    });
                }
                homeModels.HomeSliders = homesliders;

                // return View(homeModels);
            }
        }

        private async Task CallNews(string news, List<News> newslist, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageNews = await _client.GetAsync(news);
            if (responseMessageNews.IsSuccessStatusCode)
            {
                var responseData = responseMessageNews.Content.ReadAsStringAsync().Result;
                var retunNewlist = JsonConvert.DeserializeObject<List<News>>(responseData);


                foreach (var forAbout in retunNewlist)
                {
                    newslist.Add(new News
                    {
                        DisplayValue = forAbout.DisplayValue,
                        Id = forAbout.Id,
                        DisplayValueDesc = forAbout.DisplayValueDesc,
                        URL = forAbout.URL
                    });
                }
                homeModels.News = newslist;
            }
        }

    }
}