using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc; 
using GMG_Portal.API.Models.General;
using GMG_Portal.API.Models.Hotels.Hotel;
using Newtonsoft.Json;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.SystemParameters.ContactUs;

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
        public async Task<ActionResult> HeaderNavBar()
        {
            string General = url + "General/GetAll";
            //string _homeSlider = url + "HomeSliders/GetAll";
            //string _about = url + "About/GetAll";
            //string _HotelFeatures = url + "Features/GetAll";
            //string _News = url + "News/GetAll";
            //string _Hotels = url + "Hotels/GetAll";
            //string gallery = url + "Hotels/GetAllImages";
            //string _Owners = url + "Owners/GetAll";
            //string _ContactUs = url + "ContactUs/GetAll";
            //await CallHomeSliders(_homeSlider, homeModels);
            //await CallAbout(_about, homeModels);
            //await CallFacilities(_HotelFeatures, homeModels);
            //await CallHotels(_Hotels, homeModels);
            //await Callowners(_Owners, homeModels);
            //await CallNews(_News, gallery, homeModels);
            //await CallContactus(_ContactUs, homeModels);

            var homeModels = new HomeModels();


            HttpResponseMessage responseMessage = await _client.GetAsync(General);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var homesliders = JsonConvert.DeserializeObject<HomeModels>(responseData);
                homeModels = homesliders;
            }

           // return View(homeModels);
            return PartialView("_HeaderNavBar");
        }
        public async Task<ActionResult> Index()
        {
            string General = url + "General/GetAll";
            //string _homeSlider = url + "HomeSliders/GetAll";
            //string _about = url + "About/GetAll";
            //string _HotelFeatures = url + "Features/GetAll";
            //string _News = url + "News/GetAll";
            //string _Hotels = url + "Hotels/GetAll";
            //string gallery = url + "Hotels/GetAllImages";
            //string _Owners = url + "Owners/GetAll";
            //string _ContactUs = url + "ContactUs/GetAll";
            //await CallHomeSliders(_homeSlider, homeModels);
            //await CallAbout(_about, homeModels);
            //await CallFacilities(_HotelFeatures, homeModels);
            //await CallHotels(_Hotels, homeModels);
            //await Callowners(_Owners, homeModels);
            //await CallNews(_News, gallery, homeModels);
            //await CallContactus(_ContactUs, homeModels);

            var homeModels = new HomeModels();

             
            HttpResponseMessage responseMessage = await _client.GetAsync(General);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var homesliders = JsonConvert.DeserializeObject<HomeModels>(responseData);
                homeModels = homesliders; 
            }

            return View(homeModels);

        }
     
        public ActionResult Error()
        {
            return View();

        }
        // Home Partial Views Loading 
        public ActionResult Slider()
        {
            return Slider();
        }

        //public ActionResult OurHotels()
        //{
        //    return OurHotels();
        //}

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


        private async Task<string> CallHomeSliders(string homeSlider, HomeModels homeModels)
        {
            var returnValue = "";
            HttpResponseMessage responseMessage = await _client.GetAsync(homeSlider);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var homesliders = JsonConvert.DeserializeObject<List<HomeSlider>>(responseData);
                homeModels.HomeSliders = homesliders;
                returnValue = HttpStatusCode.OK.ToString();
            }
            if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                returnValue = HttpStatusCode.InternalServerError.ToString();
            }
            return returnValue;
        }

        //Fill Models with data Retrieved

        private async Task<string> CallGeneral(string general)
        {
            var returnValue = "";
            HttpResponseMessage responseMessage = await _client.GetAsync(general);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                //var homesliders = JsonConvert.DeserializeObject<List<HomeSlider>>(responseData);
                //homeModels.HomeSliders = homesliders;
                //returnValue = HttpStatusCode.OK.ToString();
            }
            if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                returnValue = HttpStatusCode.InternalServerError.ToString();
            }
            return returnValue;
        }

        private async Task CallAbout(string _about, HomeModels homeModels)
        {
            //if (_about == null) throw new ArgumentNullException(nameof(_about));
            //HttpResponseMessage responseMessageApi = await _client.GetAsync(_about);
            //if (responseMessageApi.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
            //    var about = JsonConvert.DeserializeObject<List<About>>(responseData);
            //    homeModels.About = about;
            //}
        }

        private async Task CallFacilities(string hotelFeatures, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageApi = await _client.GetAsync(hotelFeatures);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var features = JsonConvert.DeserializeObject<List<Features>>(responseData);
                homeModels.Features = features;
            }
        }

        private async Task CallHotels(string _Hotels, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageApi = await _client.GetAsync(_Hotels);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var hotels = JsonConvert.DeserializeObject<List<Hotels>>(responseData);
                homeModels.Hotels = hotels;
            }
        }


        private async Task Callowners(string _Owners, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageApi = await _client.GetAsync(_Owners);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var owners = JsonConvert.DeserializeObject<List<Owners>>(responseData);
                homeModels.Owners = owners;
            }
        }

        private async Task CallNews(string _News, string gallery, HomeModels homeModels)
        {
            HttpResponseMessage responseMessageApi = await _client.GetAsync(_News);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var news = JsonConvert.DeserializeObject<List<News>>(responseData);
                homeModels.News = news;
            }

            HttpResponseMessage responseMessageGallery = await _client.GetAsync(gallery);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseDataGallery = responseMessageGallery.Content.ReadAsStringAsync().Result;
                homeModels.Gallery = JsonConvert.DeserializeObject<List<HotelImages>>(responseDataGallery);

            }
        }

        private async Task CallContactus(string _ContactUs, HomeModels homeModels)
        {
            //HttpResponseMessage responseMessageApi = await _client.GetAsync(_ContactUs);
            //if (responseMessageApi.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
            //    var contactUs = JsonConvert.DeserializeObject<List<ContactUs>>(responseData);
            //    homeModels.ContactUs = contactUs;
            //}
        }

    }
}