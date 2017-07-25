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
    [RoutePrefix("SystemParameters/Features")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FeaturesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var featuresLogic = new FeaturesLogic();
                var features = featuresLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Features>>(features));
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
                var FeaturesLogic = new FeaturesLogic();
                var Features = FeaturesLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.Features>>(Features));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Features postedFeatures)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var featuresLogic = new FeaturesLogic();
                    SystemParameters_Features Feature = null;
                    if (postedFeatures.Id.Equals(0))
                    {
                        Feature = featuresLogic.Insert(Mapper.Map<SystemParameters_Features>(postedFeatures));
                    }
                    else
                    {
                        if (postedFeatures.IsDeleted)
                        {
                            Feature = featuresLogic.Delete(Mapper.Map<SystemParameters_Features>(postedFeatures));
                        }
                        else
                        {
                            Feature = featuresLogic.Edit(Mapper.Map<SystemParameters_Features>(postedFeatures));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Features>(Feature));
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
