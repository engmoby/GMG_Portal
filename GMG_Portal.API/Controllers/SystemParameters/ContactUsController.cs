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
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/ContactUs")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactUsController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                 
                var contactUsLogic = new ContactUsLogic();
                var contactUs = contactUsLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ContactUs>>(contactUs));
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
                var contactUsLogic = new ContactUsLogic();
                var contactUss = contactUsLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ContactUs>>(contactUss));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(ContactUs postedContactUss)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contactUsLogic = new ContactUsLogic();
                    SystemParameters_ContactUs contactUs = null;
                    if (postedContactUss.Id.Equals(0))
                    {
                        contactUs = contactUsLogic.Insert(Mapper.Map<SystemParameters_ContactUs>(postedContactUss));
                    }
                    else
                    {
                        if (postedContactUss.IsDeleted)
                        {
                            contactUs = contactUsLogic.Delete(Mapper.Map<SystemParameters_ContactUs>(postedContactUss));
                        }
                        else
                        {
                            contactUs = contactUsLogic.Edit(Mapper.Map<SystemParameters_ContactUs>(postedContactUss));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ContactUs>(contactUs));
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
