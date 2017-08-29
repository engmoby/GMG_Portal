﻿using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class SystemParameters_Notify
    {
      

        [NotMapped]
        public string OperationStatus { get; set; }
        public string DepartmentName { get; set; }

        public List<SystemParameters_NotifyDepartment>  Departments { get; set; }
       
    }
}
