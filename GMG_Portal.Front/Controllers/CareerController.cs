using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Front.Resources;
using GMG_Portal.API.Models.General;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.SystemParameters.CareerForm;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.Data;
using Newtonsoft.Json;

namespace Front.Controllers
{
    public class CareerController : Controller
    {
        // GET: Career
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServerIp"] + "/SystemParameters/";
        public CareerController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Career
        [HandleError]
        public async Task<ActionResult> Index()
        {
            string career = "";
            career = url + "Career/GetAll";
            var careerModels = new List<Career>();

            if (career == null) throw new ArgumentNullException(nameof(career));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(career);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                var careerList = JsonConvert.DeserializeObject<List<Career>>(responseData);
                careerModels = careerList;
            }

            return View(careerModels);

        }
        public async Task<ActionResult> Details(int id)
        {
            string careerDetails = url + "Career/GetCareerDetails/" + id;
            var careerModels = new Career();
            var careerForm = new CareerForm();

            if (careerDetails == null) throw new ArgumentNullException(nameof(careerDetails));
            HttpResponseMessage responseMessageApi = await _client.GetAsync(careerDetails);
            if (responseMessageApi.IsSuccessStatusCode)
            {
                var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                careerModels = JsonConvert.DeserializeObject<Career>(responseData);
                careerForm.CareerId = careerModels.Id;
                careerForm.CareerTitle = careerModels.DisplayValue;
            }
            ViewBag.Title = careerModels.DisplayValue;
            return RedirectToAction("Upload", careerForm);

            //return View(careerForm);
        }


        [HandleError]
        public ActionResult Upload(CareerForm careerForm)
        {
            return View(careerForm);
        }

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(CareerForm careerForm, HttpPostedFileBase file)
        {
            string fileName = "";
            var fileDetails = new List<FileDetail>();

            
            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                FileDetail fileDetail = new FileDetail()
                {
                    FileName = fileName,
                    Extension = Path.GetExtension(fileName),
                    Id = Guid.NewGuid()
                };
                fileDetails.Add(fileDetail);

                var path = Path.Combine(Server.MapPath("~/Uploads/"), fileDetail.Id + fileDetail.Extension);
                file.SaveAs(path);
            }
            careerForm.Attach = fileDetails[0].Id.ToString() + fileDetails[0].Extension.ToString();

            careerForm.CareerId = careerForm.CareerId;

            if (string.IsNullOrEmpty(careerForm.FirstName))
                ModelState.AddModelError("FirstName", Global.Last_Name_Required);
            if (string.IsNullOrEmpty(careerForm.LastName))
                ModelState.AddModelError("LastName", Global.Last_Name_Required);
            if (string.IsNullOrEmpty(careerForm.Email))
                ModelState.AddModelError("Email", Global.Email_Required);
            if (string.IsNullOrEmpty(careerForm.PhoneNo))
                ModelState.AddModelError("PhoneNo", Global.PhoneNo_Required);
            if (string.IsNullOrEmpty(careerForm.Attach))
                ModelState.AddModelError("Attach", Global.Attach_file_Required);

            if (ModelState.IsValid)
            {
                 
                HttpResponseMessage responseMessageApi = await _client.PostAsJsonAsync("CareerForm/Save/", careerForm);
                if (responseMessageApi.IsSuccessStatusCode)
                {
                    var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                    careerForm = JsonConvert.DeserializeObject<CareerForm>(responseData);
                    if (careerForm != null)
                    {
                        TempData["alertMessage"] = "Thanks, Kindly our team will contact with you shortly";

                    }
                }
                return RedirectToAction("Index");
            }

            return View(careerForm);
        }
    }
}