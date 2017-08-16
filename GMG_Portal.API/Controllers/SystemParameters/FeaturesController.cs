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
    [RoutePrefix("SystemParameters/Features")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FeaturesController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var featuresLogic = new FeaturesLogic();
                var featuresLogicTranslate = new FeaturesLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = featuresLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Features>>(obj));
                }
                else

                {
                    var objByLang = featuresLogicTranslate.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Features>>(objByLang));
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
                var featuresLogic = new FeaturesLogic();
                var featuresLogicTranslate = new FeaturesLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = featuresLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Features>>(obj));

                }
                else

                {
                    var objByLang = featuresLogicTranslate.GetAllWithDeleted(langId);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Features>>(objByLang));

                }

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
                    var featuresLogicTranslate = new FeaturesLogicTranslate();

                    SystemParameters_Features obj = null;
                    SystemParameters_Features_Translate objByLang = null;
                    if (postedFeatures.Id.Equals(0))
                    {
                        if (postedFeatures.langId == Parameters.DefaultLang)
                            obj = featuresLogic.Insert(Mapper.Map<SystemParameters_Features>(postedFeatures));
                        else
                            objByLang = featuresLogicTranslate.Insert(Mapper.Map<SystemParameters_Features_Translate>(postedFeatures));
                    }
                    else
                    {
                        if (postedFeatures.IsDeleted)
                        {
                            if (postedFeatures.langId == Parameters.DefaultLang)
                                obj = featuresLogic.Delete(Mapper.Map<SystemParameters_Features>(postedFeatures));
                            else
                                objByLang = featuresLogicTranslate.Delete(Mapper.Map<SystemParameters_Features_Translate>(postedFeatures));
                        }
                        else
                        {
                            if (postedFeatures.langId == Parameters.DefaultLang)
                                obj = featuresLogic.Edit(Mapper.Map<SystemParameters_Features>(postedFeatures));
                            else
                                objByLang = featuresLogicTranslate.Edit(Mapper.Map<SystemParameters_Features_Translate>(postedFeatures));
                        }
                    }
                    if (postedFeatures.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_Features>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_Features_Translate>(objByLang));

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
