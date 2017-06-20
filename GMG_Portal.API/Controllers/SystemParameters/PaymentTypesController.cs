using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Data;
using Helpers;
using GMG_Portal.Business.Logic.SystemParameters;
using AutoMapper;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/PaymentTypes")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PaymentTypesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                PaymentTypesLogic paymentTypesLogic = new PaymentTypesLogic();
                var paymentTypes = paymentTypesLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.PaymentTypes>>(paymentTypes));
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
                PaymentTypesLogic paymentTypesLogic = new PaymentTypesLogic();
                var paymentTypes = paymentTypesLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.PaymentTypes>>(paymentTypes));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(API.Models.SystemParameters.PaymentTypes postedPaymentType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var paymentTypesLogic = new PaymentTypesLogic();
                    Data.PaymentTypes paymentType = null;
                    if (postedPaymentType.ID.Equals(0))
                    {
                        paymentType = paymentTypesLogic.Insert(Mapper.Map<GMG_Portal.Data.PaymentTypes>(postedPaymentType));
                    }
                    else
                    {
                        if (postedPaymentType.IsDeleted)
                        {
                            paymentType = paymentTypesLogic.Delete(Mapper.Map<GMG_Portal.Data.PaymentTypes>(postedPaymentType));
                        }
                        else
                        {
                            paymentType = paymentTypesLogic.Edit(Mapper.Map<GMG_Portal.Data.PaymentTypes>(postedPaymentType));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<GMG_Portal.API.Models.SystemParameters.PaymentTypes>(paymentType));
                }

                goto throwBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        throwBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
