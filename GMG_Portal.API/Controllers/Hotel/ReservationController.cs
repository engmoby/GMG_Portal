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
using Front.Helpers;
using GMG_Portal.API.Models.Hotels.Reservation;
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Reservation")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReservationController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var reservationLogic = new ReservationLogic();
                var reservation = reservationLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Reservation>>(reservation));

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
                var reservationLogic = new ReservationLogic();
                var reservations = reservationLogic.GetAllWithSeen();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Reservation>>(reservations));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Reservation postedReservations)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ReservationLogic = new ReservationLogic();
                    Hotels_Reservation Reservation = null;
               

                    if (postedReservations.Id.Equals(0))
                    {
                        Reservation = ReservationLogic.Insert(Mapper.Map<Hotels_Reservation>(postedReservations));
                     
                    }
                    else
                    {
                        Reservation = ReservationLogic.Edit(Mapper.Map<Hotels_Reservation>(postedReservations));
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Reservation>(Reservation));
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
