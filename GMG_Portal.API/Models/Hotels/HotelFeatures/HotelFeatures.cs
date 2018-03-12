using System;

namespace GMG_Portal.API.Models.Hotels.HotelFeatures
{
    public class HotelFeatures
    {
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
    }
}