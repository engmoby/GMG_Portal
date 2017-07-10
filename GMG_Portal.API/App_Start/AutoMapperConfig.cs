﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using GMG_Portal.Content.Admin.SystemParameters;
using GMG_Portal.Data;
using AccountTypes = GMG_Portal.Data.Partials.SystemParameters.AccountTypes;
using Customer = GMG_Portal.API.Models.Customer.Customers.Customer;
using HomeSlider = GMG_Portal.API.Models.SystemParameters.HomeSlider; 

namespace GMG_Portal.API
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {

                cfg.CreateMap<AccountTypes, AccountType>();
                cfg.CreateMap<AccountType, AccountTypes>();


                cfg.CreateMap<Systemparameters_Languages, Models.SystemParameters.Languages>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.Languages, Systemparameters_Languages>();


                cfg.CreateMap<SystemParameters_About, About>();
                cfg.CreateMap<About, SystemParameters_About>();



                cfg.CreateMap<SystemParameters_HomeSlider, HomeSlider>();
                cfg.CreateMap<HomeSlider, SystemParameters_HomeSlider>();

                cfg.CreateMap<SystemParameters_News, News>();
                cfg.CreateMap<News, SystemParameters_News>();

                cfg.CreateMap<Front_Vision, Vision>();
                cfg.CreateMap<Vision, Front_Vision>();

                cfg.CreateMap<Front_Mission, Models.SystemParameters.Mission>();
                cfg.CreateMap<Models.SystemParameters.Mission, Front_Mission>();


                cfg.CreateMap<Hotels_Features, Features>();
                cfg.CreateMap<Features, Hotels_Features>();


                cfg.CreateMap<Data.Hotel, Hotels>();
                cfg.CreateMap<Hotels, Data.Hotel>();

                cfg.CreateMap<SystemParameters_Owners, Owners>();
                cfg.CreateMap<Owners, SystemParameters_Owners>();

                cfg.CreateMap<SystemParameters_ContactUs, ContactUs>();
                cfg.CreateMap<ContactUs, SystemParameters_ContactUs>();

                cfg.CreateMap<SystemParameters_Countries, GMG_Portal.API.Models.SystemParameters.Countries>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.Countries, SystemParameters_Countries>();

                cfg.CreateMap<GMG_Portal.Data.PaymentTypes, GMG_Portal.API.Models.SystemParameters.PaymentTypes>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.PaymentTypes, GMG_Portal.Data.PaymentTypes>();

                cfg.CreateMap<GMG_Portal.Data.InvoiceStatuses, GMG_Portal.API.Models.SystemParameters.InvoiceStatuses>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.InvoiceStatuses, GMG_Portal.Data.InvoiceStatuses>();


                cfg.CreateMap<SystemParameters_Cities, ViewModelCities>();
                cfg.CreateMap<ViewModelCities, SystemParameters_Cities>();

                cfg.CreateMap<GMG_Portal.Data.InvoiceTypes, GMG_Portal.API.Models.SystemParameters.InvoiceTypes>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.InvoiceTypes, GMG_Portal.Data.InvoiceTypes>();

                cfg.CreateMap<GMG_Portal.Data.Customers, GMG_Portal.API.Models.SystemParameters.Customer>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.Customer, GMG_Portal.Data.Customers>();


                cfg.CreateMap<GMG_Portal.Data.Departments, GMG_Portal.API.Models.SystemParameters.Departments>();
                cfg.CreateMap<GMG_Portal.API.Models.SystemParameters.Departments, GMG_Portal.Data.Departments>();


                cfg.CreateMap<GMG_Portal.Data.Customers, Customer>();
                cfg.CreateMap<Customer, GMG_Portal.Data.Customers>();

            });


        }
    }
}