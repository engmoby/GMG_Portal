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
    public class OwnersController : Controller
    {
        // GET: Owners
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
        public OwnersController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: owner
        public async Task<ActionResult> Index()
        {
            string owners = url + "Owners/GetAll";
            var ownerModels = new List<Owners>();

            if (owners == null) throw new ArgumentNullException(nameof(owners));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(owners);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var ownersList = JsonConvert.DeserializeObject<List<Owners>>(responseData);
                ownerModels = ownersList;
            } 
            return View(ownerModels);

        }






    }
}