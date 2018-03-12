using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class Owners_Translate
    {
        [NotMapped]
        public string OperationStatus { get; set; }
        public int Bootstrap { get; set; }
    }
}
