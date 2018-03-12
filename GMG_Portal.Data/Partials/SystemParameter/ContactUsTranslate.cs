using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class ContactUs_Translate
    {
        [NotMapped]
        public string OperationStatus { get; set; }
    }
}
