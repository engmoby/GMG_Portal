using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class Owners
    {
        public int Id { get; set; }
        public Guid? SGuid { get; set; }
        public string DisplayValueName { get; set; }
        public int? LookupKeyName { get; set; }
        public Guid? LookupKeyGuidName { get; set; }
        public string DisplayValuePosition { get; set; }
        public int? LookupKeyPosition { get; set; }
        public Guid? LookupKeyGuidPosition { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Show { get; set; }
        public string DisplayValueDesc { get; set; }
        public int? LookupKeyDesc { get; set; }
        public Guid? LookupKeyGuidDesc { get; set; }
        public string Image { get; set; } 
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public int Bootstrap { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
    }
}