using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Front.Helpers;
using Newtonsoft.Json;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.Hotels;
using GMG_Portal.Data;


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
            string Offers = url + "Offers/GetAll?langId=" + Common.CurrentLang;
            var OffersModels = new List<Hotles_Offers>();

            if (Offers == null) throw new ArgumentNullException(nameof(Offers));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(Offers);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var OffersList = JsonConvert.DeserializeObject<List<Hotles_Offers>>(responseData);
                OffersModels = OffersList;
                if (OffersList.Count == 0)
                { 
                    TempData["alertNoOffers"] = "No Offers, Kindly redirect to home";

                    return RedirectToAction("Index", "Home");


                }
            }
            return View(OffersModels);

        }


        public async Task<ActionResult> OfferDetails(int id)
        {
            string offerdetails = url + "Offers/GetOfferDetails/" + id + "?langId=" + Common.CurrentLang;
            var offersModel = new Hotles_Offers();

            if (offerdetails == null) throw new ArgumentNullException(nameof(offerdetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(offerdetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                offersModel = JsonConvert.DeserializeObject<Hotles_Offers>(responseData);

            }

            return View(offersModel);
        }









    }
}