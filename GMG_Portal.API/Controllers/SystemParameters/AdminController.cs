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
using GMG_Portal.API.Models.SystemParameters.Admin;
using Heloper;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Admin")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var adminLogic = new AdminLogic();
                var obj = adminLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Admin>>(obj));

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
                var AdminLogic = new AdminLogic();
                var obj = AdminLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Admin>>(obj));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Admin postedAdmins)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var adminLogic = new AdminLogic();

                    Systemparameters_Admin obj = null;
                    if (postedAdmins.Id.Equals(0))
                    {
                        obj = adminLogic.Insert(Mapper.Map<Systemparameters_Admin>(postedAdmins));
                    }
                    else
                    {
                        if (postedAdmins.IsDeleted)
                        {
                            obj = adminLogic.Delete(Mapper.Map<Systemparameters_Admin>(postedAdmins));
                        }
                        else
                        {
                            obj = adminLogic.Edit(Mapper.Map<Systemparameters_Admin>(postedAdmins));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Admin>(obj));

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
        public HttpResponseMessage Login(Admin postedAdmins)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var adminLogic = new AdminLogic();
                    Systemparameters_Admin obj = null;

                    obj = adminLogic.Login(Mapper.Map<Systemparameters_Admin>(postedAdmins));


                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Admin>(obj));

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
