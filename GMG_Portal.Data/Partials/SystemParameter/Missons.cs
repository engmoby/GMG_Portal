﻿using System.Collections.Generic;

namespace GMG_Portal.Data
{
    public partial class Mission
    { 
        public string OperationStatus { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }
    }
}
