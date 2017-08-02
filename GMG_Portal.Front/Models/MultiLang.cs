using System.ComponentModel.DataAnnotations;
using AutoMapper.XpressionMapper;

namespace Front.Models
{
    public class MultiLang
    {
        [Display(Name = "Home", ResourceType = typeof(Resource))]
        public string Home { get; set; }

        [Display(Name = "About", ResourceType = typeof(Resource))]
        public string Hotels { get; set; }
    }
}