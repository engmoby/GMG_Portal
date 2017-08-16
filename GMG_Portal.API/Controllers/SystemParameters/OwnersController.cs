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
using Heloper;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Owners")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OwnersController : ApiController
    {
    
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var ownersLogic = new OwnerLogic();
                var ownersLogicTranslate = new OwnerLogicTranslate();
                if (langId == Parameters.DefaultLang)
                {
                    var obj = ownersLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Owners>>(obj));
                }
                else

                {
                    var objByLang = ownersLogicTranslate.GetAll(langId);
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Owners>>(objByLang));
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
                var ownersLogic = new OwnerLogic();
                var ownersLogicTranslate = new OwnerLogicTranslate();

                if (langId == Parameters.DefaultLang)
                {
                    var obj = ownersLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Owners>>(obj));

                }
                else

                {
                    var objByLang = ownersLogicTranslate.GetAllWithDeleted(langId);

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Owners>>(objByLang));

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }




        [HttpPost]
        public HttpResponseMessage Save(Owners postedOwners)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ownersLogic = new OwnerLogic();
                    var ownersLogicTranslate = new OwnerLogicTranslate();

                    SystemParameters_Owners obj = null;
                    SystemParameters_Owners_Translate objByLang = null;
                    if (postedOwners.Id.Equals(0))
                    {
                        if (postedOwners.langId == Parameters.DefaultLang)
                            obj = ownersLogic.Insert(Mapper.Map<SystemParameters_Owners>(postedOwners));
                        else
                            objByLang = ownersLogicTranslate.Insert(Mapper.Map<SystemParameters_Owners_Translate>(postedOwners));
                    }
                    else
                    {
                        if (postedOwners.IsDeleted)
                        {
                            if (postedOwners.langId == Parameters.DefaultLang)
                                obj = ownersLogic.Delete(Mapper.Map<SystemParameters_Owners>(postedOwners));
                            else
                                objByLang = ownersLogicTranslate.Delete(Mapper.Map<SystemParameters_Owners_Translate>(postedOwners));
                        }
                        else
                        {
                            if (postedOwners.langId == Parameters.DefaultLang)
                                obj = ownersLogic.Edit(Mapper.Map<SystemParameters_Owners>(postedOwners));
                            else
                                objByLang = ownersLogicTranslate.Edit(Mapper.Map<SystemParameters_Owners_Translate>(postedOwners));
                        }
                    }
                    if (postedOwners.langId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_Owners>(obj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<SystemParameters_Owners_Translate>(objByLang));

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
