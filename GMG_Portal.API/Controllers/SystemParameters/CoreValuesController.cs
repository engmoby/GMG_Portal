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
    [RoutePrefix("SystemParameters/CoreValues")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CoreValuesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var coreValuesLogic = new CoreValuesLogic();
                var coreValues = coreValuesLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CoreValues>>(coreValues));
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
                var coreValuesLogic = new CoreValuesLogic();
                var coreValues = coreValuesLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CoreValues>>(coreValues));
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
                    SystemParameters_CoreValues coreValue = null;
                    if (posteCoreValues.Id.Equals(0))
                    {
                        coreValue = coreValuesLogic.Insert(Mapper.Map<SystemParameters_CoreValues>(posteCoreValues));
                    }
                    else
                    {
                        if (posteCoreValues.IsDeleted)
                        {
                            coreValue = coreValuesLogic.Delete(Mapper.Map<SystemParameters_CoreValues>(posteCoreValues));
                        }
                        else
                        {
                            coreValue = coreValuesLogic.Edit(Mapper.Map<SystemParameters_CoreValues>(posteCoreValues));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<CoreValues>(coreValue));
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
