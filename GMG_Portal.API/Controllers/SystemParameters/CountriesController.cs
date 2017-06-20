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
    [RoutePrefix("SystemParameters/Countries")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CountriesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var countriesLogic = new CountriesLogic();
                var countries= countriesLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List< API.Models.SystemParameters.Countries>>(countries));
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
                var countriesLogic = new CountriesLogic();
                var countries= countriesLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List< API.Models.SystemParameters.Countries>>(countries));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(GMG_Portal.API.Models.SystemParameters.Countries postedCountries)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CountriesLogic = new CountriesLogic();
                    Data.Countries Country = null;
                    if (postedCountries.ID.Equals(0))
                    {
                        Country = CountriesLogic.Insert(Mapper.Map<GMG_Portal.Data.Countries>(postedCountries));
                    }
                    else
                    {
                        if (postedCountries.IsDeleted)
                        {
                            Country = CountriesLogic.Delete(Mapper.Map<GMG_Portal.Data.Countries>(postedCountries));
                        }
                        else
                        {
                            Country = CountriesLogic.Edit(Mapper.Map<GMG_Portal.Data.Countries>(postedCountries));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<GMG_Portal.API.Models.SystemParameters.Countries>(Country));
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
