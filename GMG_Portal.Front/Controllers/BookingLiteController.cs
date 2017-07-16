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
        public ActionResult Index()
        {
            return View();
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



                var dd = collection.FirstName;
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