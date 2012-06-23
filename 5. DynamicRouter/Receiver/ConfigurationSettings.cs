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
            ControlChannelName = ConfigurationManager.AppSettings["ControlChannelName"];
            Topic = ConfigurationManager.AppSettings["Topic"];
            PollingTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["PollingTimeout"]);
        }
    }
}
