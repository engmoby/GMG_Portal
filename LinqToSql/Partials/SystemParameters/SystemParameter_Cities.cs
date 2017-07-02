using System.ComponentModel.DataAnnotations.Schema; 

namespace GMG_Portal.Data
{
    public partial class SystemParameter_Cities
    {
        [NotMapped]
        public string OperationStatus { get; set; }
    }
}
