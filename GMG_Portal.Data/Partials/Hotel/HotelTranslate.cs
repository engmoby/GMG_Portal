using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class Hotels_Translate
    {
        [NotMapped]
        public string Image { get; set; }
        public string OperationStatus { get; set; }
        public int Bootstrap { get; set; }
        public List<SystemParameters_Features_Translate> FeaturesList { get; set; }
        public List<Hotels_Images_Translate> ImageList { get; set; }
        public bool HasImage { get; set; }

    }
}
