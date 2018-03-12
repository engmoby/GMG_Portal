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
    public class OffersController : Controller
    {
        // GET: Offers
        private readonly HttpClient _client;

        private string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";

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
            string offers = url + "Offers/GetAll?langId=" + Common.CurrentLang;
            var offersModels = new List<OfferModel>();

            if (offers == null) throw new ArgumentNullException(nameof(offers));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(offers);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var offersList = JsonConvert.DeserializeObject<List<OfferModel>>(responseData);
                offersModels = offersList;
                if (offersList.Count == 0)
                {
                    TempData["alertNoOffers"] = "No Offers, Kindly redirect to home";

                    return RedirectToAction("Index", "Error");
                }
            }
            return View(offersModels);
        }

        public async Task<ActionResult> OfferDetails(int id)
        {
            string offerdetails = url + "Offers/GetOfferDetails/" + id + "?langId=" + Common.CurrentLang;
            var offersModel = new OfferModel();

            if (offerdetails == null) throw new ArgumentNullException(nameof(offerdetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(offerdetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                offersModel = JsonConvert.DeserializeObject<OfferModel>(responseData);
            }

            return View(offersModel);
        }
    }
}