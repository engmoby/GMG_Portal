using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.DateTime;

namespace Heloper
{
    public class Parameters
    { 
        static readonly DateTime ThisTime = Now;
        static readonly TimeZoneInfo Tst = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
        static readonly DateTime TstTime = TimeZoneInfo.ConvertTime(ThisTime, TimeZoneInfo.Local, Tst); 

        public static int UserId = 1;
        public static DateTime CurrentDateTime = TstTime;
        public static bool Show = true; 
        public static string DefaultLang = "en";
    }
}
