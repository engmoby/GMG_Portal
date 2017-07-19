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
    [RoutePrefix("SystemParameters/About")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AboutController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {

                var aboutLogic = new AboutLogic();
                var about = aboutLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<About>>(about));
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
                var AboutLogic = new AboutLogic();
                var Abouts = AboutLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.About>>(Abouts));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
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
                    SystemParameters_About about = null;
                    if (postedAbouts.Id.Equals(0))
                    {
                        about = aboutLogic.Insert(Mapper.Map<SystemParameters_About>(postedAbouts));
                    }
                    else
                    {
                        if (postedAbouts.IsDeleted)
                        {
                            about = aboutLogic.Delete(Mapper.Map<SystemParameters_About>(postedAbouts));
                        }
                        else
                        {
                            about = aboutLogic.Edit(Mapper.Map<SystemParameters_About>(postedAbouts));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<About>(about));
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
        public HttpResponseMessage Aboutall()
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


                var missionLogic = new MissionsLogic();
                var mission = missionLogic.GetAll();
                retunAboutAll.MissionTitle = mission[0].DisplayValue;
                retunAboutAll.MissionDesc = mission[0].DisplayValueDesc;

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
