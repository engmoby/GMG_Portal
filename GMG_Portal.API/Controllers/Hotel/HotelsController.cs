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
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Data.Hotel>>(objByLang));
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
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Data.Hotel>>(objByLang));
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

        public HttpResponseMessage GetAllImages()
        {
            try
            {
                var hotelLogic = new HotelLogic();
                var hotels = hotelLogic.GetAllImages();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelImages>>(hotels));
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
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotel>(objByLang));
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
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotel>(objByLang));
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
        public HttpResponseMessage SaveImages(List<Hotels_Images> postedImages)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotelLogic = new HotelLogic();

                    var image = hotelLogic.InsertHotelImages(Mapper.Map<List<Hotels_Images>>(postedImages));
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels_Images>>(image));
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
        public HttpResponseMessage DeleteImage(Hotels_Images postedImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotelLogic = new HotelLogic();
                    Hotels_Images image = null;

                    image = hotelLogic.DeleteImage(Mapper.Map<Hotels_Images>(postedImage), postedImage.IsDeleted);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Hotels_Images>(image));
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
                    Hotels_Translate objByLang = null;
                    if (postedFeatures[0].langId == Parameters.DefaultLang)
                    {
                        obj = hotelLogic.InsertHotelFeatures(Mapper.Map<List<Hotels_Features>>(postedFeatures));
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels_Features>>(obj));
                    }
                    //else
                    //{
                    //    objByLang = hotelLogicTranslate.InsertHotelFeatures(Mapper.Map<List<Hotels_Features_Translate>>(postedFeatures));

                    //    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels_Features_Translate>>(objByLang));
                    //}
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
