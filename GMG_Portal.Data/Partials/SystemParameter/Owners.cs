using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class Owner
    {
        [NotMapped]
        public string OperationStatus { get; set; }
        public int Bootstrap { get; set; }

        public Dictionary<string, string> OwnerNameDictionary { get; set; }
        public Dictionary<string, string> OwnerPostionDictionary { get; set; }
        public Dictionary<string, string> OwnerDescDictionary { get; set; }

    }
}
