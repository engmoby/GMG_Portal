using System;
using System.Collections.Generic;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Data;

namespace GMG_Portal.API.Models.Hotels.Hotel
{
    public class HotelsModel
    {
        public int Id { get; set; }
      
        public bool IsDeleted { get; set; }
      
        public int? PriceStart { get; set; }
       // public string Image { get; set; }
       // public int? CityId { get; set; } 
        public System.DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public double? Late { get; set; }
        public double? Long { get; set; }
        public string DisplayValueAddress { get; set; }
        public int? LookupKeyAddress { get; set; }
        public Guid? LookupKeyGuidAddress { get; set; }
        public string Email { get; set; }
        public double? DistanceDownTown { get; set; }
        public string OperationStatus { get; set; }
        public List<HotelImages> ImageList { get; set; }
        public List<HotelFeatures> FeaturesList { get; set; }
        public int Bootstrap { get; set; }
        public int? Rate { get; set; }
        public bool HasImage { get; set; }
        public string langId { get; set; } 
        public int? Currency { get; set; }
        public string CurrencyTitle { get; set; } 
        public Dictionary<string, string> TitleDictionary { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }
    }
}