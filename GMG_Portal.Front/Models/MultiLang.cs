using AutoMapper.XpressionMapper;
using System.ComponentModel.DataAnnotations;

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