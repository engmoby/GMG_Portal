namespace GMG_Portal.API.Models.Customer.Customers
{
    public class Customer
    {

        public System.Guid Id { get; set; }
        public string FirstNameAr { get; set; }
        public string FirstNameEn { get; set; }
        public string MidNameAr { get; set; }
        public string MidNameEn { get; set; }
        public string LastNameAr { get; set; }
        public string LastNameEn { get; set; }
        public  int CityId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public  decimal MailBoxNo { get; set; }
        public  decimal PhoneNo { get; set; }
        public  decimal FaxNo { get; set; }
        public  decimal MobileNo { get; set; }
        public string Email { get; set; }
        public string WaselEmail { get; set; }
        public  int AccountTypeId { get; set; }
        public  int RegionId { get; set; }
        public  decimal ContractNo { get; set; }
        public string ContractDate { get; set; }
        public string PersonInCharge { get; set; }
        public  decimal CoMobileNo { get; set; }
        public  decimal CoPhoneNo { get; set; }
        public string CoEmail { get; set; }
        public  int PaymentTypeId { get; set; }
        public  int InstallementNos { get; set; }
        public  int CustomerAccountStatusId { get; set; }
        public string ActivationCode { get; set; }
        public  bool IsDeleted { get; set; }
    }
}