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
    public class CareerController : Controller
    {
        // GET: Career
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
        public CareerController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Career
        public async Task<ActionResult> Index()
        {
            string career = "";
            career = url + "Career/GetAll";
            var careerModels = new List<Career>();

            if (career == null) throw new ArgumentNullException(nameof(career));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(career);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var careerList = JsonConvert.DeserializeObject<List<Career>>(responseData);
                careerModels = careerList;
            }

            return View(careerModels);

        } 
        public async Task<ActionResult> Details(int id)
        {
            string careerDetails = url + "Career/GetCareerDetails/" + id;
            var careerModels = new Career();

            if (careerDetails == null) throw new ArgumentNullException(nameof(careerDetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(careerDetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                careerModels = JsonConvert.DeserializeObject<Career>(responseData);

            }
            ViewBag.Title = careerModels.DisplayValue;
            string career = "";

            career = url + "Career/GetAll";

            var CareerModelList = new List<Career>();

            if (career == null) throw new ArgumentNullException(nameof(career));
            HttpResponseMessage responseMessage = await _client.GetAsync(career);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                ViewBag.CareerList = JsonConvert.DeserializeObject<List<Career>>(responseData);

            }

            return View(careerModels);
        }
    }
}