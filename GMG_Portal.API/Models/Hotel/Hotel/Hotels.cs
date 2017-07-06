﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class Hotels
    {
        public int Id { get; set; }
        public Guid? SGuid { get; set; }
        public string DisplayValue { get; set; }
        public int? LookupKey { get; set; }
        public Guid? LookupKeyGuid { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Show { get; set; }
        public string DisplayValueDesc { get; set; }
        public int? LookupKeyDesc { get; set; }
        public Guid? LookupKeyGuidDesc { get; set; }
        public string Image { get; set; }
        public int? CityId { get; set; }
        public int? Rate { get; set; }
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public decimal? Late { get; set; }
        public decimal? Long { get; set; }
        public string DisplayValueAddress { get; set; }
        public int? LookupKeyAddress { get; set; }
        public Guid? LookupKeyGuidAddress { get; set; }
        public string Email { get; set; }
        public double? DistanceDownTown { get; set; }
        public string OperationStatus { get; set; }
    }
}