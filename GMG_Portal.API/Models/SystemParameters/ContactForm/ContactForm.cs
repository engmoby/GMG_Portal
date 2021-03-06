﻿using System;

namespace GMG_Portal.API.Models.SystemParameters.ContactUs
{
    public class ContactForm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? SeenBy { get; set; }
        public DateTime? SeenDate { get; set; }
        public bool? Seen { get; set; }
        public string OperationStatus { get; set; }
    }
}