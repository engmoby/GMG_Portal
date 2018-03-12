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
using Heloper;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/ContactUs")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactUsController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var contactUsLogic = new ContactUsLogic();
                var obj = contactUsLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ContactUsModel>(obj));
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
                var contactUsLogic = new ContactUsLogic();
                var obj = contactUsLogic.GetAllWithDeleted();

                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ContactUsModel>>(obj));

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(ContactUsModel postedContactUs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contactUsLogic = new ContactUsLogic();
                    ContactU obj = null;
                    obj = contactUsLogic.Edit(Mapper.Map<ContactU>(postedContactUs));
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ContactUsModel>(obj));

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
