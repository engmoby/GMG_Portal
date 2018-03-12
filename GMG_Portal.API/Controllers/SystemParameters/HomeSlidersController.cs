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
    [RoutePrefix("SystemParameters/HomeSliders")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeSlidersController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var homeSlidersLogic = new HomeSlidersLogic();
                //var homeSlidersLogicTranslate = new HomeSliderLogicTranslate();
                //if (langId == Parameters.DefaultLang)
                //{
                    var obj = homeSlidersLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HomeSliderModel>>(obj));
                //}
                //else

                //{
                //    var objByLang = homeSlidersLogicTranslate.GetAll(langId);
                //    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_HomeSlider_Translate>>(objByLang));
                //}
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
                var homeSlidersLogic = new HomeSlidersLogic();
                //var homeSlidersLogicTranslate = new HomeSliderLogicTranslate();

                //if (langId == Parameters.DefaultLang)
                //{
                    var obj = homeSlidersLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HomeSliderModel>>(obj));

                //}
                //else

                //{
                //    var objByLang = homeSlidersLogicTranslate.GetAllWithDeleted(langId);

                //    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_HomeSlider_Translate>>(objByLang));

                //}

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
         

        [HttpPost]
        public HttpResponseMessage Save(HomeSliderModel postedHomeSliders)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var homeSlidersLogic = new HomeSlidersLogic();
                //    var homeSlidersLogicTranslate = new HomeSliderLogicTranslate();

                    HomeSlider obj = null;
                    SystemParameters_HomeSlider_Translate objByLang = null;
                    if (postedHomeSliders.Id.Equals(0))
                    {
                      //  if (postedHomeSliders.langId == Parameters.DefaultLang)
                            obj = homeSlidersLogic.Insert(Mapper.Map<HomeSlider>(postedHomeSliders));
                        //else
                        //    objByLang = homeSlidersLogicTranslate.Insert(Mapper.Map<SystemParameters_HomeSlider_Translate>(postedHomeSliders));
                    }
                    else
                    {
                        if (postedHomeSliders.IsDeleted)
                        {
                          //  if (postedHomeSliders.langId == Parameters.DefaultLang)
                                obj = homeSlidersLogic.Delete(Mapper.Map<HomeSlider>(postedHomeSliders));
                          // / else
                              //  objByLang = homeSlidersLogicTranslate.Delete(Mapper.Map<SystemParameters_HomeSlider_Translate>(postedHomeSliders));
                        }
                        else
                        {
                          //  if (postedHomeSliders.langId == Parameters.DefaultLang)
                                obj = homeSlidersLogic.Edit(Mapper.Map<HomeSlider>(postedHomeSliders));
                          //  else
                              //  objByLang = homeSlidersLogicTranslate.Edit(Mapper.Map<SystemParameters_HomeSlider_Translate>(postedHomeSliders));
                        }
                    }
                   // if (postedHomeSliders.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<HomeSliderModel>(obj));
                  //  else
                      //  return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_HomeSlider_Translate>(objByLang));

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
