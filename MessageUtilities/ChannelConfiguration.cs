using System.Configuration;

namespace MessageUtilities
{
    public class ChannelConfiguration
    {
        public static string Name{get;set;}

        static ChannelConfiguration()
        {
            string channelName = ConfigurationManager.AppSettings["ChannelName"];
            Name = string.Format(@".\private$\{0}", channelName);
        }
    }
}