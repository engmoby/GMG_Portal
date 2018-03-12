using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class HomeSlider
    {
        [NotMapped]
        public string OperationStatus { get; set; }

        public Dictionary<string, string> TitleDictionary { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }
    }
}
