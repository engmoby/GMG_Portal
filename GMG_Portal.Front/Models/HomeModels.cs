using System.Collections.Generic;
using Front.Models;
using GMG_Portal.API.Models.Hotels.Hotel; 
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.API.Models.SystemParameters.ContactUs;
using GMG_Portal.Data;


namespace Front.Models
{
    public class HomeModels
    {
        public List<HomeSlider> HomeSliders { get; set; }
        public List<About> About { get; set; }
        public List<Features> Features { get; set; }
        public List<Hotels> Hotels { get; set; }
        public List<Owners> Owners { get; set; }
        public List<News> News { get; set; }
        public List<ContactUs> ContactUs { get; set; }
        public List<HotelImages> Gallery { get; set; }
    }



    public class NavModels
    {
        public List<HomeSlider> HomeSliders { get; set; }
        public List<About> About { get; set; }
        public List<Features> Features { get; set; }
        public List<Hotels> Hotels { get; set; }
        public List<Owners> Owners { get; set; }
        public List<News> News { get; set; }
        public List<ContactUs> ContactUs { get; set; }
        public List<HotelImages> Gallery { get; set; }
    }
}