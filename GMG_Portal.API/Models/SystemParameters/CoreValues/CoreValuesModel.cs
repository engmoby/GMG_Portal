using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class CoreValuesModel
    {
        public int Id { get; set; }
      
        public bool IsDeleted { get; set; }
      
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public string langId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; } 
    }
}