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
    
    public partial class Hotels_Hotel_Videos
    {
        public int Id { get; set; }
        public Nullable<System.Guid> SGuid { get; set; }
        public string Video { get; set; }
        public Nullable<int> Hotel_Id { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime LastModificationTime { get; set; }
        public Nullable<int> LastModifierUserId { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<int> CreatorUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public Nullable<int> DeleterUserId { get; set; }
        public bool Show { get; set; }
    }
}
