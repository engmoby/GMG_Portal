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
    [RoutePrefix("SystemParameters/Abouts")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AboutsController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var AboutLogic = new AboutLogic();
                var Abouts = AboutLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<About>>(Abouts));
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
                var AboutLogic = new AboutLogic();
                var Abouts = AboutLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.About>>(Abouts));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(About postedAbouts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var AboutLogic = new AboutLogic();
                    SystemParameter_About language = null;
                    if (postedAbouts.Id.Equals(0))
                    {
                        language = AboutLogic.Insert(Mapper.Map<SystemParameter_About>(postedAbouts));
                    }
                    else
                    {
                        if (postedAbouts.IsDeleted)
                        {
                            language = AboutLogic.Delete(Mapper.Map<SystemParameter_About>(postedAbouts));
                        }
                        else
                        {
                            language = AboutLogic.Edit(Mapper.Map<SystemParameter_About>(postedAbouts));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<About>(language));
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
