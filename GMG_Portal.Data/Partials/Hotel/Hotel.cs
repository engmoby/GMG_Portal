using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class Hotel
    {
        [NotMapped]
        public string Image { get; set; }
        public string OperationStatus { get; set; }
        public int Bootstrap { get; set; } 
    }
}
