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
    [RoutePrefix("SystemParameters/Visions")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VisionsController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var visionsLogic = new VisionsLogic();
                var visions = visionsLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Vision>>(visions));
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
                var VisionsLogic = new VisionsLogic();
                var Visions = VisionsLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.Vision>>(Visions));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Vision postedVisions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var visionsLogic = new VisionsLogic();
                    Front_Vision vision = null;
                    if (postedVisions.Id.Equals(0))
                    {
                        vision = visionsLogic.Insert(Mapper.Map<Front_Vision>(postedVisions));
                    }
                    else
                    {
                        if (postedVisions.IsDeleted)
                        {
                            vision = visionsLogic.Delete(Mapper.Map<Front_Vision>(postedVisions));
                        }
                        else
                        {
                            vision = visionsLogic.Edit(Mapper.Map<Front_Vision>(postedVisions));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Vision>(vision));
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
