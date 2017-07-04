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
    [RoutePrefix("SystemParameters/HomeSliders")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeSlidersController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var homeSlidersLogic = new HomeSlidersLogic();
                var homeSliders = homeSlidersLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HomeSlider>>(homeSliders));
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
                var homeSlidersLogic = new HomeSlidersLogic();
                var homeSliders = homeSlidersLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.HomeSlider>>(homeSliders));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(HomeSlider postedHomeSliders)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var homeSlidersLogic = new HomeSlidersLogic();
                    SystemParamater_HomeSlider language = null;
                    if (postedHomeSliders.Id.Equals(0))
                    {
                        language = homeSlidersLogic.Insert(Mapper.Map<SystemParamater_HomeSlider>(postedHomeSliders));
                    }
                    else
                    {
                        if (postedHomeSliders.IsDeleted)
                        {
                            language = homeSlidersLogic.Delete(Mapper.Map<SystemParamater_HomeSlider>(postedHomeSliders));
                        }
                        else
                        {
                            language = homeSlidersLogic.Edit(Mapper.Map<SystemParamater_HomeSlider>(postedHomeSliders));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<HomeSlider>(language));
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
