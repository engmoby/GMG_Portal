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
    [RoutePrefix("SystemParameters/ContactForm")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactFormController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var contactFormLogic = new ContactFormLogic();
                var contactForm = contactFormLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ContactForm>>(contactForm));
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
                var contactFormLogic = new ContactFormLogic();
                var contactForms = contactFormLogic.GetAllWithSeen();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ContactForm>>(contactForms));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(ContactForm postedContactForms)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contactFormLogic = new ContactFormLogic();
                    SystemParameters_ContactForm ContactForm = null;
                    if (postedContactForms.Id.Equals(0))
                    {
                        ContactForm = contactFormLogic.Insert(Mapper.Map<SystemParameters_ContactForm>(postedContactForms));
                    }
                    else
                    {
                        ContactForm = contactFormLogic.Edit(Mapper.Map<SystemParameters_ContactForm>(postedContactForms));
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ContactForm>(ContactForm));
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
