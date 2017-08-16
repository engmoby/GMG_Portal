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
    [RoutePrefix("SystemParameters/Visions")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VisionsController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var visionLogic = new VisionsLogic();
                var visionLogicTranslate = new VisionLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = visionLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Vision>(obj));
                }
                else

                {
                    var objByLang = visionLogicTranslate.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Vision>(objByLang));
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
                var visionLogic = new VisionsLogic();
                var visionLogicTranslate = new VisionLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = visionLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Vision>(obj));

                }
                else

                {
                    var objByLang = visionLogicTranslate.GetAllWithDeleted(langId);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Vision>(objByLang));

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(Vision postedVision)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var visionLogic = new VisionsLogic();
                    var visionLogicTranslate = new VisionLogicTranslate();

                    Front_Vision obj = null;
                    Front_Vision_Translate objByLang = null;
                    if (postedVision.Id.Equals(0))
                    {
                        if (postedVision.langId == Parameters.DefaultLang)
                            obj = visionLogic.Insert(Mapper.Map<Front_Vision>(postedVision));
                        else
                            objByLang = visionLogicTranslate.Insert(Mapper.Map<Front_Vision_Translate>(postedVision));
                    }
                    else
                    {
                        if (postedVision.IsDeleted)
                        {
                            if (postedVision.langId == Parameters.DefaultLang)
                                obj = visionLogic.Delete(Mapper.Map<Front_Vision>(postedVision));
                            else
                                objByLang = visionLogicTranslate.Delete(Mapper.Map<Front_Vision_Translate>(postedVision));
                        }
                        else
                        {
                            if (postedVision.langId == Parameters.DefaultLang)
                                obj = visionLogic.Edit(Mapper.Map<Front_Vision>(postedVision));
                            else
                                objByLang = visionLogicTranslate.Edit(Mapper.Map<Front_Vision_Translate>(postedVision));
                        }
                    }
                    if (postedVision.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<CoreValues>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Front_Vision_Translate>(objByLang));

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
 
