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
                var contactUsLogicTranslate = new ContactUsLogicTranslate();



                if (langId == Parameters.DefaultLang)
                {
                    var obj = contactUsLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_ContactUs>(obj));
                }
                else

                {
                    var objByLang = contactUsLogicTranslate.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_ContactUs_Translate>(objByLang));
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
                var contactUsLogic = new ContactUsLogic();
                var contactUsLogicTranslate = new ContactUsLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = contactUsLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_ContactUs>>(obj));

                }
                else

                {
                    var objByLang = contactUsLogicTranslate.GetAllWithDeleted(langId);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<SystemParameters_ContactUs_Translate>>(objByLang));

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(ContactUs postedContactUs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contactUsLogic = new ContactUsLogic();
                    var contactUsLogicTranslate = new ContactUsLogicTranslate();

                    SystemParameters_ContactUs obj = null;
                    SystemParameters_ContactUs_Translate objByLang = null;

                    if (postedContactUs.langId == Parameters.DefaultLang)
                        obj = contactUsLogic.Edit(Mapper.Map<SystemParameters_ContactUs>(postedContactUs));
                    else
                        objByLang = contactUsLogicTranslate.Edit(Mapper.Map<SystemParameters_ContactUs_Translate>(postedContactUs));
                     
                    if (postedContactUs.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_ContactUs>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_ContactUs_Translate>(objByLang));
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
