using System;
using System.ComponentModel.DataAnnotations;

namespace GMG_Portal.API.Models.SystemParameters.CareerForm
{
    public class CareerForm
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Please Enter FirstName")]
        //[Display(Name = "FirstName")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Please Enter LastName")]
        //[Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
      //  [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Please Enter PhoneNo")]
         [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        //[Display(Name = "PhoneNo")]
        public string PhoneNo { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? SeenBy { get; set; }
        public DateTime? SeenDate { get; set; }
        public bool? Seen { get; set; }
        public string OperationStatus { get; set; }
        //[Required(ErrorMessage = "Please Enter Attach Your CV")]
        //[Display(Name = "Attach")]
        public string Attach { get; set; }
        public string CareerTitle { get; set; }
        public int CareerId { get; set; }
    }
}