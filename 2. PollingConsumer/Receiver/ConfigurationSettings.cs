using System;
using System.Configuration;

namespace Receiver
{
    public static class ConfigurationSettings
    {
        public static int PollingInterval{get;set;}
        public static long PollingTimeout{get;set;}

        static ConfigurationSettings()
        {
            PollingInterval = Convert.ToInt32(ConfigurationManager.AppSettings["PollingInterval"]);
            PollingTimeout = Convert.ToInt64(ConfigurationManager.AppSettings["PollingTimeout"]);
        }
    }
}
