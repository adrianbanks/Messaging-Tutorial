using System.Configuration;

namespace MessageUtilities
{
    public class ChannelConfiguration
    {
        public static string Name{get;set;}

        static ChannelConfiguration()
        {
            string channelName = ConfigurationManager.AppSettings["ChannelName"];
            Name = GetFullChannelName(channelName);
        }

        public static string GetFullChannelName(string part)
        {
            return string.Format(@".\private$\{0}", part);
        }
    }
}