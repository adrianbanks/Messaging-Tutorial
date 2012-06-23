using System.Configuration;

namespace MessageUtilities
{
    public class ChannelConfiguration
    {
        public static string Name{get;set;}

        static ChannelConfiguration()
        {
            Name = ConfigurationManager.AppSettings["ChannelName"];
        }
    }
}