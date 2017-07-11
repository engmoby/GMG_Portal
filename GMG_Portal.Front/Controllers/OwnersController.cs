using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Front.Models;
using GMG_Portal.API.Models.Hotels.Hotel;
using Newtonsoft.Json;
using GMG_Portal.API.Models.SystemParameters;
namespace Front.Controllers
{
    public class OwnersController : Controller
    {
        // GET: Owners
        public ActionResult Index()
        {
            return View();
        }


     


    }
}