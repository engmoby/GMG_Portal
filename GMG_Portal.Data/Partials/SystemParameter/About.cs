using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class About
    {
      

        [NotMapped]
        public string OperationStatus { get; set; }
        public Dictionary<string, string> AboutTitleDictionary { get; set; }
        public Dictionary<string, string> AboutDescDictionary { get; set; }
        public Dictionary<string, string> VisionTitleDictionary { get; set; }
        public Dictionary<string, string> VisionDescDictionary { get; set; }
        public Dictionary<string, string> MissionTitleDictionary { get; set; }
        public Dictionary<string, string> MissionDescDictionary { get; set; }
        public Dictionary<string, string> CoreValueTitleDictionary { get; set; }
        public Dictionary<string, string> CoreValueDescDictionary { get; set; }

    }
}
