﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMG_Portal.Data
{
    public partial class Hotels_Features_Translate
    {
        [NotMapped]
        public string OperationStatus { get; set; }
    }
}
