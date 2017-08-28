using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Front.Helpers;
using Front.Resources;
using GMG_Portal.API.Models.Hotels.Reservation;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.Data;
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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");



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
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (string.IsNullOrEmpty(reservation.FirstName))
                ModelState.AddModelError("FirstName", Global.First_Name_Required);
            if (string.IsNullOrEmpty(reservation.LastName))
                ModelState.AddModelError("LastName", Global.Last_Name_Required);
            if (string.IsNullOrEmpty(reservation.Email))
                ModelState.AddModelError("Email", Global.Email_Required);
            if (string.IsNullOrEmpty(reservation.Phone))
                ModelState.AddModelError("PhoneNo", Global.PhoneNo_Required);
         



            if (ModelState.IsValid)
            {

             


                HttpResponseMessage responseMessageApi = await _client.PostAsJsonAsync("Reservation/Save/", reservation);
            
                if (responseMessageApi.IsSuccessStatusCode)
                {
                    //Send Instant Notifications to Selected Personnells
                    HttpResponseMessage responseMessageEmails = await _client.GetAsync(url + "DepartmentNotifyController/GetAllNotify?Department=Reservation");
                    if (responseMessageEmails.IsSuccessStatusCode)
                    {


                        var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                        reservation = JsonConvert.DeserializeObject<Reservation>(responseData);
                        if (reservation != null)
                        {
                            TempData["alertMessage"] = "ok";
                        }


                        //Start Send Instant Notifications
                        var responseDataEmails = responseMessageEmails.Content.ReadAsStringAsync().Result;
                        var emailModel = JsonConvert.DeserializeObject<List<NotifyViewModel>>(responseDataEmails);



                        string emailMessage = "FirstName : " + reservation.FirstName + "<br/>" + "Last Name : " +
                                              reservation.LastName + "<br/>" +
                                              "Email : " + reservation.Email + "<br/>" + "Phone Number :" +
                                              reservation.Phone +
                                              "<br />" + "Check In : " + reservation.CheckIn + "<br/>" + "Check out : " +
                                              reservation.CheckOut + "<br/>" +
                                              "Adult : " + reservation.Adult + "<br/>" + "Child : " + reservation.Child + "<br/>" +
                                              "Hotel Name : " + reservation.HotelName;

                        var notifyemail = new NotifyEmail();
                        notifyemail.SendMail("Reservation Request : " + reservation.FirstName, emailMessage, emailModel);
                    }
                    /////







                   
                }
                return RedirectToAction("Confirm", reservation);

            }
            return View(reservation);

        }
    }
}