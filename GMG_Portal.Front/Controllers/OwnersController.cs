using Front.Helpers;
using GMG_Portal.API.Models.SystemParameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class OwnersController : Controller
    {
        // GET: Owners
        private readonly HttpClient _client;

        private string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";

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
            string owners = url + "Owners/GetAll?langId=" + Common.CurrentLang;
            var ownerModels = new List<OwnerModel>();

            if (owners == null) throw new ArgumentNullException(nameof(owners));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(owners);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var ownersList = JsonConvert.DeserializeObject<List<OwnerModel>>(responseData);
                ownerModels = ownersList;
            }
            return View(ownerModels);
        }
    }
}