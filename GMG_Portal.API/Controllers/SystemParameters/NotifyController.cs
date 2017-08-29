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
    [RoutePrefix("SystemParameters/NotifyController")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NotifyControllerController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var notifyLogic = new NotifyLogic();
                var obj = notifyLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<NotifyViewModel>>(obj));

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllWithDeleted()
        {
            try
            {
                var notifyLogic = new NotifyLogic();
                var obj = notifyLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<NotifyViewModel>>(obj));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(NotifyViewModel postedNotifys)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var notifyLogic = new NotifyLogic();

                    SystemParameters_Notify obj = null;
                    if (postedNotifys.Id.Equals(0))
                    {
                        obj = notifyLogic.Insert(Mapper.Map<SystemParameters_Notify>(postedNotifys));
                    }
                    else
                    {
                        if (postedNotifys.IsDeleted)
                        {
                            obj = notifyLogic.Delete(Mapper.Map<SystemParameters_Notify>(postedNotifys));
                        }
                        else
                        { 
                            obj = notifyLogic.Edit(Mapper.Map<SystemParameters_Notify>(postedNotifys));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<NotifyViewModel>(obj));

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
