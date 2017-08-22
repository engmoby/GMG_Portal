using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.Data;
using Heloper;
using Helpers;

namespace GMG_Portal.API.Controllers.Hotel
{
    [RoutePrefix("SystemParameters/Hotels")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HotelsController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var hotelLogic = new HotelLogic();
                var hotelTranslateLogic = new HotelLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = hotelLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Data.Hotel>>(obj));
                }
                else
                {
                    var objByLang = hotelTranslateLogic.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Data.Hotels_Translate>>(objByLang));
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetAllWithCount(string langId)
        {
            try
            {
                var hotelLogic = new HotelLogic();
                var hotelTranslateLogic = new HotelLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = hotelLogic.GetAllWithCount();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Data.Hotel>>(obj));
                }
                else
                {
                    var objByLang = hotelTranslateLogic.GetAllWithCount(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Data.Hotels_Translate>>(objByLang));
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetAllWithDeleted(string langId)
        {
            try
            {
                var hotelLogic = new HotelLogic();
                var hotelTranslateLogic = new HotelLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = hotelLogic.GetAllWithDeleted();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Data.Hotel>>(obj));
                }
                else
                {
                    var objByLang = hotelTranslateLogic.GetAllWithDeleted(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Data.Hotels_Translate>>(objByLang));
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }

        public HttpResponseMessage GetAllImages(string langId)
        {
            try
            {
                var hotelLogic = new HotelLogic(); 
                var hotelTranslateLogic = new HotelLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var hotels = hotelLogic.GetAllImages();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelImages>>(hotels));
                }
                else
                {
                    var objByLang = hotelTranslateLogic.GetAllImages(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelImages>>(objByLang));
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage GetHotelDetails(int id, string langId)
        {
            try
            {
                var hotelLogic = new HotelLogic();
                var hotelTranslateLogic = new HotelLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = hotelLogic.Get(id);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotel>(obj));
                }
                else
                {
                    var objByLang = hotelTranslateLogic.Get(id, langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotels_Translate>(objByLang));
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetHotelDetailsWith(int id, string langId)
        {
            try
            {
                var hotelLogic = new HotelLogic();
                var hotelTranslateLogic = new HotelLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = hotelLogic.GetWithDeleted(id);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotel>(obj));
                }
                else
                {
                    var objByLang = hotelTranslateLogic.GetWithDeleted(id, langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotels_Translate>(objByLang));
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(Hotels postedHotels)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotelLogic = new HotelLogic();
                    var hotelLogicTranslate = new HotelLogicTranslate();

                    Data.Hotel obj = null;
                    Hotels_Translate objByLang = null;

                    if (postedHotels.Id.Equals(0))
                    {
                        if (postedHotels.langId == Parameters.DefaultLang)
                            obj = hotelLogic.Insert(Mapper.Map<Data.Hotel>(postedHotels));
                        else
                            objByLang = hotelLogicTranslate.Insert(Mapper.Map<Data.Hotels_Translate>(postedHotels));
                    }
                    else
                    {
                        if (postedHotels.IsDeleted)
                        {
                            if (postedHotels.langId == Parameters.DefaultLang)
                                obj = hotelLogic.Delete(Mapper.Map<Data.Hotel>(postedHotels));
                            else
                                objByLang = hotelLogicTranslate.Delete(Mapper.Map<Data.Hotels_Translate>(postedHotels));
                        }
                        else
                        {

                            if (postedHotels.langId == Parameters.DefaultLang)
                                obj = hotelLogic.Edit(Mapper.Map<Data.Hotel>(postedHotels));
                            else
                                objByLang = hotelLogicTranslate.Edit(Mapper.Map<Data.Hotels_Translate>(postedHotels));
                        }
                    }
                    if (postedHotels.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotel>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotels_Translate>(objByLang));
                }
                goto ThrowBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }

            ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public HttpResponseMessage SaveImages(List<HotelImages> postedImages)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotelLogic = new HotelLogic();
                    var hotelTranslateLogic = new HotelLogicTranslate();
                    if (postedImages[0].langId == Parameters.DefaultLang)
                    {
                        var hotels = hotelLogic.InsertHotelImages(Mapper.Map<List<Hotels_Images>>(postedImages));
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelImages>>(hotels));
                    }
                    else
                    {
                        var objByLang = hotelTranslateLogic.InsertHotelImages(Mapper.Map<List<Hotels_Images_Translate>>(postedImages));
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels_Images_Translate>>(objByLang));
                    } 
                }
                goto ThrowBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }

            ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        public HttpResponseMessage DeleteImage(HotelImages postedImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotelLogic = new HotelLogic(); 
                    var hotelTranslateLogic = new HotelLogicTranslate();
                    if (postedImage.langId == Parameters.DefaultLang)
                    {
                       var obj = hotelLogic.DeleteImage(Mapper.Map<Hotels_Images>(postedImage), postedImage.IsDeleted);
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelImages>>(obj));
                    }
                    else
                    {
                        var objByLang = hotelTranslateLogic.DeleteImage(Mapper.Map<Hotels_Images_Translate>(postedImage), postedImage.IsDeleted);
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels_Images_Translate>>(objByLang));
                    } 
                }
                goto ThrowBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }

            ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        public HttpResponseMessage SaveFeatures(List<HotelFeatures> postedFeatures)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotelLogic = new HotelLogic();
                    var hotelLogicTranslate = new HotelLogicTranslate();

                    List<Hotels_Features> obj = null;
                    List <Hotels_Features_Translate> objByLang = null;
                    if (postedFeatures[0].langId == Parameters.DefaultLang)
                    {   
                        obj = hotelLogic.InsertHotelFeatures(Mapper.Map<List<Hotels_Features>>(postedFeatures));
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels_Features>>(obj));
                    }
                    else
                    {
                        objByLang = hotelLogicTranslate.InsertHotelFeatures(Mapper.Map<List<Hotels_Features_Translate>>(postedFeatures)); 
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels_Features_Translate>>(objByLang));
                    }
                }
                goto ThrowBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }

            ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }

}
