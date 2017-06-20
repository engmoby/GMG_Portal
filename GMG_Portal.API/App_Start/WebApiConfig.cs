using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Business.Logic;
using GMG_Portal.Data;



namespace GMG_Portal.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "SystemParametersRoute",
               routeTemplate: "SystemParameters/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
               );

            config.Routes.MapHttpRoute(
               name: "Customer",
               routeTemplate: "Customer/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
               );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           


            //  Mapper.CreateMap<AccountType. , AccountTypes >

            //var source = new Source();
            //var dest = mapper.Map<Source, Dest>(source);

            //var mapConfig = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Source, Dest>();
            //});
            //IMapper mapper = mapConfig.CreateMapper();



        }
    }
}
