using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class ContactU
    {
        [NotMapped]
        public string OperationStatus { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }

    }
}
