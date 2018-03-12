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
                    var obj = hotelLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelsModel>>(obj));
               
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
                    var obj = hotelLogic.GetAllWithCount();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelsModel>>(obj)); 

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
                    var obj = hotelLogic.GetAllWithDeleted();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelsModel>>(obj)); 
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
                    var obj = hotelLogic.GetWithDeleted(id);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<HotelsModel>(obj)); 
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
                    var obj = hotelLogic.GetWithDeleted(id);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<HotelsModel>(obj)); 
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(HotelsModel postedHotels)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotelLogic = new HotelLogic(); 

                    Data.Hotel obj = null; 

                    if (postedHotels.Id.Equals(0))
                    { 
                            obj = hotelLogic.Insert(Mapper.Map<Data.Hotel>(postedHotels)); 
                    }
                    else
                    {
                        if (postedHotels.IsDeleted)
                        { 
                                obj = hotelLogic.Delete(Mapper.Map<Data.Hotel>(postedHotels)); 
                        }
                        else
                        { 
                                obj = hotelLogic.Edit(Mapper.Map<Data.Hotel>(postedHotels)); 
                        }
                    } 
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<HotelsModel>(obj)); 
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
                        var hotels = hotelLogic.InsertHotelImages(Mapper.Map<List<Hotels_Images>>(postedImages));
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelImages>>(hotels));
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
                       var obj = hotelLogic.DeleteImage(Mapper.Map<Hotels_Images>(postedImage), postedImage.IsDeleted);
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<HotelImages>>(obj)); 
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

                    List<Hotels_Features> obj = null;  
                        obj = hotelLogic.InsertHotelFeatures(Mapper.Map<List<Hotels_Features>>(postedFeatures));
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels_Features>>(obj)); 
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
