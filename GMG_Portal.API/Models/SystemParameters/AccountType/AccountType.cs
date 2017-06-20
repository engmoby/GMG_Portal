using GMG_Portal.API.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GMG_Portal.API.Helpers;

namespace GMG_Portal.API.Models.SystemParameters
{

    public class AccountType
    {
        public int ID { get; set; }

        [LocalizedRegex("NameAr")]
        public string NameAr { get; set; }
        [LocalizedRegex("NameEn")]
        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public string OperationStatus { get; set; }

    }
}