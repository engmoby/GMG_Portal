using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GMG_Portal.API.Models.SystemParameters;

namespace Front.Controllers
{
    public class AboutController : Controller
    {
        // GET: About Content
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
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

          



            string About = url + "About/GetAll";
            var AboutModels = new List<About>();

            if (About == null) throw new ArgumentNullException(nameof(About));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(About);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var AboutList = JsonConvert.DeserializeObject<List<About>>(responseData);
                AboutModels = AboutList;
            }
            return View(AboutModels);

        }




    



    }
}