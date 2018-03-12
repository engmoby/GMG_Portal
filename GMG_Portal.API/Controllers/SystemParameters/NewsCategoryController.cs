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
    [RoutePrefix("SystemParameters/NewsCategory")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NewsCategoryController : ApiController
    {


        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var categoryLogic = new CategoryLogic();
                var obj = categoryLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CategoryModel>>(obj));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllWithDeleted(string langId)
        {
            try
            {
                var categoryLogic = new CategoryLogic();
                var obj = categoryLogic.GetAllWithDeleted(); 
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CategoryModel>>(obj));
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
                    var categoryLogic = new CategoryLogic();

                    Category obj = null;
                    if (postedCategory.Id.Equals(0))
                    {
                        obj = categoryLogic.Insert(Mapper.Map<Category>(postedCategory));
                    }
                    else
                    {
                        if (postedCategory.IsDeleted)
                        {
                            obj = categoryLogic.Delete(Mapper.Map<Category>(postedCategory));
                        }
                        else
                        {
                            obj = categoryLogic.Edit(Mapper.Map<Category>(postedCategory));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<CategoryModel>(obj));

                }
                //goto ThrowBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            //    ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }

}
