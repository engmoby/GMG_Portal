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
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var missionsLogic = new MissionsLogic();
                var missions = missionsLogic.GetAll(langId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Mission>>(missions));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetAllWithDeleted(string langId)
        {
            try
            {
                var missionsLogic = new MissionsLogic();
                var missions = missionsLogic.GetAllWithDeleted(langId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Mission>>(missions));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Mission postedMissions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var missionsLogic = new MissionsLogic();
                    Front_Mission mission = null;
                    mission = missionsLogic.Edit(Mapper.Map<Front_Mission>(postedMissions), postedMissions.Lang_Id);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Mission>(mission));
                }
                goto ThrowBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }

            ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }

}
