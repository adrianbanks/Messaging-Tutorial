﻿using System;
using System.Configuration;

namespace Receiver
{
    public static class ConfigurationSettings
    {
        static ConfigurationSettings()
        {
            PollingInterval = Convert.ToInt32(ConfigurationManager.AppSettings["PollingInterval"]);
            PollingTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["PollingTimeout"]);
            InBoundChannelName = ConfigurationManager.AppSettings["InBoundChannelName"];

            var topics = ConfigurationManager.AppSettings["Topics"];
            Topics = topics.Split(';');
        }

        public static int PollingInterval { get; set; }
        public static int PollingTimeout { get; set; }
        public static string InBoundChannelName { get; set; }
        public static string[] Topics { get; set; }
    }
}
