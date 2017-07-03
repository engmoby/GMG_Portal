using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data.Partials.SystemParameters
{
    public partial class AccountTypes
    {
        [NotMapped]
        public string OperationStatus { get; set; }
    }
}
