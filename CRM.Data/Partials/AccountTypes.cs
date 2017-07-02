using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
    public partial class AccountTypes
    {
        [NotMapped]
        public string OperationStatus { get; set; }
    }
}
