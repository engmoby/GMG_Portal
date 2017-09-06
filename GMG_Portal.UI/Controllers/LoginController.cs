using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GMG_Portal.API.Models.General;
using Newtonsoft.Json;

namespace GMG_Portal.UI.Controllers
{
    public class LoginController : Controller
    {
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServicesURL"] + "/SystemParameters/";

        public LoginController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        // GET: Login
        public ActionResult Index()
        {
            //if (Request.Cookies["Global"] != null) 
            //return RedirectToAction("Index", "Admin"); 
            //else 
            Response.Cookies.Remove("Global");
            return View();
        }
        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(API.Models.SystemParameters.Admin.Admin admin)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage responseMessageApi = await _client.PostAsJsonAsync("Admin/Login/", admin);
                if (responseMessageApi.IsSuccessStatusCode)
                {
                    var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                    var retunObj = JsonConvert.DeserializeObject<API.Models.SystemParameters.Admin.Admin>(responseData);
                    if (retunObj != null)
                    {
                        var cookie = new HttpCookie("Global");
                        cookie.Value = retunObj.UserName;
                        Response.Cookies.Add(cookie);
                        Response.Expires= 1;
                        TempData["alertMessage"] = "Thanks, Kindly our team will contact with you shortly";

                    }
                }
                return RedirectToAction("Index", "Admin");
            }

            return View(admin);
        }

    }
}