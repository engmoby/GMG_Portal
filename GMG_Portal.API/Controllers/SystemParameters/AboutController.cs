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
                var aboutLogic = new AboutLogic();
                var obj = aboutLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<AboutModel>(obj));
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
                var aboutLogic = new AboutLogic();
                var obj = aboutLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<AboutModel>>(obj));
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
                    About obj = null;

                    if (postedAbouts.Id.Equals(0))
                    {
                        obj = aboutLogic.Insert(Mapper.Map<About>(postedAbouts));
                    }
                    else
                    {
                        if (postedAbouts.IsDeleted)
                        {
                            obj = aboutLogic.Delete(Mapper.Map<About>(postedAbouts));
                        }
                        else
                        {
                            obj = aboutLogic.Edit(Mapper.Map<About>(postedAbouts));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<AboutModel>(obj));
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

                //var obj = aboutLogic.GetAll();
                //retunAboutAll.AboutTitle = obj.DisplayValue;
                //retunAboutAll.AboutDesc = obj.DisplayValueDesc;
                //retunAboutAll.AboutVideoUrl = obj.Url;

                //// Get Vision By Lang 
                //var visionLogic = new VisionsLogic();
                //var objvVision = visionLogic.GetAll();
                //retunAboutAll.VisionTitle = objvVision.DisplayValue;
                //retunAboutAll.VisionDesc = objvVision.DisplayValueDesc;

                //// Get Mission By Lang 
                //var missionLogic = new MissionsLogic();
                //var objmMission = missionLogic.GetAll();
                //retunAboutAll.MissionTitle = objmMission.DisplayValue;
                //retunAboutAll.MissionDesc = objmMission.DisplayValueDesc;




                ////Core Values By Lang
                //var coreValueLogic = new CoreValuesLogic();
                //var returnCoreValues = new List<CoreValues>();
                //var values = coreValueLogic.GetAll();
                //foreach (var valueObj in values)
                //{
                //    returnCoreValues.Add(new CoreValues
                //    {
                //        DisplayValue = valueObj.DisplayValue,
                //        DisplayValueDesc = valueObj.DisplayValueDesc
                //    });
                //}
                //retunAboutAll.CoreValueses = returnCoreValues;
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
