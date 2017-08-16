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

                //General Declerations
                var retunAboutAll = new AboutAll();
                var aboutLogic = new AboutLogic();
                var aboutLogicTranslate = new AboutLogicTranslate();



                // Get About By Lang 
                if (langId == Parameters.DefaultLang)
                {
                    var obj = aboutLogic.GetAll();
                    retunAboutAll.AboutTitle = obj.DisplayValue;
                    retunAboutAll.AboutDesc = obj.DisplayValueDesc;
                    retunAboutAll.AboutVideoUrl = obj.Url;
                }
                else
                {
                    var objtranslate = aboutLogicTranslate.GetAll(langId);
                    retunAboutAll.AboutTitle = objtranslate.DisplayValue;
                    retunAboutAll.AboutDesc = objtranslate.DisplayValueDesc;
                    retunAboutAll.AboutVideoUrl = objtranslate.Url;
                }


                // Get Vision By Lang 
                var visionLogic = new VisionsLogic();
                var visionLogicTranslate = new VisionLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = visionLogic.GetAll();
                    retunAboutAll.VisionTitle = obj[0].DisplayValue;
                    retunAboutAll.VisionDesc = obj[0].DisplayValueDesc;
                }
                else
                {
                    var objtranslate = visionLogicTranslate.GetAll(langId);
                    retunAboutAll.VisionTitle = objtranslate.DisplayValue;
                    retunAboutAll.VisionDesc = objtranslate.DisplayValueDesc;
                }


                // Get Mission By Lang 
                var missionLogic = new MissionsLogic();
                var missionLogicTranslate = new MissionLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = missionLogic.GetAll();
                    retunAboutAll.MissionTitle = obj[0].DisplayValue;
                    retunAboutAll.MissionDesc = obj[0].DisplayValueDesc;
                }
                else
                {
                    var objtranslate = missionLogicTranslate.GetAll(langId);
                    retunAboutAll.MissionTitle = objtranslate.DisplayValue;
                    retunAboutAll.MissionDesc = objtranslate.DisplayValueDesc;

                }




                //Core Values By Lang
                var coreValueLogic = new CoreValuesLogic();
                var coreValueLogicTranslate = new CoreValuesLogicTranslate();


                if (langId == Parameters.DefaultLang)
                {
                    var returnCoreValues = new List<CoreValues>();
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
                }
                else
                {
                    var returnCoreValues = new List<SystemParameters_CoreValues_Translate>();
                    var values = coreValueLogicTranslate.GetAll(langId);
                    foreach (var valueObj in values)
                    {
                        returnCoreValues.Add(new SystemParameters_CoreValues_Translate
                        {
                            DisplayValue = valueObj.DisplayValue,
                            DisplayValueDesc = valueObj.DisplayValueDesc
                        });
                    }
                    retunAboutAll.CoreValueses = Mapper.Map<List<CoreValues>>(returnCoreValues);
                }

               //Finally Return the Object 
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
