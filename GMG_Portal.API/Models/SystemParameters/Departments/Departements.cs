using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class Departments
    {
        public int ID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public string OperationStatus { get; set; }
    }
}