using System;
using System.Collections.Generic;
using GMG_Portal.Data;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class FeaturesModel
    {
        public string langId;
        public int Id { get; set; }  
        public bool IsDeleted { get; set; }  
        public string Icon { get; set; } 
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }



        public virtual Feature Feature { get; set; }
    }
}