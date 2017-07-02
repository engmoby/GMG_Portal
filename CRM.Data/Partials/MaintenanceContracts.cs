using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Data
{
    public partial class MaintenanceContracts
    {
        [NotMapped]
        public string OperationStatus { get; set; }
    }
}
