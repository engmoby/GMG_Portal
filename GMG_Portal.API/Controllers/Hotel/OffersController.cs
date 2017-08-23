using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.Data;
using Heloper;
using Helpers;

namespace GMG_Portal.API.Controllers.Offer
{
    [RoutePrefix("SystemParameters/Offers")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OffersController : ApiController
    {
    


        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var offerLogic = new OfferLogic();
                var offerLogicTranslate = new OfferLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = offerLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotles_Offers>>(obj));
                }
                else

                {
                    var objByLang = offerLogicTranslate.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotles_Offers_Translate>>(objByLang));
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllWithDeleted(string langId)
        {
            try
            {
                var offerLogic = new OfferLogic();
                var offerLogicTranslate = new OfferLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = offerLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotles_Offers>>(obj));

                }
                else

                {
                    var objByLang = offerLogicTranslate.GetAllWithDeleted(langId);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Hotles_Offers_Translate>>(objByLang));

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }





        public HttpResponseMessage GetOfferDetails(int id, string langId)
        {
            try
            {
                var offerLogic = new OfferLogic();
                var offerLogicTranslate = new OfferLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = offerLogic.GetOfferInfo(id);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Hotles_Offers>(obj));
                }
                else
                {
                    var objTranslate = offerLogicTranslate.GetOfferInfo(id,langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Hotles_Offers_Translate>(objTranslate));
                }
                  


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
                    var offerLogic = new OfferLogic();
                    var offerLogicTranslate = new OfferLogicTranslate();
                    Hotles_Offers obj = null;
                    Hotles_Offers_Translate objTranslate =  null;
                    if (postedOffers.Id.Equals(0))
                    {
                        if (postedOffers.langId == Parameters.DefaultLang)
                        {
                            obj = offerLogic.Insert(Mapper.Map<Hotles_Offers>(postedOffers));
                        }
                        else
                            objTranslate = offerLogicTranslate.Insert(Mapper.Map<Hotles_Offers_Translate>(postedOffers));
                    }

                    else
                    {
                        if (postedOffers.IsDeleted)
                        {
                            if (postedOffers.langId == Parameters.DefaultLang)
                                obj = offerLogic.Delete(Mapper.Map<Hotles_Offers>(postedOffers));
                            else
                                objTranslate =
                                    offerLogicTranslate.Delete(Mapper.Map<Hotles_Offers_Translate>(postedOffers));
                        }


                        else
                        {
                            if (postedOffers.langId == Parameters.DefaultLang)
                                obj = offerLogic.Edit(Mapper.Map<Hotles_Offers>(postedOffers));
                            else
                                objTranslate= offerLogicTranslate.Edit(Mapper.Map<Hotles_Offers_Translate>(postedOffers));

                        }
                    
                    }
                    if (postedOffers.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Hotles_Offers>(obj));
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Hotles_Offers_Translate>(objTranslate));
                    }
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
