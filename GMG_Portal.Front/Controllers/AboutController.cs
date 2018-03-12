using Front.Helpers;
using GMG_Portal.API.Models.SystemParameters;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class AboutController : Controller
    {
        // GET: About Content
        private readonly HttpClient _client;

        private string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";

        public AboutController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: About Content
        public async Task<ActionResult> Index()
        {
            string about = url + "About/GetAll?langId=" + Common.CurrentLang;
            var aboutModels = new AboutModel();

            if (about == null) throw new ArgumentNullException(nameof(about));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(about);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var aboutList = JsonConvert.DeserializeObject<AboutModel>(responseData);
                aboutModels = aboutList;
            }
            return View(aboutModels);
        }
    }
}