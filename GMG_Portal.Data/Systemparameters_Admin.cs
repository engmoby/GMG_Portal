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
    
    public partial class Systemparameters_Admin
    {
        public int Id { get; set; }
        public Nullable<System.Guid> SGuid { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<bool> Show { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string PassWd { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Icon { get; set; }
        public Nullable<System.DateTime> LastModificationTime { get; set; }
        public Nullable<int> LastModifierUserId { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<int> CreatorUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public Nullable<int> DeleterUserId { get; set; }
        public string DisplayFront { get; set; }
    }
}
