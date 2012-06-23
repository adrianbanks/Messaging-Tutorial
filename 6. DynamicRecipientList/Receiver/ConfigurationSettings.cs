using System;
using System.Configuration;

namespace Receiver
{
    public static class ConfigurationSettings
    {
        public static string ControlChannelName{get;set;}
        public static string Topic{get;set;}
        public static int PollingTimeout{get;set;}

        static ConfigurationSettings()
        {
            PollingTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["PollingTimeout"]);
            Topic = ConfigurationManager.AppSettings["Topic"];
            ControlChannelName = ConfigurationManager.AppSettings["ControlChannelName"];
        }
    }
}
