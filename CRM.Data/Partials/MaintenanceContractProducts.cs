using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Data
{
    public partial class MaintenanceContractProducts
    {
        [NotMapped]
        public string OperationStatus { get; set; }
    }
}
