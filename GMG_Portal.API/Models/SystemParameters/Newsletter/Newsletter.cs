using System;

namespace GMG_Portal.API.Models.SystemParameters.Newsletter
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? SeenDate { get; set; }
        public int? SeenBy { get; set; }
        public string OperationStatus { get; set; }
    }
}