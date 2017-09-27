using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.API.Models.Hotels.Reservation;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.SystemParameters.Admin;
using GMG_Portal.API.Models.SystemParameters.CareerForm;
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using GMG_Portal.API.Models.SystemParameters.Newsletter; 
using GMG_Portal.Data;
using AccountTypes = GMG_Portal.Data.Partials.SystemParameters.AccountTypes;
using Customer = GMG_Portal.API.Models.Customer.Customers.Customer;
using HomeSlider = GMG_Portal.API.Models.SystemParameters.HomeSlider;
using HotelFeatures = GMG_Portal.API.Models.Hotels.Hotel.HotelFeatures;

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


                //About 
                cfg.CreateMap<SystemParameters_About, About>();
                cfg.CreateMap<About, SystemParameters_About>();
                cfg.CreateMap<SystemParameters_About_Translate, About>();
                cfg.CreateMap<About, SystemParameters_About_Translate>();
                 
                //HomeSlider
                cfg.CreateMap<SystemParameters_HomeSlider, HomeSlider>();
                cfg.CreateMap<HomeSlider, SystemParameters_HomeSlider>(); 
                cfg.CreateMap<SystemParameters_HomeSlider_Translate, HomeSlider>();
                cfg.CreateMap<HomeSlider, SystemParameters_HomeSlider_Translate>();

                //News
                cfg.CreateMap<SystemParameters_News, News>();
                cfg.CreateMap<News, SystemParameters_News>();
                cfg.CreateMap<SystemParameters_News_Translate, News>();
                cfg.CreateMap<News, SystemParameters_News_Translate>();

                //News Category
                cfg.CreateMap<SystemParameters_Category, Category>();
                cfg.CreateMap<Category, SystemParameters_Category>();
                cfg.CreateMap<SystemParameters_Category_Translate, Category>();
                cfg.CreateMap<Category, SystemParameters_Category_Translate>();

                //Vision
                cfg.CreateMap<Front_Vision, Vision>();
                cfg.CreateMap<Vision, Front_Vision>();
                cfg.CreateMap<Front_Vision_Translate, Vision>();
                cfg.CreateMap<Vision, Front_Vision_Translate>();

                //Mission
                cfg.CreateMap<Front_Mission,  Mission>();
                cfg.CreateMap<Mission, Front_Mission>();
                cfg.CreateMap<Front_Mission_Translate, Mission>();
                cfg.CreateMap<Mission, Front_Mission_Translate>();

                // Features
                cfg.CreateMap<SystemParameters_Features, Features>();
                cfg.CreateMap<Features, SystemParameters_Features>();
                cfg.CreateMap<SystemParameters_Features_Translate, Features>();
                cfg.CreateMap<Features, SystemParameters_Features_Translate>();

                //Hotels
                cfg.CreateMap<Data.Hotel, Hotels>();
                cfg.CreateMap<Hotels, Data.Hotel>(); 
                cfg.CreateMap<Data.Hotels_Translate, Hotels>();
                cfg.CreateMap<Hotels, Data.Hotels_Translate>();


                cfg.CreateMap<Hotels_Images, HotelImages>();
                cfg.CreateMap<HotelImages, Hotels_Images>();
                cfg.CreateMap<Hotels_Images_Translate, HotelImages>();
                cfg.CreateMap<HotelImages, Hotels_Images_Translate>();

                cfg.CreateMap<Hotels_Features, HotelFeatures>();
                cfg.CreateMap<HotelFeatures, Hotels_Features>();
                cfg.CreateMap<Hotels_Features_Translate, HotelFeatures>();
                cfg.CreateMap<HotelFeatures, Hotels_Features_Translate>();


                //Owners
                cfg.CreateMap<SystemParameters_Owners, Owners>();
                cfg.CreateMap<Owners, SystemParameters_Owners>();
                cfg.CreateMap<SystemParameters_Owners_Translate, Owners>();
                cfg.CreateMap<Owners, SystemParameters_Owners_Translate>();


                //Core Values
                cfg.CreateMap<SystemParameters_CoreValues, CoreValues>();
                cfg.CreateMap<CoreValues, SystemParameters_CoreValues>();
                cfg.CreateMap<SystemParameters_CoreValues_Translate, CoreValues>();
                cfg.CreateMap<CoreValues, SystemParameters_CoreValues_Translate>();

                //Contact Us
                cfg.CreateMap<SystemParameters_ContactUs, ContactUs>();
                cfg.CreateMap<ContactUs, SystemParameters_ContactUs>();
                cfg.CreateMap<SystemParameters_ContactUs_Translate, ContactUs>();
                cfg.CreateMap<ContactUs, SystemParameters_ContactUs_Translate>();


                //Offers
                cfg.CreateMap<Hotles_Offers, Offer>();
                cfg.CreateMap<Offer, Hotles_Offers>();
                cfg.CreateMap<Hotles_Offers_Translate, Offer>();
                cfg.CreateMap<Offer, Hotles_Offers_Translate>();
                cfg.CreateMap<Hotles_Offers_Translate, Hotles_Offers>();


                //Currency
                cfg.CreateMap<Currency, CurrencyVm>();
                cfg.CreateMap<CurrencyVm, Currency>();
                cfg.CreateMap<Currency_Translate, CurrencyVm>();
                cfg.CreateMap<CurrencyVm, Currency_Translate>();


                cfg.CreateMap<SystemParameters_Newsletter, Newsletter>();
                cfg.CreateMap<Newsletter, SystemParameters_Newsletter>();

                cfg.CreateMap<SystemParameters_ContactForm, ContactForm>();
                cfg.CreateMap<ContactForm, SystemParameters_ContactForm>();


                cfg.CreateMap<SystemParameters_Careers, Career>();
                cfg.CreateMap<Career, SystemParameters_Careers>();

                cfg.CreateMap<SystemParameters_CareerForm, CareerForm>();
                cfg.CreateMap<CareerForm, SystemParameters_CareerForm>();



                cfg.CreateMap<Hotels_Reservation, Reservation>();
                cfg.CreateMap<Reservation, Hotels_Reservation>();


                cfg.CreateMap<SystemParameters_Notify, NotifyViewModel>();
                cfg.CreateMap<NotifyViewModel, SystemParameters_Notify>();

                cfg.CreateMap<Systemparameters_Admin, Admin>();
                cfg.CreateMap<Admin, Systemparameters_Admin>();

                cfg.CreateMap<SystemParameters_NotifyDepartment, Department>();
                cfg.CreateMap<Department, SystemParameters_NotifyDepartment>();

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

                 

                cfg.CreateMap<GMG_Portal.Data.Customers, Customer>();
                cfg.CreateMap<Customer, GMG_Portal.Data.Customers>();

            });


        }
    }
}