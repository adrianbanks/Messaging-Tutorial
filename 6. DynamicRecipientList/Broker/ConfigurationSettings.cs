using System;
using System.Configuration;

namespace Receiver
{
    public static class ConfigurationSettings
    {
        public static string ControlChannel{get;set;}
        public static int PollingTimeout{get;set;}

        static ConfigurationSettings()
        {
            PollingTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["PollingTimeout"]);
            ControlChannel = ConfigurationManager.AppSettings["ControlChannelName"];
        }
    }
}
