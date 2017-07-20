using System;
using System.ComponentModel.DataAnnotations;

namespace GMG_Portal.API.Models.Hotels.Reservation
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Your FirstName")]
        [Display(Name = "FirstName")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Your LastName")]
        [Display(Name = "LastName")]
        [MaxLength(100)]
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
        public int? OperationId { get; set; }

    }
}