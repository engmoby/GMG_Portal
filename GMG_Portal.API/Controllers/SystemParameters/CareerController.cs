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
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Career")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CareerController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                 
                var careerLogic = new CareerLogic();
                var career = careerLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Career>>(career));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetCareerDetails(int id)
        {
            try
            {

                var careerLogic = new CareerLogic();
                var career = careerLogic.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Career>(career));
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
                var CareerLogic = new CareerLogic();
                var Careers = CareerLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.Career>>(Careers));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Career postedCareers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CareerLogic = new CareerLogic();
                    SystemParameters_Careers Career = null;
                    if (postedCareers.Id.Equals(0))
                    {
                        Career = CareerLogic.Insert(Mapper.Map<SystemParameters_Careers>(postedCareers));
                    }
                    else
                    {
                        if (postedCareers.IsDeleted)
                        {
                            Career = CareerLogic.Delete(Mapper.Map<SystemParameters_Careers>(postedCareers));
                        }
                        else
                        {
                            Career = CareerLogic.Edit(Mapper.Map<SystemParameters_Careers>(postedCareers));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Career>(Career));
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
