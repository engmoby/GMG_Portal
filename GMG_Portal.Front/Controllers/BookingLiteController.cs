using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMG_Portal.API.Models.Hotels.Reservation;

namespace Front.Controllers
{
    public class BookingLiteController : Controller
    {
        // GET: BookingLite
        public ActionResult Index(string checkin, string checkout, string adult, string child)
        {
            var reservation = new Reservation();
           // reservation.CheckIn = Convert.ToDateTime(checkin);
           // reservation.CheckOut = Convert.ToDateTime(checkout);
            reservation.Child = Convert.ToInt32(child);
            reservation.Adult = Convert.ToInt32(adult);
            return View(reservation);
        }
        [HttpPost]
        public ActionResult Index(Reservation collection)
        {
            try
            {

                //Get the Querystrings Sent from Index 
                var checkIn = Request.QueryString["CheckIn"];
                var checkOut = Request.QueryString["CheckOut"];
                var adult = Request.QueryString["Adult"];
                var child = Request.QueryString["Child"];



               // TODO: Add insert logic here
                            
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}