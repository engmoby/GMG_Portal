using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.General
{
    public class FileDetail
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; } 

    }
}