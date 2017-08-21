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
        public List<SystemParameters_Features> FeaturesList { get; set; }
        public List<Hotels_Images> ImageList { get; set; }
        public bool HasImage { get; set; }

        public int? Original_HotelId { get; set; }
    }
}
