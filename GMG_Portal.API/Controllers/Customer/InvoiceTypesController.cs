using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMG_Portal.Data;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Business.Logic.SystemParameters;
using AutoMapper;
using GMG_Portal.Business.Logic.Customer;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/InvoiceTypes")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InvoiceTypesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var InvoiceTypesLogic = new InvoiceTypesLogic();
                var InvoiceTypes = InvoiceTypesLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.InvoiceTypes>>(InvoiceTypes));
            }

            catch (Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        public HttpResponseMessage Save(GMG_Portal.API.Models.SystemParameters.InvoiceTypes PostedInvoiceType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var InvoiceTypesLogic = new InvoiceTypesLogic();
                    Data.InvoiceTypes InvoiceType = null;
                    if (PostedInvoiceType.ID.Equals(0))
                    {
                        InvoiceType = InvoiceTypesLogic.Insert(Mapper.Map<GMG_Portal.Data.InvoiceTypes>(PostedInvoiceType));
                    }
                    else
                    {
                        if (PostedInvoiceType.IsDeleted)
                        {
                            InvoiceType = InvoiceTypesLogic.Delete(Mapper.Map<GMG_Portal.Data.InvoiceTypes>(PostedInvoiceType));
                        }
                        else
                        {
                            InvoiceType = InvoiceTypesLogic.Edit(Mapper.Map<GMG_Portal.Data.InvoiceTypes>(PostedInvoiceType));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<GMG_Portal.API.Models.SystemParameters.InvoiceTypes>(InvoiceType));
                }
                goto ThrowBadRequest;

            }

            catch (Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }
    }
}
