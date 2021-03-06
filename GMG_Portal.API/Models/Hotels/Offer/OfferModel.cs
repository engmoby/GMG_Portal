﻿using System;
using System.Collections.Generic;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class OfferModel
    {
        public int Id { get; set; }
        public int? Hotel_Id { get; set; }
        public string HotelTitle { get; set; }

        //public Guid? SGuid { get; set; }
        //public string DisplayValue { get; set; }
        //public int? LookupKey { get; set; }
        //public Guid? LookupKeyGuid { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Show { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Image { get; set; }
        //public string DisplayValueDesc { get; set; }
        //public int? LookupKeyDesc { get; set; }
        //public Guid? LookupKeyGuidDesc { get; set; }
        public System.DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }

        public string langId { get; set; }

        public string CurrencyTitle { get; set; }
        public int? Currency { get; set; }


        public Dictionary<string, string> OfferTitleDictionary { get; set; }
        public Dictionary<string, string> OfferDescDictionary { get; set; }
    }
}