﻿using Front.Helpers;
using GMG_Portal.API.Models.Hotels.Hotel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class HotelController : Controller
    {
        private readonly HttpClient _client;

        private string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";

        public HotelController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Hotel
        [HandleError]
        public async Task<ActionResult> Index()
        {
            string hotels = url + "Hotels/GetAll?langId=" + Common.CurrentLang;
            var hotelModels = new List<HotelsModel>();

            if (hotels == null) throw new ArgumentNullException(nameof(hotels));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(hotels);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var hotelsList = JsonConvert.DeserializeObject<List<HotelsModel>>(responseData);
                hotelModels = hotelsList;
            }

            return View(hotelModels);
        }

        // GET: Hotel/Details/5
        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Hotel");
            }
            //Fix for Map Co-Ordinates on Multi Lingual
            Thread.CurrentThread.CurrentCulture = Common.CurrentLang == "ar" ? new CultureInfo("ar-EG") : new CultureInfo("en-US");
            // string hotelDetails = url + "Hotels/GetHotelDetails/"+ id+ "?langId=\" + Common.CurrentLang";
            string hotelDetails = url + "Hotels/GetHotelDetails/" + id + "?langId=" + Helpers.Common.CurrentLang;
            var hotelModels = new HotelsModel();

            if (hotelDetails == null) throw new ArgumentNullException(nameof(hotelDetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(hotelDetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<HotelsModel>(responseData);
                hotelModels = obj;
            }

            return View(hotelModels);
        }
    }
}