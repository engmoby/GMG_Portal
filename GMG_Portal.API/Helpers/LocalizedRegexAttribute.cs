using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.ModelBinding;

namespace GMG_Portal.API.Helpers
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class LocalizedRegexAttribute : RegularExpressionAttribute
    {


        static LocalizedRegexAttribute()
        {
            // necessary to enable client side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(LocalizedRegexAttribute), typeof(System.Web.ModelBinding.RegularExpressionAttributeAdapter));
        }

        public LocalizedRegexAttribute(string _RegularExpression)
            : base(LoadRegex(_RegularExpression))
        {            
        }

        private static string LoadRegex(string key)
        {
            return GMG_Portal.Content.Regex.ResourceManager.GetObject(key)?.ToString();
        }
    }
}