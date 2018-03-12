using Front.Helpers;
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class ContactController : Controller
    {
        // GET: About Content
        private readonly HttpClient _client;

        private string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";

        public ContactController()
        {
            Thread.CurrentThread.CurrentCulture = Common.CurrentLang == "ar" ? new CultureInfo("ar-EG") : new CultureInfo("en-US");
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: About Content
        public async Task<ActionResult> Index()
        {
            string contactUs = url + "ContactUs/GetAll?langId=" + Common.CurrentLang;
            var contactUsModels = new ContactUsModel();

            if (contactUs == null) throw new ArgumentNullException(nameof(contactUs));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(contactUs);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var contactUsList = JsonConvert.DeserializeObject<ContactUsModel>(responseData);
                contactUsModels = contactUsList;
            }
            return View(contactUsModels);
        }
    }
}