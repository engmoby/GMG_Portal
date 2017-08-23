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
                var categoryLogicTranslate = new CategoryLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = categoryLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Category>>(obj));
                }
                else

                {
                    var objByLang = categoryLogicTranslate.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_Category_Translate>>(objByLang));
                }
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
                var categoryLogicTranslate = new CategoryLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = categoryLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Category>>(obj));

                }
                else

                {
                    var objByLang = categoryLogicTranslate.GetAllWithDeleted(langId);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_Category_Translate>>(objByLang));

                }

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
                    var categoryLogicTranslate = new CategoryLogicTranslate();

                    SystemParameters_Category obj = null;
                    SystemParameters_Category_Translate objByLang = null;
                    if (postedCategory.Id.Equals(0))
                    {
                        if (postedCategory.langId == Parameters.DefaultLang)
                            obj = categoryLogic.Insert(Mapper.Map<SystemParameters_Category>(postedCategory));
                        else
                            objByLang = categoryLogicTranslate.Insert(Mapper.Map<SystemParameters_Category_Translate>(postedCategory));
                    }
                    else
                    {
                        if (postedCategory.IsDeleted)
                        {
                            if (postedCategory.langId == Parameters.DefaultLang)
                                obj = categoryLogic.Delete(Mapper.Map<SystemParameters_Category>(postedCategory));
                            else
                                objByLang = categoryLogicTranslate.Delete(Mapper.Map<SystemParameters_Category_Translate>(postedCategory));
                        }
                        else
                        {
                            if (postedCategory.langId == Parameters.DefaultLang)
                                obj = categoryLogic.Edit(Mapper.Map<SystemParameters_Category>(postedCategory));
                            else
                                objByLang = categoryLogicTranslate.Edit(Mapper.Map<SystemParameters_Category_Translate>(postedCategory));
                        }
                    }
                    if (postedCategory.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Category>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_Category_Translate>(objByLang));

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
