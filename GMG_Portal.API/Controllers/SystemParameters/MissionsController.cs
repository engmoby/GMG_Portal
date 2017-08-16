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
using Heloper;
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
                var missionLogic = new MissionsLogic();
                var missionLogicTranslate = new MissionLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = missionLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Mission>(obj));
                }
                else

                {
                    var objByLang = missionLogicTranslate.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Mission>(objByLang));
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllWithDeleted(string langId)
        {
            try
            {
                var missionLogic = new MissionsLogic();
                var missionLogicTranslate = new MissionLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = missionLogic.GetAllWithDeleted(); 
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Mission>(obj));

                }
                else

                {
                    var objByLang = missionLogicTranslate.GetAllWithDeleted(langId); 
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Mission>(objByLang));

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(Mission postedMissions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var missionLogic = new MissionsLogic();
                    var missionLogicTranslate = new MissionLogicTranslate();

                    Front_Mission obj = null;
                    Front_Mission_Translate objByLang = null;
                    if (postedMissions.Id.Equals(0))
                    {
                        if (postedMissions.langId == Parameters.DefaultLang)
                            obj = missionLogic.Insert(Mapper.Map<Front_Mission>(postedMissions));
                        else
                            objByLang = missionLogicTranslate.Insert(Mapper.Map<Front_Mission_Translate>(postedMissions));
                    }
                    else
                    {
                        if (postedMissions.IsDeleted)
                        {
                            if (postedMissions.langId == Parameters.DefaultLang)
                                obj = missionLogic.Delete(Mapper.Map<Front_Mission>(postedMissions));
                            else
                                objByLang = missionLogicTranslate.Delete(Mapper.Map<Front_Mission_Translate>(postedMissions));
                        }
                        else
                        {
                            if (postedMissions.langId == Parameters.DefaultLang)
                                obj = missionLogic.Edit(Mapper.Map<Front_Mission>(postedMissions));
                            else
                                objByLang = missionLogicTranslate.Edit(Mapper.Map<Front_Mission_Translate>(postedMissions));
                        }
                    }
                    if (postedMissions.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<CoreValues>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Front_Mission_Translate>(objByLang));

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
