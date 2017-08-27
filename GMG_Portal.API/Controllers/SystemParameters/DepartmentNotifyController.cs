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
    [RoutePrefix("SystemParameters/DepartmentNotifyController")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepartmentNotifyControllerController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var departmentLogic = new DepartmentLogic();
                var obj = departmentLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Department>>(obj));

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllNotify(string department)
        {
            try
            {
                var departmentLogic = new DepartmentLogic();
                var notifyLogic = new NotifyLogic();
                var obj = departmentLogic.GetDepartmentByName(department);
                var objList = notifyLogic.GetNotifyByDepId(obj.Id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_Notify>>(objList));

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
                var departmentLogic = new DepartmentLogic();
                var obj = departmentLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Department>>(obj));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Department postedDepartments)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var departmentLogic = new DepartmentLogic();

                    SystemParameters_NotifyDepartment obj = null;
                    if (postedDepartments.Id.Equals(0))
                    {
                        obj = departmentLogic.Insert(Mapper.Map<SystemParameters_NotifyDepartment>(postedDepartments));
                    }
                    else
                    {
                        if (postedDepartments.IsDeleted)
                        {
                            obj = departmentLogic.Delete(Mapper.Map<SystemParameters_NotifyDepartment>(postedDepartments));
                        }
                        else
                        {
                            obj = departmentLogic.Edit(Mapper.Map<SystemParameters_NotifyDepartment>(postedDepartments));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Department>(obj));

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
