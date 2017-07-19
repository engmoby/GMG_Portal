using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using GMG_Portal.API.Models.Hotels.Reservation;
using Newtonsoft.Json;


namespace Front.Controllers
{
    public class BookingLiteController : Controller
    {

        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";

        public BookingLiteController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }






        // GET: BookingLite
        public ActionResult Index(string checkin, string checkout, string adult, string child)
        {
            var reservation = new Reservation
            {
                CheckIn = Convert.ToDateTime(checkin),
                CheckOut = Convert.ToDateTime(checkout),
                Child = Convert.ToInt32(child),
                Adult = Convert.ToInt32(adult)
            };
            return View(reservation);
        }




        public ActionResult Confirm(string checkin, string checkout, string adult, string child)
        {
           
            return View();
        }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Index(Reservation reservation)
        { 
            if (ModelState.IsValid)
                {


                    //string ReservationObj = url + "Reservation/Save/" + reservation;

                    HttpResponseMessage responseMessageApi =await _client.PostAsJsonAsync("Reservation/Save/", reservation);
                    if (responseMessageApi.IsSuccessStatusCode)
                    {
                        var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                        reservation = JsonConvert.DeserializeObject<Reservation>(responseData);
                        if (reservation != null)
                        {
                            TempData["alertMessage"] = "ok";

                        }
                    }
                return View(reservation);

            }

            return View(reservation);
        }
    }
}