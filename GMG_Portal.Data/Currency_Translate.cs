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
    
    public partial class Currency_Translate
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<bool> Show { get; set; }
        public string DisplayValueDesc { get; set; }
        public Nullable<System.DateTime> LastModificationTime { get; set; }
        public Nullable<int> LastModifierUserId { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<int> CreatorUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public Nullable<int> DeleterUserId { get; set; }
        public string LangId { get; set; }
    }
}
