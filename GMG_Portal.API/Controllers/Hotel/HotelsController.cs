using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.Data;
using Helpers;

namespace GMG_Portal.API.Controllers.Hotel
{
    [RoutePrefix("SystemParameters/Hotels")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HotelsController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var hotelLogic = new HotelLogic();
                var hotels = hotelLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels>>(hotels));
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
                var hotelLogic = new HotelLogic();
                var hotels = hotelLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotels>>(hotels));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
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

        public HttpResponseMessage GetHotelDetails(int id)
        {
            try
            {
                var hotelLogic = new HotelLogic();
                var hotels = hotelLogic.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Hotels>(hotels));
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
                    Data.Hotel hotel = null;
                    if (postedHotels.Id.Equals(0))
                    {
                        hotel = hotelLogic.Insert(Mapper.Map<Data.Hotel>(postedHotels));
                    }
                    else
                    {
                        if (postedHotels.IsDeleted)
                        {
                            hotel = hotelLogic.Delete(Mapper.Map<Data.Hotel>(postedHotels));
                        }
                        else
                        {
                            hotel = hotelLogic.Edit(Mapper.Map<Data.Hotel>(postedHotels));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotel>(hotel));
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

    }

}
