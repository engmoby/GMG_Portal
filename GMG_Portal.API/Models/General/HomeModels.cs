using System.Collections.Generic;
using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.SystemParameters.ContactUs;

namespace GMG_Portal.API.Models.General
{
    public class HomeModels
    {
        public List<HomeSlider> HomeSliders { get; set; }
        public About About { get; set; }
        public List<Features> Features { get; set; }
        public List<Hotels.Hotel.Hotels> Hotels { get; set; }
        public List<Owners> Owners { get; set; }
        public List<News> News { get; set; }
        public ContactUs ContactUs { get; set; }
        public List<HotelImages> Gallery { get; set; }
    }
}