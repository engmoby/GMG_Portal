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
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Hotels")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HotelsController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var HotelLogic = new HotelLogic();
                var Hotels = HotelLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotel>>(Hotels));
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
                var HotelLogic = new HotelLogic();
                var Hotels = HotelLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotel>>(Hotels));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Hotel postedHotels)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var HotelLogic = new HotelLogic();
                    Hotel language = null;
                    if (postedHotels.Id.Equals(0))
                    {
                        language = HotelLogic.Insert(Mapper.Map<Hotel>(postedHotels));
                    }
                    else
                    {
                        if (postedHotels.IsDeleted)
                        {
                            language = HotelLogic.Delete(Mapper.Map<Hotel>(postedHotels));
                        }
                        else
                        {
                            language = HotelLogic.Edit(Mapper.Map<Hotel>(postedHotels));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Hotel>(language));
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
