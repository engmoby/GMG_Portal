using AutoMapper;
using System.Linq;
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


                cfg.CreateMap<Systemparameters_Languages, Languages>();
                cfg.CreateMap<Languages, Systemparameters_Languages>();

                //News Category
                cfg.CreateMap<Category, CategoryModel>()
                    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.Category_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.Category_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));

                cfg.CreateMap<CategoryModel, Category>();



                //About 
                cfg.CreateMap<About, AboutModel>()
                    .ForMember(x => x.AboutTitleDictionary, o => o.MapFrom(src => src.About_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.AboutTitle)))
                    .ForMember(x => x.AboutDescDictionary, o => o.MapFrom(src => src.About_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.AboutDescription)))
                    .ForMember(x => x.VisionTitleDictionary, o => o.MapFrom(src => src.About_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.VisionTitle)))
                    .ForMember(x => x.VisionDescDictionary, o => o.MapFrom(src => src.About_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.VisionDescription)))
                    .ForMember(x => x.MissionTitleDictionary, o => o.MapFrom(src => src.About_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.MissionTitle)))
                    .ForMember(x => x.MissionDescDictionary, o => o.MapFrom(src => src.About_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.MissionDescription)))
                    .ForMember(x => x.CoreValueTitleDictionary, o => o.MapFrom(src => src.About_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.CoreValueTitle)))
                    .ForMember(x => x.CoreValueDescDictionary, o => o.MapFrom(src => src.About_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.CoreValueDescription)));

                cfg.CreateMap<AboutModel, About>();

                //HomeSlider
                cfg.CreateMap<HomeSlider, HomeSliderModel>()
                    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.HomeSlider_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.HomeSlider_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));

                cfg.CreateMap<HomeSliderModel, HomeSlider>();
                //News
                cfg.CreateMap<News, NewsModel>()
                    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.News_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.News_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));

                cfg.CreateMap<NewsModel, News>();



                //Vision
                //cfg.CreateMap<Vision, VisionModel>()
                //    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.Vision_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                //    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.Vision_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));

                //cfg.CreateMap<VisionModel, Vision>(); 

                ////Mission
                //cfg.CreateMap<Mission, MissionModel>()
                //    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.Mission_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                //    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.Mission_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));

                cfg.CreateMap<MissionModel, Mission>();

                // Features
                cfg.CreateMap<Feature, FeaturesModel>()
                    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.Features_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.Features_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));

                cfg.CreateMap<FeaturesModel, Feature>();
                //cfg.CreateMap<SystemParameters_Features_Translate, Features>();
                //cfg.CreateMap<Features, SystemParameters_Features_Translate>();

                //Hotels
                cfg.CreateMap<Hotel, HotelsModel>()
                    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.Hotels_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.Hotels_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Descrtiption)))
                    .ForMember(x => x.ImageList, o => o.MapFrom(src => src.Hotels_Images.Where(x => x.IsDeleted == false).ToList()))
                    .ForMember(x => x.FeaturesList, o => o.MapFrom(src => src.Hotels_Features.Where(x => x.IsDeleted == false).ToList()));

                cfg.CreateMap<HotelsModel, Hotel>();


                cfg.CreateMap<Hotels_Images, HotelImages>();
                cfg.CreateMap<HotelImages, Hotels_Images>();
                //cfg.CreateMap<Hotels_Images_Translate, HotelImages>();
                //cfg.CreateMap<HotelImages, Hotels_Images_Translate>();

                cfg.CreateMap<HotelFeatures, Feature>();
                cfg.CreateMap<Feature, HotelFeatures>();

                cfg.CreateMap<Hotels_Features, HotelFeatures>();
                cfg.CreateMap<HotelFeatures, Hotels_Features>();


                //cfg.CreateMap<Hotels_Features_Translate, HotelFeatures>();
                //cfg.CreateMap<HotelFeatures, Hotels_Features_Translate>();


                //Owners
                cfg.CreateMap<Owner, OwnerModel>()
                       .ForMember(x => x.OwnerNameDictionary, o => o.MapFrom(src => src.Owners_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.DisplayValueName)))
                       .ForMember(x => x.OwnerPostionDictionary, o => o.MapFrom(src => src.Owners_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.DisplayValuePosition)))
                       .ForMember(x => x.OwnerDescDictionary, o => o.MapFrom(src => src.Owners_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.DisplayValueDesc)));

                cfg.CreateMap<OwnerModel, Owner>();
                //cfg.CreateMap<SystemParameters_Owners_Translate, Owners>();
                //cfg.CreateMap<Owners, SystemParameters_Owners_Translate>();


                ////Core Values
                //cfg.CreateMap<CoreValues, CoreValuesModel>()
                //    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.CoreValues_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                //    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.Mission_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));


                //  cfg.CreateMap<CoreValuesModel, CoreValues>(); 

                //Contact Us
                cfg.CreateMap<ContactU, ContactUsModel>()
                .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.ContactUs_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));

                cfg.CreateMap<ContactUsModel, ContactU>();


                //Offers
                cfg.CreateMap<Offer, OfferModel>()
                    .ForMember(x => x.OfferTitleDictionary, o => o.MapFrom(src => src.Offers_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Title)))
                    .ForMember(x => x.OfferDescDictionary, o => o.MapFrom(src => src.Offers_Translate.ToDictionary(lang => lang.langId.ToLower(), lang => lang.Description)));

                cfg.CreateMap<OfferModel, Offer>();
                //cfg.CreateMap<Hotles_Offers_Translate, Offer>();
                //cfg.CreateMap<Offer, Hotles_Offers_Translate>();
                //cfg.CreateMap<Hotles_Offers_Translate, Hotles_Offers>();


                //Currency
                cfg.CreateMap<Currency, CurrencyModel>()
                    .ForMember(x => x.TitleDictionary, o => o.MapFrom(src => src.Currency_Translate.ToDictionary(lang => lang.LangId.ToLower(), lang => lang.Title)))
                    .ForMember(x => x.DescDictionary, o => o.MapFrom(src => src.Currency_Translate.ToDictionary(lang => lang.LangId.ToLower(), lang => lang.Description)));

                cfg.CreateMap<CurrencyModel, Currency>();
                //cfg.CreateMap<Currency_Translate, CurrencyVm>();
                //cfg.CreateMap<CurrencyVm, Currency_Translate>();


                cfg.CreateMap<SystemParameters_Newsletter, Newsletter>();
                cfg.CreateMap<Newsletter, SystemParameters_Newsletter>();

                cfg.CreateMap<SystemParameters_ContactForm, ContactForm>();
                cfg.CreateMap<ContactForm, SystemParameters_ContactForm>();


                cfg.CreateMap<Career, CareerModel>();
                cfg.CreateMap<CareerModel, Career>();

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