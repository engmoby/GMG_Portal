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
    [RoutePrefix("SystemParameters/About")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AboutController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var AboutLogic = new AboutLogic();
                var AboutLogicTranslate = new AboutLogicTranslate();
               
             

                if (langId == Parameters.DefaultLang)
                {
                    var Obj = AboutLogic.GetAll(); 
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<About>>(Obj)); 
                }
                else

                {
                    var ObjByLang = AboutLogicTranslate.GetAll(langId); 
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<About>>(ObjByLang)); 
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
                var AboutLogic = new AboutLogic();
                var AboutLogicTranslate = new AboutLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var Obj = AboutLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<About>>(Obj));

                }
                else

                {
                    var ObjByLang = AboutLogicTranslate.GetAllWithDeleted(langId);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<About>>(ObjByLang));

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(About postedAbouts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var aboutLogic = new AboutLogic();
                    var aboutLogicTranslate = new AboutLogicTranslate();

                    SystemParameters_About obj = null;
                    SystemParameters_About_Translate objByLang = null;

                    if (postedAbouts.Id.Equals(0))
                    {
                        if (postedAbouts.LangId == Parameters.DefaultLang)
                            obj = aboutLogic.Insert(Mapper.Map<SystemParameters_About>(postedAbouts));
                        else
                            objByLang =  aboutLogicTranslate.Insert(Mapper.Map<SystemParameters_About_Translate>(postedAbouts));

                    }
                    else
                    {
                        if (postedAbouts.IsDeleted)
                        {
                            if (postedAbouts.LangId == Parameters.DefaultLang)
                                obj = aboutLogic.Delete(Mapper.Map<SystemParameters_About>(postedAbouts));
                            else
                                objByLang = aboutLogicTranslate.Delete(Mapper.Map<SystemParameters_About_Translate>(postedAbouts));
                        }
                        else
                        {
                            if (postedAbouts.LangId == Parameters.DefaultLang)
                                obj = aboutLogic.Edit(Mapper.Map<SystemParameters_About>(postedAbouts));
                            else
                                objByLang = aboutLogicTranslate.Edit(Mapper.Map<SystemParameters_About_Translate>(postedAbouts));
                        }
                    }
                    if (postedAbouts.LangId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<About>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_About_Translate>(objByLang));
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
        [HttpGet]
        public HttpResponseMessage Aboutall(string langId)
        {
            try
            {
                var retunAboutAll = new AboutAll();
                var aboutLogic = new AboutLogic();
                var about = aboutLogic.GetAll();
                retunAboutAll.AboutTitle = about.DisplayValue;
                retunAboutAll.AboutDesc = about.DisplayValueDesc;
                retunAboutAll.AboutVideoUrl = about.Url;

                var visionLogic = new VisionsLogic();
                var vision = visionLogic.GetAll();
                retunAboutAll.VisionTitle = vision[0].DisplayValue;
                retunAboutAll.VisionDesc = vision[0].DisplayValueDesc;


                //var missionLogic = new MissionsLogic();
                //var mission = missionLogic.GetAll(Parameters.DefaultLang);
                //retunAboutAll.MissionTitle = mission[0].DisplayValue;
                //retunAboutAll.MissionDesc = mission[0].DisplayValueDesc;

                var returnCoreValues = new List<CoreValues>();
                var coreValueLogic = new CoreValuesLogic();
                var values = coreValueLogic.GetAll();
                foreach (var valueObj in values)
                {
                    returnCoreValues.Add(new CoreValues
                    {
                         DisplayValue = valueObj.DisplayValue,
                         DisplayValueDesc = valueObj.DisplayValueDesc
                    });
                }
                retunAboutAll.CoreValueses = returnCoreValues;
                return Request.CreateResponse(HttpStatusCode.OK, retunAboutAll);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

    }

}
