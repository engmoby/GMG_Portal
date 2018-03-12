using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class CareerModel
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; }
        public bool IsDeleted { get; set; }  
        public string Image { get; set; }

        public string Experience { get; set; }
        public string CareerLevel { get; set; }
        public string JobType { get; set; }
        public string EducationLevel { get; set; }
        public int? Vacancies { get; set; }
          public System.DateTime LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string OperationStatus { get; set; }
        public string SalaryAverage { get; set; }
        public int ApplyCount { get; set; }

    }
}