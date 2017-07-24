using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper; 
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.Data;
using Helpers;

namespace GMG_Portal.API.Controllers.Offer
{
    [RoutePrefix("SystemParameters/Offers")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OffersController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var offerLogic = new OfferLogic();
                var offers = offerLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.Offer>>(offers));
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
                var offerLogic = new OfferLogic();
                var offers = offerLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotles_Offers>>(offers));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

         

        public HttpResponseMessage GetOfferDetails(int id)
        {
            try
            {
                var offerLogic = new OfferLogic();
                var offers = offerLogic.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Models.SystemParameters.Offer>(offers));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(Hotles_Offers postedOffers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var OfferLogic = new OfferLogic();
                    Hotles_Offers offers = null;
                    if (postedOffers.Id.Equals(0))
                    {
                        offers = OfferLogic.Insert(Mapper.Map<Hotles_Offers>(postedOffers));
                    }
                    else
                    {
                        if (postedOffers.IsDeleted)
                        {
                            offers = OfferLogic.Delete(Mapper.Map<Hotles_Offers>(postedOffers));
                        }
                        else
                        {
                            offers = OfferLogic.Edit(Mapper.Map<Hotles_Offers>(postedOffers));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Hotles_Offers>(offers));
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
