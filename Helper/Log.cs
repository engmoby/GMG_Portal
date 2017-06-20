using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Helpers
{
    public class Log
    {
        public static void LogData(string Data)
        {

        }
        public static void LogError(Exception Exception)
        {
            //System.Diagnostics.EventLog.WriteEntry("Paymentgateway", Exception.Message,
            //                           System.Diagnostics.EventLogEntryType.Error);
        }


    }
}
