using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class News
    {
        public int Id { get; set; }
        public Guid? SGuid { get; set; }
        public string DisplayValue { get; set; }
        public int? LookupKey { get; set; }
        public Guid? LookupKeyGuid { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Show { get; set; }
        public string DisplayValueDesc { get; set; }
        public int? LookupKeyDesc { get; set; }
        public Guid? LookupKeyGuidDesc { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public List<string> Tags { get; set; }
        public int CreationDay { get; set; }
        public int CreationMonth { get; set; }
        public List<Category> Categories { get; set; }
        public string LangId { get; set; }
    }
}