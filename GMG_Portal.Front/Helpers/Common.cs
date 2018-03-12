using GMG_Portal.API.Models.Hotels.Hotel;
using GMG_Portal.API.Models.SystemParameters;
using System.Collections.Generic;

namespace Front.Helpers
{
    public class Common
    {
        public static string CurrentLang = "en";
        public static List<HotelsModel> Hotels { get; set; }
        public static List<CurrencyModel> Currency { get; set; }
        public static List<FeaturesModel> Features { get; set; }
    }
}