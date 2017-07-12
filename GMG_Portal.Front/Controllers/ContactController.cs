using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.SystemParameters.ContactUs;

namespace Front.Controllers
{
    public class ContactController : Controller
    {



        // GET: About Content
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
        public ContactController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: About Content
        public async Task<ActionResult> Index()
        {





            string contactUs = url + "ContactUs/GetAll";
            var contactUsModels = new List<ContactUs>();

            if (contactUs == null) throw new ArgumentNullException(nameof(contactUs));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(contactUs);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var contactUsList = JsonConvert.DeserializeObject<List<ContactUs>>(responseData);
                contactUsModels = contactUsList;
            }
            return View(contactUsModels);

        }












    }
}