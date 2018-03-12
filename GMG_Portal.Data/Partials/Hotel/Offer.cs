using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class Offer
    {
        public string langId;

        [NotMapped] 
        public string HotelTitle { get; set; }
        public string OperationStatus { get; set; }

        public Dictionary<string, string> OfferTitleDictionary { get; set; }
        public Dictionary<string, string> OfferDescDictionary { get; set; }

    }
}
