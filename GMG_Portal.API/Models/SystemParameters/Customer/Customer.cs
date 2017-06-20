using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class Customer
    {
        public System.Guid ID { get; set; }
        public string FirstNameAr { get; set; }
        public string FirstNameEn { get; set; }
        public string MidNameAr { get; set; }
        public string MidNameEn { get; set; }
        public string LastNameAr { get; set; }
        public string LastNameEn { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public Nullable<decimal> MailBoxNo { get; set; }
        public Nullable<decimal> PhoneNo { get; set; }
        public Nullable<decimal> FaxNo { get; set; }
        public Nullable<decimal> MobileNo { get; set; }
        public string Email { get; set; }
        public string WaselEmail { get; set; }
        public Nullable<int> AccountTypeID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<decimal> ContractNo { get; set; }
        public string ContractDate { get; set; }
        public string PersonInCharge { get; set; }
        public Nullable<decimal> CoMobileNo { get; set; }
        public Nullable<decimal> CoPhoneNo { get; set; }
        public string CoEmail { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<int> InstallementNos { get; set; }
        public Nullable<int> CustomerAccountStatusID { get; set; }
        public string ActivationCode { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        //public List<CustomerInstallment> CustomerInstallments = new List<CustomerInstallment>();

        public string OperationStatus { get; set; }
        public List<string> ContractImages = new List<string>();
        
        public string AccountTypeNameAr { get; set; }
        public string AccountTypeNameEn { get; set; }
        public string PaymentTypeNameAr { get; set; }
        public string PaymentTypeNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public int CountryID { get; set; }
        public string CountryNameEn { get; set; }
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }
        public string AccountStatusNameAr { get; set; }
        public string AccountStatusNameEn { get; set; }



    }
}