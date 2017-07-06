using System;
using System.ComponentModel.DataAnnotations;

namespace Front.Models
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
        public string URL { get; set; }
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd}")]
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
    }
}