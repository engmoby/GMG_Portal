using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMG_Portal.Business.Logic.SystemParameters;
using AutoMapper;
using Helpers;
using GMG_Portal.API.Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Departments")]
    [CorsFilter]
    public class DepartmentsController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var departmentsLogic = new DepartmentLogic();
                var departments = departmentsLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.Departments>>(departments));
            }

            catch(Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
        public HttpResponseMessage GetAllWithDeleted()
        {
            try
            {
                var departmentsLogic = new DepartmentLogic();
                var departments = departmentsLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.Departments>>(departments));
            }

            catch(Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        public HttpResponseMessage Save(GMG_Portal.API.Models.SystemParameters.Departments PostedDepartment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var DepartmentsLogic = new DepartmentLogic();
                    Data.Departments Department = null;
                    if (PostedDepartment.ID.Equals(0))
                    {
                        Department = DepartmentsLogic.Insert(Mapper.Map<GMG_Portal.Data.Departments>(PostedDepartment));
                    }
                    else
                    {
                        if (PostedDepartment.IsDeleted)
                        {
                            Department = DepartmentsLogic.Delete(Mapper.Map<GMG_Portal.Data.Departments>(PostedDepartment));
                        }
                        else
                        {
                            Department = DepartmentsLogic.Edit(Mapper.Map<GMG_Portal.Data.Departments>(PostedDepartment));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<GMG_Portal.API.Models.SystemParameters.Departments>(Department));
                }
                goto ThrowBadRequest;

            }

            catch (Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

    }
}
