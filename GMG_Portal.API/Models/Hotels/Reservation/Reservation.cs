using System;
using System.ComponentModel.DataAnnotations;

namespace GMG_Portal.API.Models.Hotels.Reservation
{
    public class Reservation
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Please Enter Your FirstName")]
        [Display(Name = "FirstName")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Please Enter Your LastName")]
        [Display(Name = "LastName")]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$")]
        public string Email { get; set; }
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,20}$")]
        public string Phone { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? Adult { get; set; }
        public int? Child { get; set; }
        public int? HotelId { get; set; }
        public string HotelName{ get; set; }
        public int? CountryId { get; set; }
        public string Notes { get; set; }
        public string HotelName { get; set; }
        public DateTime? SeenDate { get; set; }
        public int? SeenBy { get; set; }
        public bool Seen { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? OperationId { get; set; }
        public string OperationStatus { get; set; }

    }
}