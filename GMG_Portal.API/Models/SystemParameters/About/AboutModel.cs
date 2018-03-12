using System;
using System.Collections.Generic;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class AboutModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public string LangId { get; set; }


        public Dictionary<string, string> AboutTitleDictionary { get; set; }
        public Dictionary<string, string> AboutDescDictionary { get; set; }
        public Dictionary<string, string> VisionTitleDictionary { get; set; }
        public Dictionary<string, string> VisionDescDictionary { get; set; }
        public Dictionary<string, string> MissionTitleDictionary { get; set; }
        public Dictionary<string, string> MissionDescDictionary { get; set; } 
        public Dictionary<string, string> CoreValueTitleDictionary { get; set; }
        public Dictionary<string, string> CoreValueDescDictionary { get; set; }
    }
}