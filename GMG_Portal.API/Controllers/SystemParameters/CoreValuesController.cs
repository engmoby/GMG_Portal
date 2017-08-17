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
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using Heloper;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/CoreValues")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CoreValuesController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var coreValuesLogic = new CoreValuesLogic();
                var coreValuesLogicTranslate = new CoreValuesLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = coreValuesLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CoreValues>>(obj));
                }
                else

                {
                    var objByLang = coreValuesLogicTranslate.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_CoreValues_Translate>>(objByLang));
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
                var coreValuesLogic = new CoreValuesLogic();
                var coreValuesLogicTranslate = new CoreValuesLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = coreValuesLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CoreValues>>(obj));

                }
                else

                {
                    var objByLang = coreValuesLogicTranslate.GetAllWithDeleted(langId);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_CoreValues_Translate>>(objByLang));

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(CoreValues posteCoreValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var coreValuesLogic = new CoreValuesLogic();
                    var coreValuesLogicTranslate = new CoreValuesLogicTranslate();

                    SystemParameters_CoreValues obj = null;
                    SystemParameters_CoreValues_Translate objByLang = null;
                    if (posteCoreValues.Id.Equals(0))
                    {
                        if (posteCoreValues.langId == Parameters.DefaultLang)
                            obj = coreValuesLogic.Insert(Mapper.Map<SystemParameters_CoreValues>(posteCoreValues));
                        else
                            objByLang = coreValuesLogicTranslate.Insert(Mapper.Map<SystemParameters_CoreValues_Translate>(posteCoreValues));
                    }
                    else
                    {
                        if (posteCoreValues.IsDeleted)
                        {
                            if (posteCoreValues.langId == Parameters.DefaultLang)
                                obj = coreValuesLogic.Delete(Mapper.Map<SystemParameters_CoreValues>(posteCoreValues));
                            else
                                objByLang = coreValuesLogicTranslate.Delete(Mapper.Map<SystemParameters_CoreValues_Translate>(posteCoreValues));
                        }
                        else
                        {
                            if (posteCoreValues.langId == Parameters.DefaultLang)
                                obj = coreValuesLogic.Edit(Mapper.Map<SystemParameters_CoreValues>(posteCoreValues));
                            else
                                objByLang = coreValuesLogicTranslate.Edit(Mapper.Map<SystemParameters_CoreValues_Translate>(posteCoreValues));
                        }
                    }
                    if (posteCoreValues.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<CoreValues>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_CoreValues_Translate>(objByLang));

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
