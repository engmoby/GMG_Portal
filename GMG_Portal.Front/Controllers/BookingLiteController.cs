﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Front.Resources;
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




        public ActionResult Confirm(Reservation reservation)
        {

            return View(reservation);
        }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Reservation reservation)
        {
            if (string.IsNullOrEmpty(reservation.FirstName))
                ModelState.AddModelError("FirstName", Global.First_Name_Required);
            if (string.IsNullOrEmpty(reservation.LastName))
                ModelState.AddModelError("LastName", Global.Last_Name_Required);
            if (string.IsNullOrEmpty(reservation.Email))
                ModelState.AddModelError("Email", Global.Email_Required);
            if (reservation.Phone == 0)
                ModelState.AddModelError("PhoneNo", Global.PhoneNo_Required);

            if (ModelState.IsValid)
            {
                HttpResponseMessage responseMessageApi = await _client.PostAsJsonAsync("Reservation/Save/", reservation);
                if (responseMessageApi.IsSuccessStatusCode)
                {
                    var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                    reservation = JsonConvert.DeserializeObject<Reservation>(responseData);
                    if (reservation != null)
                    {
                        TempData["alertMessage"] = "ok";
                    }
                }
                return RedirectToAction("Confirm", reservation);

            }
            return View(reservation);

        }
    }
}