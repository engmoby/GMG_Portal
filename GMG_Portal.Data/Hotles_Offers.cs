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
    
    public partial class Hotles_Offers
    {
        public int Id { get; set; }
        public Nullable<System.Guid> SGuid { get; set; }
        public Nullable<int> Hotel_Id { get; set; }
        public string DisplayValue { get; set; }
        public Nullable<int> LookupKey { get; set; }
        public Nullable<System.Guid> LookupKeyGuid { get; set; }
        public bool IsDeleted { get; set; }
        public string DisplayValueDesc { get; set; }
        public Nullable<int> LookupKeyDesc { get; set; }
        public Nullable<System.Guid> LookupKeyGuidDesc { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> Show { get; set; }
        public Nullable<System.DateTime> LastModificationTime { get; set; }
        public Nullable<int> LastModifierUserId { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<int> CreatorUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public Nullable<int> DeleterUserId { get; set; }
        public string Image { get; set; }
        public Nullable<int> Currency { get; set; }
    }
}
