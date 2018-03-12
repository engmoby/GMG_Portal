using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class Hotel
    { 
        [NotMapped]
        public string Image { get; set; }
        public string OperationStatus { get; set; }
        public int Bootstrap { get; set; }
        public List<Feature> FeaturesList { get; set; }
        public List<Hotels_Images> ImageList { get; set; }
        public bool HasImage { get; set; }
        public string CurrencyTitle { get; set; }


        public Dictionary<string, string> TitleDictionary { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }
    }
}
