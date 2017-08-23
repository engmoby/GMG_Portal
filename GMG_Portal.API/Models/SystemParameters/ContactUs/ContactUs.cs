using System;

namespace GMG_Portal.API.Models.SystemParameters.ContactUs
{
    public class ContactUs
    {
        public int Id { get; set; }
        public Guid? SGuid { get; set; }
        public string DisplayValueAddress { get; set; }
        public int? LookupKeyAddress { get; set; }
        public Guid? LookupKeyGuidAddress { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Show { get; set; }
        public string DisplayValueDesc { get; set; }
        public int? LookupKeyDesc { get; set; }
        public Guid? LookupKeyGuidDesc { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instgram { get; set; }
        public string Youtube { get; set; }
        public string Snapchat { get; set; }
        public string Fax { get; set; }
        public string WhatsApp { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string PostalCode { get; set; }
        public string Mailbox { get; set; }
        public double? Late { get; set; }
        public double? Long { get; set; }

        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public string MailNo1 { get; set; }
        public string MailNo2 { get; set; }
        public string langId { get; set; }

    }
}