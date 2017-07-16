using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Data;
using GMG_Portal.Business.Logic.SystemParameters;
using AutoMapper;
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
    }

}
