using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GMG_Portal.Data;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.API.Models.SystemParameters;
using Helpers;
//using Cities = GMG_Portal.Data.Partials.SystemParameters.Cities;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Cities")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CitiesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                CitiesLogic citiesLogic = new CitiesLogic();
                var cities = citiesLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ViewModelCities>>(cities));
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
                CitiesLogic citiesLogic = new CitiesLogic();
                var cities = citiesLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ViewModelCities>>(cities));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage GetByCountryId(int countryId)
        {
            try
            {
                CitiesLogic citiesLogic = new CitiesLogic();
                var cities = citiesLogic.GetAll(countryId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ViewModelCities>>(cities));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetByCountryIdWithDeleted(int countryId)
        {
            try
            {
                CitiesLogic citiesLogic = new CitiesLogic();
                var cities = citiesLogic.GetAllWithDeleted(countryId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ViewModelCities>>(cities));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(ViewModelCities postedCity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CitiesLogic = new CitiesLogic();
                    SystemParameter_Cities City = null;
                    if (postedCity.ID.Equals(0))
                    {
                        City = CitiesLogic.Insert(Mapper.Map<SystemParameter_Cities>(postedCity));
                    }
                    else
                    {
                        if (postedCity.IsDeleted)
                        {
                            City = CitiesLogic.Delete(Mapper.Map<SystemParameter_Cities>(postedCity));
                        }
                        else
                        {
                            City = CitiesLogic.Edit(Mapper.Map<SystemParameter_Cities>(postedCity));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ViewModelCities>(City));
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
