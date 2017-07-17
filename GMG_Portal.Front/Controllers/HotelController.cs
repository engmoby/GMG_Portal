using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc; 
using GMG_Portal.API.Models.Hotels.Hotel;
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
            string hotels = url + "Hotels/GetAll";
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
            string hotelDetails = url + "Hotels/GetHotelDetails/"+ id;
            var hotelModels = new Hotels();

            if (hotelDetails == null) throw new ArgumentNullException(nameof(hotelDetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(hotelDetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                hotelModels = JsonConvert.DeserializeObject<Hotels>(responseData);
               
            }

            return View(hotelModels);
        }

        // GET: Hotel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hotel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hotel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hotel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hotel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
