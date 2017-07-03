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
    [RoutePrefix("SystemParameters/Languages")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LanguagesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var languagesLogic = new LanguagesLogic();
                var languages = languagesLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Languages>>(languages));
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
                var languagesLogic = new LanguagesLogic();
                var languages = languagesLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.Languages>>(languages));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Languages postedLanguages)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var languagesLogic = new LanguagesLogic();
                    Systemparameters_Languages language = null;
                    if (postedLanguages.Id.Equals(0))
                    {
                        language = languagesLogic.Insert(Mapper.Map<Systemparameters_Languages>(postedLanguages));
                    }
                    else
                    {
                        if (postedLanguages.IsDeleted)
                        {
                            language = languagesLogic.Delete(Mapper.Map<Systemparameters_Languages>(postedLanguages));
                        }
                        else
                        {
                            language = languagesLogic.Edit(Mapper.Map<Systemparameters_Languages>(postedLanguages));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Languages>(language));
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
