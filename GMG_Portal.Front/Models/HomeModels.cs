using System.Collections.Generic;
using Front.Models;
 using GMG_Portal.API.Models.SystemParameters;


namespace Front.Models
{
    public class HomeModels
    {
        public List<HomeSlider> HomeSliders { get; set; }
        public List<About> About { get; set; }
        public List<Features> Features { get; set; }
        public List<Hotels> Hotels { get; set; }
        public List<News> News { get; set; }
    }
}