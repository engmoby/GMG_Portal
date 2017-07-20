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
    [RoutePrefix("SystemParameters/NewsCategory")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NewsCategoryController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var categoryLogic = new CategoryLogic();
                var Category = categoryLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Category>>(Category));
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
                var categoryLogic = new CategoryLogic();
                var category = categoryLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Category>>(category));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Category postedCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CategoryLogic = new CategoryLogic();
                    SystemParameters_Category language = null;
                    if (postedCategory.Id.Equals(0))
                    {
                        language = CategoryLogic.Insert(Mapper.Map<SystemParameters_Category>(postedCategory));
                    }
                    else
                    {
                        if (postedCategory.IsDeleted)
                        {
                            language = CategoryLogic.Delete(Mapper.Map<SystemParameters_Category>(postedCategory));
                        }
                        else
                        {
                            language = CategoryLogic.Edit(Mapper.Map<SystemParameters_Category>(postedCategory));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Category>(language));
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
