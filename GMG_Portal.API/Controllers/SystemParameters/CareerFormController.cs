using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Data;
using GMG_Portal.Business.Logic.SystemParameters;
using AutoMapper;
using GMG_Portal.API.Helpers;
using GMG_Portal.API.Models.SystemParameters.CareerForm; 
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/CareerForm")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CareerFormController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var careerFormLogic = new CareerFormLogic();
                var careerForm = careerFormLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CareerForm>>(careerForm));

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetAllWithDeleted()
        {
            try
            {
                var careerFormLogic = new CareerFormLogic();
                var careerForms = careerFormLogic.GetAllWithSeen();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CareerForm>>(careerForms));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(CareerForm postedCareerForms)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var careerFormLogic = new CareerFormLogic();
                    SystemParameters_CareerForm careerForm = null;
                    if (postedCareerForms.Id.Equals(0))
                    {
             
                        careerForm = careerFormLogic.Insert(Mapper.Map<SystemParameters_CareerForm>(postedCareerForms));

                        //Instant Notifications Logic
                        var notifyemail = new NotifyEmail();
                        var departmentLogic = new DepartmentLogic();
                        var notifyLogic = new NotifyLogic();
                        var obj = departmentLogic.GetDepartmentByName("Career");
                        var objList = notifyLogic.GetNotifyByDepId(obj.Id);
                    
                        string emailMessage = "Job Applied For :  " + postedCareerForms.CareerTitle + "<br/>" +
                            "First Name : " + postedCareerForms.FirstName + "<br/>" +
                                              "Last Name : " + postedCareerForms.LastName + "<br/>" +
                                              "Email : " + postedCareerForms.Email + "<br/>" + "Phone Number :" +
                                              postedCareerForms.PhoneNo +
                                              "<br />" + "Message : " + postedCareerForms.Message + "<br/>" +
                                              "C.v  : <a href='" + System.Configuration.ConfigurationManager.AppSettings["HomeUrl"] +
                                              "/Uploads/" + postedCareerForms.Attach + "'> Download / View Applicant C.v</a><br/>";
                        notifyemail.SendMail("Careers Notification :  " + postedCareerForms.CareerTitle, emailMessage, objList);
                    }
                    else
                    {
                        careerForm = careerFormLogic.Edit(Mapper.Map<SystemParameters_CareerForm>(postedCareerForms));
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<CareerForm>(careerForm));
                }
                goto ThrowBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        [HttpPost] 
        public HttpResponseMessage Download(CareerForm postedCareerForms)
        {
            try
            {
                string fileName = postedCareerForms.Attach;
                //if (name.Equals("pdf", StringComparison.InvariantCultureIgnoreCase))
                //{
                //    fileName = "SamplePdf.pdf";
                //}
                //else if (name.Equals("zip", StringComparison.InvariantCultureIgnoreCase))
                //{
                //    fileName = "SampleZip.zip";
                //}

                if (!string.IsNullOrEmpty(fileName))
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/Uploads/") + fileName;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            byte[] bytes = new byte[file.Length];
                            file.Read(bytes, 0, (int)file.Length);
                            ms.Write(bytes, 0, (int)file.Length);

                            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                            httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
                            httpResponseMessage.Content.Headers.Add("x-filename", fileName);
                            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                            httpResponseMessage.Content.Headers.ContentDisposition.FileName = fileName;
                            httpResponseMessage.StatusCode = HttpStatusCode.OK;
                            return httpResponseMessage;
                        }
                    }
                }
                return this.Request.CreateResponse(HttpStatusCode.NotFound, "File not found.");
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }

}
