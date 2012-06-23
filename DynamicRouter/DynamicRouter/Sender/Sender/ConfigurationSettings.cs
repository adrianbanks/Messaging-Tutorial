using System.Configuration;

namespace Sender
{
    class ConfigurationSettings
    {
        static ConfigurationSettings()
        {
            OutBoundChannel = ConfigurationManager.AppSettings["OutputChannel"];
        }

        public static string OutBoundChannel { get; set; }
    }
}
