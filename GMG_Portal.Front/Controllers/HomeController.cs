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
using HomeSlider = School.Models.HomeSlider;

namespace School.Controllers
{
    public class HomeController : Controller
    {
        readonly HttpClient client;
        //The URL of the WEB API Service
        string url = "http://localhost:2798/SystemParameters/HomeSliders/GetAll";

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            //var objLogic = new GMG_Portal.API.Controllers.SystemParameters.HomeSlidersController();
            //var list = objLogic.GetAllWithDeleted();
            //  var viewModel = new HomeSlider();
            //if (list != null)
            //{
            //    foreach (var VARIABLE in list)
            //    {

            //    }
            //    return View(viewModel);
            //}
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<List<HomeSlider>>(responseData);

                return View(Employees);
            }
            return View("Error");

        }

    }
}