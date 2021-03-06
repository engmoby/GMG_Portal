﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class Feature
    {
        [NotMapped]
        public string OperationStatus { get; set; }
        public int HotelId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }
    }
}
