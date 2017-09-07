using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Data;
using GMG_Portal.Business.Logic.SystemParameters;
using AutoMapper;
using GMG_Portal.API.Models.SystemParameters.Newsletter;
using Heloper;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [System.Web.Http.RoutePrefix("SystemParameters/Newsletter")]
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
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Save(Newsletter postedNewsletters)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newsletterLogic = new NewsletterLogic();
                    SystemParameters_Newsletter newsletter = null;
                    
                        newsletter = newsletterLogic.Insert(Mapper.Map<SystemParameters_Newsletter>(postedNewsletters));
                 
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

        public HttpResponseMessage Getcsv()
        {
            var newsletterLogic = new NewsletterLogic();
            var newsletter = newsletterLogic.GetAll();
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            StringBuilder builtString = new StringBuilder();

            foreach (var obj in newsletter)
            {
                builtString.Append(obj.Mail);
                builtString.Append(",");

            }

            //Fix for Last Comma 
         //   builtString.Remove(builtString.Length, 1);


            writer.Write(builtString);
            writer.Flush();
            stream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = ("Newsletter_ " + Parameters.CurrentDateTime.ToShortDateString() + ".csv") };
            return result;
        }

     



    }

}
