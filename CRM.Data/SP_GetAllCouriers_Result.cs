//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM.Data
{
    using System;
    
    public partial class SP_GetAllCouriers_Result
    {
        public System.Guid ID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Nullable<int> ShipmentTypeID { get; set; }
        public string ShipmentPolicyAr { get; set; }
        public string ShipmentPolicyEn { get; set; }
        public string DeliveryPolicyAr { get; set; }
        public string DeliveryPolicyEn { get; set; }
        public Nullable<bool> IsSalesAgent { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
