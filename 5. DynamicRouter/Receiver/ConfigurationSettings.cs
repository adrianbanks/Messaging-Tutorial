using System;
using System.Configuration;
using MessageUtilities;

namespace Receiver
{
    public static class ConfigurationSettings
    {
        public static string ControlChannelName{get;set;}
        public static string Topic{get;set;}
        public static int PollingTimeout{get;set;}

        static ConfigurationSettings()
        {
            string channelName = ConfigurationManager.AppSettings["ControlChannelName"];
            ControlChannelName = ChannelConfiguration.GetFullChannelName(channelName);
            Topic = ConfigurationManager.AppSettings["Topic"];
            PollingTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["PollingTimeout"]);
        }
    }
}
