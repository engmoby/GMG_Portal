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
    
    public partial class ContactUs_Translate
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string langId { get; set; }
        public Nullable<int> RecordId { get; set; }
    
        public virtual ContactU ContactU { get; set; }
    }
}
