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
    [RoutePrefix("SystemParameters/Owners")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OwnersController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            { 
                var ownerLogic = new OwnerLogic();
                var owners = ownerLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Owners>>(owners));
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
                var ownerLogic = new OwnerLogic();
                var owners = ownerLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Owners>>(owners));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Owners postedOwners)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ownerLogic = new OwnerLogic();
                    SystemParameters_Owners owner = null;
                    if (postedOwners.Id.Equals(0))
                    {
                        owner = ownerLogic.Insert(Mapper.Map<SystemParameters_Owners>(postedOwners));
                    }
                    else
                    {
                        if (postedOwners.IsDeleted)
                        {
                            owner = ownerLogic.Delete(Mapper.Map<SystemParameters_Owners>(postedOwners));
                        }
                        else
                        {
                            owner = ownerLogic.Edit(Mapper.Map<SystemParameters_Owners>(postedOwners));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Owners>(owner));
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
