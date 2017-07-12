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
    public class OffersController : Controller
    {

        // GET: Offers
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
        public OffersController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Offers
        public async Task<ActionResult> Index()
        {
            string Offers = url + "Offers/GetAll";
            var OffersModels = new List<Offer>();

            if (Offers == null) throw new ArgumentNullException(nameof(Offers));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(Offers);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var OffersList = JsonConvert.DeserializeObject<List<Offer>>(responseData);
                OffersModels = OffersList;
            }
            return View(OffersModels);

        }









    }
}