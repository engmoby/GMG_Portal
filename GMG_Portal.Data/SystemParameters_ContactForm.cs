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
    
    public partial class SystemParameters_ContactForm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<int> SeenBy { get; set; }
        public Nullable<System.DateTime> SeenDate { get; set; }
        public Nullable<bool> Seen { get; set; }
    }
}
