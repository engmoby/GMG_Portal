using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.Business.Logic.SystemParameters;
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

        [HttpPost]
        public HttpResponseMessage Save(Data.Hotel postedHotels)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var HotelLogic = new HotelLogic();
                    Data.Hotel language = null;
                    if (postedHotels.Id.Equals(0))
                    {
                        language = HotelLogic.Insert(Mapper.Map<Data.Hotel>(postedHotels));
                    }
                    else
                    {
                        if (postedHotels.IsDeleted)
                        {
                            language = HotelLogic.Delete(Mapper.Map<Data.Hotel>(postedHotels));
                        }
                        else
                        {
                            language = HotelLogic.Edit(Mapper.Map<Data.Hotel>(postedHotels));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Data.Hotel>(language));
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
