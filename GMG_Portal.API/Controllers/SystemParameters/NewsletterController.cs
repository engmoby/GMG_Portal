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
using GMG_Portal.API.Models.SystemParameters.Newsletter;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Newsletter")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NewsletterController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var newsletterLogic = new NewsletterLogic();
                var newsletter = newsletterLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Newsletter>>(newsletter));
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
                var newsletterLogic = new NewsletterLogic();
                var newsletters = newsletterLogic.GetAllWithSeen();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Newsletter>>(newsletters));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Newsletter postedNewsletters)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newsletterLogic = new NewsletterLogic();
                    SystemParameters_Newsletter newsletter = null;
                    if (postedNewsletters.Id.Equals(0))
                    {
                        newsletter = newsletterLogic.Insert(Mapper.Map<SystemParameters_Newsletter>(postedNewsletters));
                    }
                    else
                    {

                        newsletter = newsletterLogic.Edit(Mapper.Map<SystemParameters_Newsletter>(postedNewsletters));

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Newsletter>(newsletter));
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
