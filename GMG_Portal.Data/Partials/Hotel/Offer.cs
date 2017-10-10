using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class Hotles_Offers
    {
        public string langId;

        [NotMapped] 
        public string HotelTitle { get; set; }
        public string OperationStatus { get; set; }
        public string CurrencyTitleX { get; set; }
    }
}
