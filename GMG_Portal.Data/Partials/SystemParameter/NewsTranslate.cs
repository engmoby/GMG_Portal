using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class SystemParameters_News_Translate
    {
        [NotMapped]
        public string OperationStatus { get; set; }
         
        public string CreatorUserName { get; set; } 
        public string CategoryName { get; set; }
        public int CreationDay { get; set; }
        public string CreationMonth { get; set; }
        public List<Category> Categories { get; set; }
    }
}
