using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class CoreValues_Translate
    {
        [NotMapped]
        public string OperationStatus { get; set; }
    }
}
