using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/General")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GeneralController : ApiController
    {

        public HttpResponseMessage GetAll(string langId)
        {
            try
            {


                var retunGeneralAll = new HomeModels();
                var generalLogic = new GeneralLogic();

                DataTable homeSlidersTable;
                DataTable hotelsTable;
                DataTable newsTable;
                DataTable featuresTable;
                DataTable aboutTable;
                DataTable hotelImages;
                DataTable ownerTable;



                if (langId == Parameters.DefaultLang)
                {
                    homeSlidersTable = generalLogic
                       .Sqlread("SELECT * FROM [dbo].[SystemParameters.HomeSlider] WHERE IsDeleted=0").Tables[0];
                    hotelsTable = generalLogic
                       .Sqlread(
                           "SELECT * FROM [dbo].[Hotels] JOIN dbo.[Hotels.Images] ON dbo.Hotels.Id = dbo.[Hotels.Images].Hotel_Id WHERE dbo.Hotels.IsDeleted=0 AND dbo.[Hotels.Images].IsDeleted=0")
                       .Tables[0];
                    newsTable = generalLogic
                       .Sqlread("SELECT * FROM [dbo].[SystemParameters.News] WHERE IsDeleted=0").Tables[0];
                    featuresTable = generalLogic
                       .Sqlread("SELECT  TOP 6 * FROM [dbo].[SystemParameters.Features] WHERE IsDeleted=0").Tables[0];
                    aboutTable = generalLogic
                       .Sqlread("SELECT * FROM [dbo].[SystemParameters.About] WHERE IsDeleted=0").Tables[0];
                    hotelImages = generalLogic
                       .Sqlread(
                           "SELECT Image FROM [dbo].[Hotels] JOIN dbo.[Hotels.Images] ON dbo.Hotels.Id = dbo.[Hotels.Images].Hotel_Id WHERE dbo.Hotels.IsDeleted=0 AND dbo.[Hotels.Images].IsDeleted=0")
                       .Tables[0];
                    ownerTable = generalLogic
                       .Sqlread("SELECT * FROM [dbo].[SystemParameters.Owners] WHERE IsDeleted=0").Tables[0];
                }
                else
                {
                    homeSlidersTable = generalLogic
                        .Sqlread("SELECT * FROM [dbo].[SystemParameters.HomeSlider_Translate] WHERE LangId='" + langId + "' AND IsDeleted=0").Tables[0];

                    hotelsTable = generalLogic
                        .Sqlread("SELECT DISTINCT ([dbo].[Hotels_Translate].Id) ,[Hotels.Images_Translate].Image,[dbo].[Hotels_Translate].LangId,PriceStart,DisplayValue,DisplayValueDesc," +
                                 "[Image], [dbo].[Hotels.Features_Translate].Id FROM [dbo].[Hotels_Translate] INNER JOIN dbo.[Hotels.Images_Translate]  " +
                                 " ON [dbo].[Hotels_Translate].Id = [dbo].[Hotels.Images_Translate].Hotel_Id INNER JOIN dbo.[Hotels.Features_Translate]   " +
                                 "ON [dbo].[Hotels_Translate].Id = [dbo].[Hotels.Features_Translate].Hotel_Id  WHERE [dbo].[Hotels_Translate].LangId='" + langId + "' " +
                                 "  AND dbo.Hotels_Translate.IsDeleted=0  AND [dbo].[Hotels.Images_Translate].LangId='" + langId + "'  AND  dbo.[Hotels.Images_Translate].IsDeleted=0 " +
                                 " AND [dbo].[Hotels.Features_Translate].LangId='" + langId + "'  AND  dbo.[Hotels.Features_Translate].IsDeleted=0")
                        .Tables[0];
                    newsTable = generalLogic
                        .Sqlread("SELECT * FROM [dbo].[SystemParameters.News_Translate] WHERE LangId='" + langId + "'AND IsDeleted=0 ").Tables[0];
                    featuresTable = generalLogic
                        .Sqlread("SELECT  TOP 6 * FROM [dbo].[SystemParameters.Features_Translate] WHERE LangId='" + langId + "' AND IsDeleted=0").Tables[0];
                    aboutTable = generalLogic
                        .Sqlread("SELECT * FROM [dbo].[SystemParameters.About_Translate] WHERE LangId='" + langId + "' AND IsDeleted=0").Tables[0];
                    hotelImages = generalLogic
                        .Sqlread(
                            "SELECT Image FROM [dbo].[Hotels_Translate] JOIN dbo.[Hotels.Images_Translate] ON [dbo].[Hotels_Translate].Id = [dbo].[Hotels.Images_Translate].Hotel_Id WHERE [dbo].[Hotels_Translate].LangId='" + langId + "' AND dbo.Hotels_Translate.IsDeleted=0 AND dbo.[Hotels.Images_Translate].IsDeleted=0")
                        .Tables[0];
                    ownerTable = generalLogic
                        .Sqlread("SELECT * FROM [dbo].[SystemParameters.Owners_Translate] WHERE LangId='" + langId + "' AND IsDeleted=0").Tables[0];
                }








                //HomeSliders
                List<HomeSlider> returnHomeSlider = new List<HomeSlider>(homeSlidersTable.Rows.Count);
                foreach (DataRow dr in homeSlidersTable.Rows)
                {
                    returnHomeSlider.Add(new HomeSlider
                    {
                        Id = (int)dr["Id"],
                        //DisplayValue = (string)dr["DisplayValue"],
                        //DisplayValueDesc = (string)dr["DisplayValueDesc"],
                        Image = (string)dr["Image"]
                    });
                }


                //Get Hotels
                List<Hotels> returnHotels = new List<Hotels>(hotelsTable.Rows.Count);
                foreach (DataRow dr in hotelsTable.Rows)
                {

                    if (returnHotels.FirstOrDefault(x => x.Id == (int)dr["Id"]) != null)
                        continue;
                    returnHotels.Add(new Hotels
                    {
                        Id = (int)dr["Id"],
                        DisplayValue = (string)dr["DisplayValue"],
                        DisplayValueDesc = (string)dr["DisplayValueDesc"],
                        Image = (string)dr["Image"],
                        PriceStart = (int)dr["PriceStart"]
                    });
                }




                //Get News
                List<News> returnNews = new List<News>(newsTable.Rows.Count);
                foreach (DataRow dr in newsTable.Rows)
                {
                    returnNews.Add(new News
                    {
                        Id = (int)dr["Id"],
                        DisplayValue = (string)dr["DisplayValue"],
                        DisplayValueDesc = (string)dr["DisplayValueDesc"],
                        CreationTime = (DateTime?)dr["CreationTime"]
                    });

                }


                //Get Features
                List<Features> returnFeatures = new List<Features>(featuresTable.Rows.Count);
                foreach (DataRow dr in featuresTable.Rows)
                    returnFeatures.Add(new Features
                    {
                        DisplayValue = (string)dr["DisplayValue"],
                        DisplayValueDesc = (string)dr["DisplayValueDesc"],
                        Icon = (string)dr["Icon"]
                    });



                //Get About
                var returnAbout = new About();
                foreach (DataRow dr in aboutTable.Rows)
                {
                    returnAbout.DisplayValue = (string)dr["DisplayValue"];
                    returnAbout.DisplayValueDesc = (string)dr["DisplayValueDesc"];
                    returnAbout.Url = (string)dr["Url"];

                }


                //Get Hotel Images
                List<HotelImages> returnHotelImages = new List<HotelImages>(hotelsTable.Rows.Count);
                foreach (DataRow dr in hotelImages.Rows)
                {
                    returnHotelImages.Add(new HotelImages
                    {
                        Image = (string)dr["Image"]
                    });

                }

                //Get News
                List<Owners> returnOwners = new List<Owners>(ownerTable.Rows.Count);
                foreach (DataRow dr in ownerTable.Rows)
                {
                    returnOwners.Add(new Owners
                    {
                        Id = (int)dr["Id"],
                        DisplayValueName = (string)dr["DisplayValueName"],
                        DisplayValuePosition = (string)dr["DisplayValuePosition"],
                        DisplayValueDesc = (string)dr["DisplayValueDesc"],
                        CreationTime = (DateTime?)dr["CreationTime"]
                    });

                }









                retunGeneralAll.HomeSliders = returnHomeSlider;
                retunGeneralAll.Hotels = returnHotels;
                retunGeneralAll.News = returnNews;
                retunGeneralAll.Features = returnFeatures;
                retunGeneralAll.About = returnAbout;
                retunGeneralAll.Gallery = returnHotelImages;
                retunGeneralAll.Owners = returnOwners;




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
                var obj = careerFormLogic.GetAllWithSeen().Take(5);
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
                var obj = contactFormLogic.GetAllWithSeen().Take(5);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ContactForm>>(obj));
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
                var obj = reservationLogic.GetAllWithSeen().Take(5);
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
