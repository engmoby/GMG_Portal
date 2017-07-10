using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Front.Models;
using Newtonsoft.Json;
using GMG_Portal.API.Models.SystemParameters;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        readonly HttpClient _client;
       
        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
        public HomeController()
        {
          
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            string _homeSlider = url + "HomeSliders/GetAll";
            string _about = url + "Abouts/GetAll";
            string _HotelFeatures = url + "Features/GetAll";
            string _News = url + "News/GetAll";
            string _Hotels = url + "Hotels/GetAll";



            var homeModels = new HomeModels();
        

            await CallHomeSliders(_homeSlider, homeModels);
            await CallAbout(_about, homeModels);
            await CallFacilities(_HotelFeatures, homeModels);
            await CallHotels(_Hotels, homeModels);
            await CallNews(_News, homeModels);


            return View(homeModels);

        }
       // Home Partial Views Loading 
        public ActionResult Slider()
        {
            return Slider();
        }

        public ActionResult OurHotels()
        {
            return OurHotels();
        }

        public ActionResult AboutHome()
        {
            return AboutHome();
        }

        public ActionResult HotelFacilities()
        {
            return HotelFacilities();
        }

        public ActionResult OwnersHome()
        {
            return OwnersHome();
        }

        public ActionResult NewsHome()
        {
            return NewsHome();
        }





         //Fill Models with data Retrieved
        private async Task CallHomeSliders(string _homeSlider, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageAbout = await _client.GetAsync(_homeSlider);
            if (responseMessageAbout.IsSuccessStatusCode)
            {
                var responseData = responseMessageAbout.Content.ReadAsStringAsync().Result;
                var homesliders = JsonConvert.DeserializeObject<List<HomeSlider>>(responseData);
                homeModels.HomeSliders = homesliders;
            }
        }
    
        private async Task CallAbout(string _about, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageAbout = await _client.GetAsync(_about);
            if (responseMessageAbout.IsSuccessStatusCode)
            {
                var responseData = responseMessageAbout.Content.ReadAsStringAsync().Result;
                var about = JsonConvert.DeserializeObject<List<About>>(responseData);
                homeModels.About = about;
            }
        }

        private async Task CallFacilities(string _HotelFeatures, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageAbout = await _client.GetAsync(_HotelFeatures);
            if (responseMessageAbout.IsSuccessStatusCode)
            {
                var responseData = responseMessageAbout.Content.ReadAsStringAsync().Result;
                var features = JsonConvert.DeserializeObject<List<Features>>(responseData);
                homeModels.Features = features;
            }
        }

        private async Task CallHotels(string _Hotels, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageAbout = await _client.GetAsync(_Hotels);
            if (responseMessageAbout.IsSuccessStatusCode)
            {
                var responseData = responseMessageAbout.Content.ReadAsStringAsync().Result;
                var hotels = JsonConvert.DeserializeObject<List<Hotels>>(responseData);
                homeModels.Hotels = hotels;
            }
        }

        private async Task CallNews(string _News, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageAbout = await _client.GetAsync(_News);
            if (responseMessageAbout.IsSuccessStatusCode)
            {
                var responseData = responseMessageAbout.Content.ReadAsStringAsync().Result;
                var news = JsonConvert.DeserializeObject<List<News>>(responseData);
                homeModels.News = news;
            }
        }


    }
}