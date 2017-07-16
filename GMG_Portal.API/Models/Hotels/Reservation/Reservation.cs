using System;

namespace GMG_Portal.API.Models.Hotels.Reservation
{
    public class Reservation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? Adult { get; set; }
        public int? Child { get; set; }
        public int? HotelId { get; set; }
        public int? CountryId { get; set; }
        public string Notes { get; set; }
        public DateTime? SeenDate { get; set; }
        public int? SeenBy { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}