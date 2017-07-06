using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GMG_Portal.API;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Business;
using Newtonsoft.Json;
using School.Models;
using HomeSlider = School.Models.HomeSlider;

namespace School.Controllers
{
    public class HomeController : Controller
    {
        readonly HttpClient _client; 
        string url = "http://localhost:2798/SystemParameters/HomeSliders/GetAll";
         
        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        { 
            HttpResponseMessage responseMessage = await _client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var home = JsonConvert.DeserializeObject<List<HomeSlider>>(responseData);
                var homeModels= new HomeModels();
                foreach (var forHomeSlider in home)
                {
                    homeModels.HomeSliders.Add(new HomeSlider
                    {
                        
                        DisplayValue = forHomeSlider.DisplayValue,
                        Id = forHomeSlider.Id,
                        DisplayValueDesc = forHomeSlider.DisplayValueDesc,
                        Image = forHomeSlider.Image
                    });
                }


                return View(homeModels);
            }
            return View("Error");

        }

    }
}