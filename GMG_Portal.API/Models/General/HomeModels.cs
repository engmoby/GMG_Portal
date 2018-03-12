using System.Collections.Generic;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.SystemParameters.ContactUs;

namespace GMG_Portal.API.Models.General
{
    public class HomeModels
    {
        public List<HomeSliderModel> HomeSliders { get; set; }
        public AboutModel About { get; set; }
        public List<FeaturesModel> Features { get; set; }
        public List<HotelsModel> Hotels { get; set; }
        public List<OwnerModel> Owners { get; set; }
        public List<NewsModel> News { get; set; }
        public ContactUsModel ContactUs { get; set; }
        public List<HotelImages> Gallery { get; set; }
        public List<CurrencyModel> Currency { get; set; }
    }
}