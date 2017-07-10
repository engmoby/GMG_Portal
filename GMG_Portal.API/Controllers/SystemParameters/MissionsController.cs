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
    [RoutePrefix("SystemParameters/Missions")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MissionsController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var missionsLogic = new MissionsLogic();
                var missions = missionsLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HomeSlider>>(missions));
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
                var missionsLogic = new MissionsLogic();
                var missions = missionsLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.HomeSlider>>(missions));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(HomeSlider postedMissions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var missionsLogic = new MissionsLogic();
                    Front_Mission mission = null;
                    if (postedMissions.Id.Equals(0))
                    {
                        mission = missionsLogic.Insert(Mapper.Map<Front_Mission>(postedMissions));
                    }
                    else
                    {
                        if (postedMissions.IsDeleted)
                        {
                            mission = missionsLogic.Delete(Mapper.Map<Front_Mission>(postedMissions));
                        }
                        else
                        {
                            mission = missionsLogic.Edit(Mapper.Map<Front_Mission>(postedMissions));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<HomeSlider>(mission));
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
