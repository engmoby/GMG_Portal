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
using GMG_Portal.Business.Logic.Customer;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/InvoiceStatuses")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InvoiceStatusesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var invoiceStatusesLogic = new InvoiceStatusesLogic();
                var InvoiceStatus = invoiceStatusesLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.InvoiceStatuses>>(InvoiceStatus));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(GMG_Portal.API.Models.SystemParameters.InvoiceStatuses PostedInvoiceStatus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var invoiceStatusesLogic = new InvoiceStatusesLogic();
                    Data.InvoiceStatuses InvoiceStatus = null;
                    if (PostedInvoiceStatus.ID.Equals(0))
                    {
                        InvoiceStatus = invoiceStatusesLogic.Insert(Mapper.Map<GMG_Portal.Data.InvoiceStatuses>(PostedInvoiceStatus));
                    }
                    else
                    {
                        if (PostedInvoiceStatus.IsDeleted)
                        {
                            InvoiceStatus = invoiceStatusesLogic.Delete(Mapper.Map<GMG_Portal.Data.InvoiceStatuses>(PostedInvoiceStatus));
                        }
                        else
                        {
                            InvoiceStatus = invoiceStatusesLogic.Edit(Mapper.Map<GMG_Portal.Data.InvoiceStatuses>(PostedInvoiceStatus));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<GMG_Portal.API.Models.SystemParameters.InvoiceStatuses>(InvoiceStatus));
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
