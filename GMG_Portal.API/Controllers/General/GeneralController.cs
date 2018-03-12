using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Data;
using GMG_Portal.Business.Logic.SystemParameters;
using AutoMapper;
using GMG_Portal.API.Models.General;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.API.Models.Hotels.Reservation;
using GMG_Portal.API.Models.SystemParameters.CareerForm;
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using GMG_Portal.Business.Logic.General;
using Heloper;
using Helpers;
using System.Web;
namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/General")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GeneralController : ApiController
    {
        private string PopulateBody(string url)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Invetation.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Url}", url);
            return body;
        }
        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            string FromMail = "gmggroupsoftware@gmail.com";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("in-v3.mailjet.com");
            mail.From = new MailAddress(FromMail);
            mail.To.Add(recepientEmail);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("9d7c1de804eabdf8fedf498bffadd546", "93187ce363c3beb198214badc25cdc3c");
            SmtpServer.EnableSsl = false;
            SmtpServer.Send(mail);

        }

        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                string url = "http://gmgportal.azurewebsites.net/Home/Index";
                //string body = PopulateBody(url);

                //SendHtmlFormattedEmail("m.abdo@gmggroupsoft.com", "New Invetation!", body);

                var retunGeneralAll = new HomeModels(); 
                var homeSlidersLogic = new HomeSlidersLogic();
                var hotelLogic = new HotelLogic();
                var newsLogic = new NewsLogic();
                var featureLogic = new FeaturesLogic();
                var aboutLogic = new AboutLogic();
                var ownerLogic = new OwnerLogic();
                var currencyLogic = new CurrencyLogic();

                var objHomeSliders = homeSlidersLogic.GetAll();
                var objHotels = hotelLogic.GetAll();
                var objNews = newsLogic.GetAll();
                var objFeatures = featureLogic.GetAllByTake6();
                var objAbout = aboutLogic.GetAll();
                var objHotelsImages = hotelLogic.GetAllImages();
                var objOwner = ownerLogic.GetAll(); 
                var objCurrency = currencyLogic.GetAll();


                retunGeneralAll.HomeSliders = Mapper.Map<List<HomeSliderModel>>(objHomeSliders);
                retunGeneralAll.Hotels = Mapper.Map<List<HotelsModel>>(objHotels);
                retunGeneralAll.News = Mapper.Map<List<NewsModel>>(objNews);
                retunGeneralAll.Features = Mapper.Map<List<FeaturesModel>>(objFeatures);
                retunGeneralAll.About = Mapper.Map<AboutModel>(objAbout);
                retunGeneralAll.Gallery = Mapper.Map<List<HotelImages>>(objHotelsImages);
                retunGeneralAll.Owners = Mapper.Map<List<OwnerModel>>(objOwner);
                retunGeneralAll.Currency = Mapper.Map<List<CurrencyModel>>(objCurrency);




                return Request.CreateResponse(HttpStatusCode.OK, retunGeneralAll);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage GetAllAppliedCarrer()
        {
            try
            {
                var careerFormLogic = new CareerFormLogic();
                var obj = careerFormLogic.GetAllWithSeenHomePage();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CareerForm>>(obj));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllContactInquiry()
        {
            try
            {
                var contactFormLogic = new ContactFormLogic();
                var obj = contactFormLogic.GetAllWithSeenHomePage();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ContactForm>(obj));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllHotelReservation()
        {
            try
            {
                var reservationLogic = new ReservationLogic();
                var obj = reservationLogic.GetAllWithSeenHomePage();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Reservation>>(obj));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }



    }

}
