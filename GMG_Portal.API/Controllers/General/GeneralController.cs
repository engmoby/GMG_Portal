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
using GMG_Portal.API.Models.General;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/General")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GeneralController : ApiController
    {

        public HttpResponseMessage GetAll()
        {
            try
            {
                var retunGeneralAll = new HomeModels();
                var returnHomeSlider = new List<HomeSlider>();
                var returnAbout = new About();
                var returnFeatures = new List<Features>();
                var returnHotels = new List<Hotels>();
                var returnOwners = new List<Owners>();
                var returnNews = new List<News>();
                var returnContactUs = new ContactUs();
                var returnHotelImages = new List<HotelImages>();
                #region HomeSlider 

                var homeSliderLogic = new HomeSlidersLogic();
                var homeslider = homeSliderLogic.GetAll();
                foreach (var systemParametersHomeSlider in homeslider)
                {
                    returnHomeSlider.Add(new HomeSlider
                    {
                        DisplayValue = systemParametersHomeSlider.DisplayValue,
                        DisplayValueDesc = systemParametersHomeSlider.DisplayValueDesc,
                        Image = systemParametersHomeSlider.Image
                    });
                }
                retunGeneralAll.HomeSliders = returnHomeSlider;
                #endregion

                #region About


                var aboutLogic = new AboutLogic();
                var about = aboutLogic.GetAll();
                returnAbout.DisplayValue = about.DisplayValue;
                returnAbout.DisplayValueDesc = about.DisplayValueDesc;
                returnAbout.Url = about.Url;
                retunGeneralAll.About = returnAbout;
                #endregion

                #region Features
                var featuresLogic = new FeaturesLogic();
                var features = featuresLogic.GetAllByTake6();
                foreach (var systemParametersfeatures in features)
                {
                    returnFeatures.Add(new Features
                    {
                        DisplayValue = systemParametersfeatures.DisplayValue,
                        DisplayValueDesc = systemParametersfeatures.DisplayValueDesc,
                        Icon = systemParametersfeatures.Icon
                    });
                }
                retunGeneralAll.Features = returnFeatures;


                #endregion

                #region Hotels

                var hotelsLogic = new HotelLogic();
                var hotels = hotelsLogic.GetAll();
                foreach (var systemParametersHotels in hotels)
                {
                    returnHotels.Add(new Hotels
                    {
                        Id = systemParametersHotels.Id,
                        DisplayValue = systemParametersHotels.DisplayValue,
                        DisplayValueDesc = systemParametersHotels.DisplayValueDesc,
                        Image = systemParametersHotels.Image,
                        Rate = systemParametersHotels.Rate,
                        PriceStart = systemParametersHotels.PriceStart
                    });
                }
                retunGeneralAll.Hotels = returnHotels;

                #endregion

                #region Owners
                var ownersLogic = new OwnerLogic();
                var owners = ownersLogic.GetAll();
                foreach (var systemParametersOwners in owners)
                {
                    returnOwners.Add(new Owners
                    {
                        Id = systemParametersOwners.Id,
                        DisplayValuePosition = systemParametersOwners.DisplayValuePosition,
                        DisplayValueName = systemParametersOwners.DisplayValueName,
                        DisplayValueDesc = systemParametersOwners.DisplayValueDesc,
                        Image = systemParametersOwners.Image
                    });
                }
                retunGeneralAll.Owners = returnOwners;

                #endregion

                #region News
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetAllWithCount("en");
                foreach (var systemParametersNews in news.Take(3))
                {
                    returnNews.Add(new News
                    {
                        Id = systemParametersNews.Id,
                        DisplayValue = systemParametersNews.DisplayValue,
                        DisplayValueDesc = systemParametersNews.DisplayValueDesc,
                        Image = systemParametersNews.Image,
                        CreationTime = systemParametersNews.CreationTime,
                        CreationDay = systemParametersNews.CreationTime.Day,
                        CreationMonth = systemParametersNews.CreationTime.ToString("MMM"),
                        CreatorUserName = "Administrator",
                        CategoryName = systemParametersNews.DisplayValue, 
                        Categories = Mapper.Map<List<Category>>(newsLogic.GetAllCatrogry())
                    });
                }
                retunGeneralAll.News = returnNews;


                #endregion

                #region ContactUs 
                var contactLogic = new ContactUsLogic();
                var contact = contactLogic.GetAll();
                returnContactUs.DisplayValueAddress = contact.DisplayValueAddress;
                returnContactUs.DisplayValueDesc = contact.DisplayValueDesc;
                returnContactUs.Url = contact.Url;
                returnContactUs.Facebook = contact.Facebook;
                returnContactUs.Twitter = contact.Twitter;
                returnContactUs.Youtube = contact.Youtube;
                returnContactUs.Snapchat = contact.Snapchat;
                returnContactUs.Instgram = contact.Instgram;
                returnContactUs.Fax = contact.Fax;
                returnContactUs.WhatsApp = contact.WhatsApp;
                returnContactUs.PhoneNo1 = contact.PhoneNo1;
                returnContactUs.PhoneNo2 = contact.PhoneNo2;
                returnContactUs.PostalCode = contact.PostalCode;
                returnContactUs.Mailbox = contact.Mailbox;
                returnContactUs.Long = contact.Long;
                returnContactUs.Late = contact.Late;
                returnContactUs.MailNo1 = contact.MailNo1;
                returnContactUs.MailNo2 = contact.MailNo2;
                retunGeneralAll.ContactUs = returnContactUs;

                #endregion

                #region Gallery
                var galleryLogic = new HotelLogic();
                var gallery = galleryLogic.GetAllImages();
                foreach (var systemParametersGallery in gallery)
                {
                    returnHotelImages.Add(new HotelImages
                    {
                        Image = systemParametersGallery.Image
                    });
                }

                retunGeneralAll.Gallery = returnHotelImages;

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, retunGeneralAll);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

    }

}
