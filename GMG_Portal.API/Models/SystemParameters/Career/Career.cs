using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class Career
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

        public string Experience { get; set; }
        public string CareerLevel { get; set; }
        public string JobType { get; set; }
        public string EducationLevel { get; set; }
        public int? Vacancies { get; set; }
        public string DisplayValueRequirements { get; set; }
        public int? LookupKeyRequirements { get; set; }
        public Guid? LookupKeyGuidRequirements { get; set; }
        public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public string SalaryAverage { get; set; }

    }
}