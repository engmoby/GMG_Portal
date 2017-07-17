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


        public ActionResult Upload(CareerForm careerForm)
        {
            ViewBag.CareerId = careerForm.CareerId;
            ViewBag.CareerTitle = careerForm.CareerTitle;
            return View(careerForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(CareerForm careerForm, HttpPostedFileBase file)
        { 
            string fileName = "";
            var fileDetails = new List<FileDetail>();
            if (ModelState.IsValid)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    // var file = Request.Files[i];

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

                        var path = Path.Combine(Server.MapPath("~/App_Data/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }
                careerForm.Attach = fileName;
                careerForm.CareerId = careerForm.CareerId;

                var careerFormLogic = new CareerFormLogic();
                SystemParameters_CareerForm careerFormaaaCareerForm = null;

                careerFormaaaCareerForm = careerFormLogic.Insert(Mapper.Map<SystemParameters_CareerForm>(careerForm));


                //string careerDetails = url + "Career/Save/" + careerForm;

                //HttpResponseMessage responseMessageApi = await _client.GetAsync(careerDetails);
                //if (responseMessageApi.IsSuccessStatusCode)
                //{
                //    var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                //    careerForm = JsonConvert.DeserializeObject<CareerForm>(responseData);

                //}
                return RedirectToAction("Index");
            }

            return View(careerForm);
        }
    }
}