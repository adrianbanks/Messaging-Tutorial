using System;
using System.Configuration;

namespace Receiver
{
    public static class ConfigurationSettings
    {
        public static int PollingTimeout{get;set;}
        public static string[] Topics{get;set;}

        static ConfigurationSettings()
        {
            PollingTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["PollingTimeout"]);

            string topics = ConfigurationManager.AppSettings["Topics"];
            Topics = topics.Split(';');
        }
    }
}
