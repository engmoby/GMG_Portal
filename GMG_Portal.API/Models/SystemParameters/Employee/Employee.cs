namespace GMG_Portal.API.Models.SystemParameters
{
    public class Employee
    {
        public System.Guid ID { get; set; }
        public string FirstNameAr { get; set; }
        public string FirstNameEn { get; set; }
        public string MidNameAr { get; set; }
        public string MidNameEn { get; set; }
        public string LastNameAr { get; set; }
        public string LastNameEn { get; set; }
        public  bool  IsSalesAgent { get; set; }
        public bool IsDeleted { get; set; }

    }
}