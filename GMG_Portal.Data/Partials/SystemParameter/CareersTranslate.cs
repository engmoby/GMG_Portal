using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class SystemParameters_Careers_Translate
    {
        [NotMapped]
        public int ApplyCount { get; set; }
        public string OperationStatus { get; set; }
    }
}
