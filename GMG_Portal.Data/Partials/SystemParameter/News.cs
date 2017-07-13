using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class SystemParameters_News
    {
        [NotMapped]
        public string OperationStatus { get; set; }

        public string CreatorUserName { get; set; } 
        public string CategoryName { get; set; }
        public int CreationDay { get; set; }
        public int CreationMonth { get; set; }
        public List<SystemParameters_Category> Categories { get; set; }
    }
}
