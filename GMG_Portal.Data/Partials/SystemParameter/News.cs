using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMG_Portal.Data
{
    public partial class News
    {
        [NotMapped]
        public string OperationStatus { get; set; }

        public Dictionary<string, string> TitleDictionary { get; set; }
        public Dictionary<string, string> DescDictionary { get; set; }
       // public  Category  Category { get; set; }

        public string CreatorUserName { get; set; } 
        public string CategoryName { get; set; }
        public int CreationDay { get; set; }
        public string CreationMonth { get; set; }
       public List<Category> Categories { get; set; }
    }
}
