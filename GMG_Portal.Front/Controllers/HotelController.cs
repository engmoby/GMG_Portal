using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Front.Helpers;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.Data;
using Newtonsoft.Json;

namespace Front.Controllers
{
    public class HotelController : Controller
    {
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
        public HotelController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Hotel
        public async Task<ActionResult> Index()
        {
            string hotels = url + "Hotels/GetAll?langId=" + Common.CurrentLang;
            var hotelModels = new List<Hotels>();

            if (hotels == null) throw new ArgumentNullException(nameof(hotels));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(hotels);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var hotelsList = JsonConvert.DeserializeObject<List<Hotels>>(responseData);
                hotelModels = hotelsList;
            }

            return View(hotelModels);

        }


        // GET: Hotel/Details/5
        public async Task<ActionResult> Details(int id)
        {
           // string hotelDetails = url + "Hotels/GetHotelDetails/"+ id+ "?langId=\" + Common.CurrentLang";
            string hotelDetails = url + "Hotels/GetHotelDetails/"+ id+ "?langId=" + Helpers.Common.CurrentLang;
            var hotelModels = new Hotel();

            if (hotelDetails == null) throw new ArgumentNullException(nameof(hotelDetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(hotelDetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
               var obj  = JsonConvert.DeserializeObject<Hotel>(responseData);
                hotelModels = obj;
            }

            return View(hotelModels);
        }
   
       
    }
}
