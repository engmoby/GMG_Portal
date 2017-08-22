using System;

namespace GMG_Portal.API.Models.Hotels.Hotel
{
    public class HotelFeatures
    {
        public int Id { get; set; }
        public int? Hotel_Id { get; set; }
        public int? Feature_Id { get; set; }
        public Guid? SGuid { get; set; }
        public bool IsDeleted { get; set; }
        public string langId { get; set; }

    }

}