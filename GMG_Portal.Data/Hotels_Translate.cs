//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GMG_Portal.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hotels_Translate
    {
        public int Id { get; set; }
        public Nullable<System.Guid> SGuid { get; set; }
        public string DisplayValue { get; set; }
        public Nullable<int> LookupKey { get; set; }
        public Nullable<System.Guid> LookupKeyGuid { get; set; }
        public Nullable<bool> IsCurrent { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> Rate { get; set; }
        public string DisplayValueDesc { get; set; }
        public Nullable<int> LookupKeyDesc { get; set; }
        public Nullable<System.Guid> LookupKeyGuidDesc { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public Nullable<double> Late { get; set; }
        public Nullable<double> Long { get; set; }
        public string DisplayValueAddress { get; set; }
        public Nullable<int> LookupKeyAddress { get; set; }
        public Nullable<System.Guid> LookupKeyGuidAddress { get; set; }
        public string Email { get; set; }
        public Nullable<int> PriceStart { get; set; }
        public Nullable<double> DistanceDownTown { get; set; }
        public Nullable<System.DateTime> LastModificationTime { get; set; }
        public Nullable<int> LastModifierUserId { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<int> CreatorUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public Nullable<int> DeleterUserId { get; set; }
        public bool Show { get; set; }
        public string langId { get; set; }
        public Nullable<int> Currency { get; set; }
    }
}
