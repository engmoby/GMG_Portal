using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMG_Portal.Data;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.API.Models.SystemParameters;
using AutoMapper;
using Helpers;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Validation.Providers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/AccountTypes")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountTypesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var accountTypesLogic = new AccountTypeLogic();
                var accountTypes = accountTypesLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.AccountType>>(accountTypes));
            }

            catch(Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        public HttpResponseMessage GetAllWithDeleted()
        {
            try
            {
                var accountTypesLogic = new AccountTypeLogic();
                var accountTypes = accountTypesLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.AccountType>>(accountTypes));
            }

            catch (Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        public HttpResponseMessage Save(GMG_Portal.API.Models.SystemParameters.AccountType PostedAccountType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var AccountTypesLogic = new AccountTypeLogic();
                    Data.AccountTypes AccountType = null;
                    if (PostedAccountType.ID.Equals(0))
                    {
                        AccountType = AccountTypesLogic.Insert(Mapper.Map<GMG_Portal.Data.AccountTypes>(PostedAccountType));
                    }
                    else
                    {
                        if (PostedAccountType.IsDeleted)
                        {
                            AccountType = AccountTypesLogic.Delete(Mapper.Map<GMG_Portal.Data.AccountTypes>(PostedAccountType));
                        }
                        else
                        {
                            AccountType = AccountTypesLogic.Edit(Mapper.Map<GMG_Portal.Data.AccountTypes>(PostedAccountType));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<GMG_Portal.API.Models.SystemParameters.AccountType>(AccountType));
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
