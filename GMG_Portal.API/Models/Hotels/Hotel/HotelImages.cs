using System;

namespace GMG_Portal.API.Models.Hotels.Hotel
{
    public class HotelImages
    {
        public int Id { get; set; }
        public Guid? SGuid { get; set; }
        public string Image { get; set; }
        public int? Hotel_Id { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public bool Show { get; set; }
        public string OperationStatus { get; set; }
    }
}