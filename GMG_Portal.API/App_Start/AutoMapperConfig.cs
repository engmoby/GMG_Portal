using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Content.Admin.SystemParameters;
using GMG_Portal.Data;
using Customer = GMG_Portal.API.Models.Customer.Customers.Customer;

namespace GMG_Portal.API
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg => {

                cfg.CreateMap<GMG_Portal.Data.AccountTypes, Models.SystemParameters.AccountType>();
                cfg.CreateMap<Models.SystemParameters.AccountType, GMG_Portal.Data.AccountTypes>();

                 

                cfg.CreateMap<SystemParameter_Countries, GMG_Portal.API.Models.SystemParameters.Countries>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.Countries, SystemParameter_Countries>();

                cfg.CreateMap<GMG_Portal.Data.PaymentTypes, GMG_Portal.API.Models.SystemParameters.PaymentTypes>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.PaymentTypes, GMG_Portal.Data.PaymentTypes>();

                cfg.CreateMap<GMG_Portal.Data.InvoiceStatuses, GMG_Portal.API.Models.SystemParameters.InvoiceStatuses>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.InvoiceStatuses, GMG_Portal.Data.InvoiceStatuses>();
                   

                cfg.CreateMap<SystemParameter_Cities,ViewModelCities>();
                cfg.CreateMap<ViewModelCities, SystemParameter_Cities>();
                 
                cfg.CreateMap<GMG_Portal.Data.InvoiceTypes, GMG_Portal.API.Models.SystemParameters.InvoiceTypes>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.InvoiceTypes, GMG_Portal.Data.InvoiceTypes>();
                 
                cfg.CreateMap<GMG_Portal.Data.Customers, GMG_Portal.API.Models.SystemParameters.Customer>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.Customer, GMG_Portal.Data.Customers>();

                     
                cfg.CreateMap<GMG_Portal.Data.Departments, GMG_Portal.API.Models.SystemParameters.Departments>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.Departments , GMG_Portal.Data.Departments>();
                  

                cfg.CreateMap<GMG_Portal.Data.Customers, Customer>();
                cfg.CreateMap<Customer, GMG_Portal.Data.Customers>();

            });


        }
    }
}