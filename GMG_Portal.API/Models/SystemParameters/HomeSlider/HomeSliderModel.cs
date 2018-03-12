using System;
using System.Collections.Generic;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class HomeSliderModel
    {
        public string langId;
        public int Id { get; set; }
      
        public bool IsDeleted { get; set; }
     
        public string Image { get; set; }
        public int? Rating { get; set; } 
        public System.DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public int SliderCount { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }
    }
}